using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Test.Shared.Nsi.Models;

namespace Test.Api.Controllers;

[Route("api/[controller]")]
public class NsiController(ILogger<NsiController> logger, IMemoryCache cache)
    : ControllerBase
{
    private const string StartUrl = "https://www.nsinternational.com/api/v2/sales/traveloffers/search?returnAdmissionPricing=true&filterOnCheapestOffer=false";
    private const string scrollUrl = "https://www.nsinternational.com/api/v2/sales/traveloffers/search/:searchId:?scrollToken=NEXT_DEPARTURE&returnAdmissionPricing=true&filterOnCheapestOffer=false";

    [HttpGet("/api/search/{From}/{To}/{Date}")]
    public async Task Get([FromRoute(Name = "From")] string from, [FromRoute(Name = "To")] string to,
        [FromRoute(Name = "Date")] string date, CancellationToken token)
    {
        var key = $"NSI_SEARCH_{from}_{to}_{date}";
        if (cache.TryGetValue(key, out List<TravelOffer>? offers) && offers is not null)
        {
            await WriteBody(offers, Response);
            return;
        }

        Response.Headers.Append("Cache-Control", "no-cache");
        Response.ContentType = "text/event-stream";

        using var client = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip |
                                     DecompressionMethods.Deflate |
                                     DecompressionMethods.Brotli
        });
        OverrideHeaders(client);

        var response = await client.PostAsync(StartUrl, GetBody(from, to, date), token);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Failed to get response from NS International");
            var error = await response.Content.ReadAsStringAsync(token);
            logger.LogError("Error message: {error}", error);
            await WriteBody([], Response);
        }

        var json = await response.Content.ReadAsStringAsync(token);
        var result = ParseNsiResult(json);

        await WriteBody(result?.Data?.TravelOffers ?? [], Response);

        var searchId = result?.Data?.Scroll.SearchId;
        var nsiToken = response.Headers.Single(x => x.Key.Equals("X-Nsiapi-Convid", StringComparison.OrdinalIgnoreCase)).Value.First();
        var url = scrollUrl.Replace(":searchId:", searchId);
        while (true)
        {
            token.ThrowIfCancellationRequested();
            if (cache.TryGetValue(key, out List<TravelOffer>? tmpOffers) && tmpOffers is not null)
            {
                await WriteBody(tmpOffers, Response);
                return;
            }

            var innerResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers = {
                    {"X-Nsiapi-Convid", nsiToken},
                }
            }, token);

            if (!innerResponse.IsSuccessStatusCode)
            {
                logger.LogError("Failed to get response from NS International");
                var error = await innerResponse.Content.ReadAsStringAsync(token);
                logger.LogError("Error message: {error}", error);
                await WriteBody([], Response);
                break;
            }

            var innerJson = await innerResponse.Content.ReadAsStringAsync(token);
            var innerResult = ParseNsiResult(innerJson);
            await WriteBody(innerResult?.Data?.TravelOffers ?? [], Response);

            if (string.IsNullOrEmpty(innerResult?.Data?.Scroll.Later))
            {
                cache.Set(key, innerResult?.Data?.TravelOffers, TimeSpan.FromMinutes(5));
                break;
            }
        }

    }

    private static async Task WriteBody(List<TravelOffer> offers, HttpResponse response)
    {
        var jsonArray = JsonSerializer.Serialize(offers);
        var message = $"{jsonArray}\n\n";

        await response.WriteAsync(message);
        await response.Body.FlushAsync();
    }

    private static NsiResult? ParseNsiResult(string json)
    {
        var result = JsonSerializer.Deserialize<NsiResult>(json);
        return result;
    }

    private static StringContent GetBody(string from, string to, string date)
    {
        var body = $$"""
        {
            "destination": "{{to}}",
            "origin": "{{from}}",
            "outboundDatetime": {
                "datetime": "{{date}}T07:00:00",
                "timetype": "DEPARTURE"
            },
            "travelers": [
                {
                    "ageType": "A"
                }
            ]
        }
        """;

        return new StringContent(body, Encoding.UTF8, "application/json");
    }

    private static void OverrideHeaders(HttpClient client)
    {
        foreach (var item in OverrideHeadersValues)
        {
            client.DefaultRequestHeaders.Remove(item.Key);
            client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
        }
    }

    private static readonly Dictionary<string, string> OverrideHeadersValues = new()
    {
        {"Accept", "application/json, text/plain, */*"},
        {"Accept-Encoding", "gzip, deflate, br, zstd"},
        {"Accept-Language", "nl"},
        {"Cache-Control", "no-cache"},
        {"Origin", "https://www.nsinternational.com"},
        {"Pragma", "no-cache"},
        {"Priority", "u=1, i"},
        {"Referer", "https://www.nsinternational.com/nl/treintickets-v3/"},
        {"Sec-Ch-Ua", "\"Not A(Brand\";v=\"8\", \"Chromium\";v=\"132\", \"Google Chrome\";v=\"132\""},
        {"Sec-Ch-Ua-Mobile", "?0"},
        {"Sec-Ch-Ua-Platform", "\"Windows\""},
        {"Sec-Fetch-Dest", "empty"},
        {"Sec-Fetch-Mode", "cors"},
        {"Sec-Fetch-Site", "same-origin"},
        {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36"},
    };
}
