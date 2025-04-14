using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Test.Api.Controllers;

[Route("api/[controller]")]
public class StreamController : ControllerBase
{
    [HttpGet]
    public async Task GetStream()
    {

        // Set headers to disable caching (optional but useful for SSE)
        Response.Headers.Append("Cache-Control", "no-cache");

        // Set the Content-Type header to indicate SSE
        Response.ContentType = "text/event-stream";

        for (int i = 0; i < 10; i++)
        {
            // Create a JSON array for this iteration.
            var items = new[] { i, i * 10, i * 100 };
            var jsonArray = JsonSerializer.Serialize(items);

            // Format the message as an SSE event.
            string message = $"{jsonArray}\n\n";

            // Write the message to the response and flush the stream.
            await Response.WriteAsync(message);
            await Response.Body.FlushAsync();

            // Simulate a delay between events.
            await Task.Delay(500);
        }
    }
}
