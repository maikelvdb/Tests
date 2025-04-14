
using System.Globalization;
using HtmlAgilityPack;

var html = File.ReadAllText("Files/test-html.html");

var document = new HtmlDocument();
document.LoadHtml(html);
var needsLogin = document.DocumentNode.SelectSingleNode("//from[@action='/en/members/login']") != null;
if (needsLogin)
{
    return;
}

var headers = document.DocumentNode.SelectNodes("//table[@class='table table-bordered aggregate_table']//thead//tr[@class='aggregate_header']//th").Skip(4)
    .Select(c => c.InnerText.Trim())
    .ToDictionary(m => m, m => DateTime.ParseExact(m.TrimEnd('.'), "MMM", CultureInfo.InvariantCulture).Month);

var nodes = document.DocumentNode.SelectNodes("//table[@class='table table-bordered aggregate_table']//tbody//tr[@class='aggregate_row level-1']");

var collection = new Dictionary<string, long>();
foreach (var node in nodes)
{
    var brand = node.SelectSingleNode("th").InnerText.Trim()
        .Replace(" total", string.Empty, StringComparison.OrdinalIgnoreCase);

    var totalSells = node.SelectNodes("td")
        .Select(td => long.Parse(td.InnerText.Trim()
            .Replace("-", "0")
            .Replace(",", "")
            .Replace("N/A", "0"))).Sum();

    collection.Add(brand, totalSells);
}

var rowFormat = "|{0,50}|{1,10}";
var header = string.Format(rowFormat, "Brand", "Total");
Console.WriteLine(header);
foreach (var (brand, total) in collection)
{
    Console.WriteLine(string.Format(rowFormat, brand, total));
}

Console.WriteLine();

Console.ReadLine();

//using var db = await DataContext.Create()
//    ?? throw new InvalidOperationException("Could not get DataContext from service provider");

//var ids = await db.Set<SimpleItem>().Take(6500).Select(x => x.Id).ToListAsync();

//var query = db.Set<SimpleItem>()
//    .Where(x => ids.Contains(x.Id))
//    .Select(x => new SimpleItem { Name = $"{x.Name} CLONE"});

//await db.AddRangeAsync(query);
//await db.SaveChangesAsync();
