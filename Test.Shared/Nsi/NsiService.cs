using System.Net;
using System.Text;
using System.Text.Json;
using Test.Shared.Nsi.Models;

namespace Test.Shared.Nsi;

public class NsiService
{

    private const string StartUrl = "https://www.nsinternational.com/api/v2/sales/traveloffers/search?returnAdmissionPricing=true&filterOnCheapestOffer=false";
    private const string scrollUrl = "https://www.nsinternational.com/api/v2/sales/traveloffers/search/:searchId:?scrollToken=NEXT_DEPARTURE&returnAdmissionPricing=true&filterOnCheapestOffer=false";

    public async Task<NsiResult?> GetNsiResultAsync(string from, string to, string date)
    {
        using var client = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip |
                                     DecompressionMethods.Deflate |
                                     DecompressionMethods.Brotli
        });
        OverrideHeaders(client);

        var body = GetBody(from, to, date);
        var response = await client.PostAsync(StartUrl, body);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Failed to get response from NS International");
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine(error);
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var result = ParseNsiResult(json);
        var allResults = new List<TravelOffer>(result?.Data?.TravelOffers ?? []);

        var nsiToken = response.Headers.Single(x => x.Key.Equals("X-Nsiapi-Convid", StringComparison.OrdinalIgnoreCase)).Value.First();
        var url = scrollUrl.Replace(":searchId:", result?.Data!.Scroll.SearchId);

        allResults.AddRange(await GetMoreResults(client, url, nsiToken));
        result!.Data!.TravelOffers = [.. allResults.OrderBy(x => x.Itinerary?.Origin?.Departure?.PlannedLocalDateTime)];

        return result;
    }

    public static async Task<List<TravelOffer>> GetMoreResults(HttpClient client, string searchId, string nsiToken)
    {
        var url = scrollUrl.Replace(":", searchId);
        while (true)
        {
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers = {
                    {"X-Nsiapi-Convid", nsiToken},
                }
            });

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to get response from NS International");
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);
                return [];
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = ParseNsiResult(json);
            if (string.IsNullOrEmpty(result?.Data?.Scroll.Later))
            {
                return result?.Data?.TravelOffers ?? [];
            }
        }
    }

    static NsiResult? ParseNsiResult(string json)
    {
        var result = JsonSerializer.Deserialize<NsiResult>(json);
        return result;
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










    private StringContent GetBody(string from, string to, string date)
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
}
