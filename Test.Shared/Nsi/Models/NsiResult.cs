using System.Text.Json.Serialization;

namespace Test.Shared.Nsi.Models;

public partial class NsiResult
{
    [JsonPropertyName("data")]
    public Data? Data { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = null!;
}

public partial class Data
{
    [JsonPropertyName("messages")]
    public object[] Messages { get; set; } = [];

    [JsonPropertyName("scroll")]
    public Scroll Scroll { get; set; } = null!;

    [JsonPropertyName("travelOffers")]
    public List<TravelOffer> TravelOffers { get; set; } = [];
}

public partial class Scroll
{
    [JsonPropertyName("earlier")]
    public string? Earlier { get; set; }

    [JsonPropertyName("later")]
    public string? Later { get; set; }

    [JsonPropertyName("searchId")]
    public string? SearchId { get; set; }
}

public partial class TravelOffer
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("itinerary")]
    public Itinerary? Itinerary { get; set; }

    [JsonPropertyName("messages")]
    public Message[] Messages { get; set; } = [];

    [JsonPropertyName("offers")]
    public Offer[] Offers { get; set; } = [];
}

public partial class Itinerary
{
    [JsonPropertyName("bookableStatus")]
    public string? BookableStatus { get; set; }

    [JsonPropertyName("containsRealTimeInfo")]
    public bool ContainsRealTimeInfo { get; set; }

    [JsonPropertyName("delayIndicator")]
    public bool DelayIndicator { get; set; }

    [JsonPropertyName("destination")]
    public Station? Destination { get; set; }

    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("modalities")]
    public Modality[]? Modalities { get; set; }

    [JsonPropertyName("numberOfChanges")]
    public long NumberOfChanges { get; set; }

    [JsonPropertyName("origin")]
    public Station? Origin { get; set; }

    [JsonPropertyName("travel")]
    public object? Travel { get; set; }
}

public partial class Station
{
    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    [JsonPropertyName("arrival")]
    public Arrival? Arrival { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("departure")]
    public Arrival? Departure { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; set; }

    [JsonPropertyName("type")]
    public object? Type { get; set; }
}

public partial class Arrival
{
    [JsonPropertyName("delay")]
    public string? Delay { get; set; }

    [JsonPropertyName("plannedLocalDateTime")]
    public DateTime PlannedLocalDateTime { get; set; }

    [JsonPropertyName("platform")]
    public string? Platform { get; set; }

    [JsonPropertyName("updatedPlatform")]
    public string? UpdatedPlatform { get; set; }
}

public partial class Modality
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }


    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public partial class Message
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("details")]
    public object? Details { get; set; }

    [JsonPropertyName("endDate")]
    public object? EndDate { get; set; }

    [JsonPropertyName("startDate")]
    public object? StartDate { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public partial class Offer
{
    [JsonPropertyName("admissions")]
    public Admission[]? Admissions { get; set; }

    [JsonPropertyName("availability")]
    public string? Availability { get; set; }

    [JsonPropertyName("classLevel")]
    public string? ClassLevel { get; set; }

    [JsonPropertyName("direction")]
    public object? Direction { get; set; }

    [JsonPropertyName("flexLevel")]
    public string? FlexLevel { get; set; }

    [JsonPropertyName("globalReservationStatus")]
    public object? GlobalReservationStatus { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("promoOffer")]
    public bool PromoOffer { get; set; }

    [JsonPropertyName("reductionCardsApplied")]
    public object? ReductionCardsApplied { get; set; }

    [JsonPropertyName("totalPrice")]
    public TotalPrice? TotalPrice { get; set; }

    [JsonPropertyName("totalPriceWithOptionalAccommodations")]
    public TotalPrice? TotalPriceWithOptionalAccommodations { get; set; }

    [JsonPropertyName("travelClassLabels")]
    public object[]? TravelClassLabels { get; set; }
}

public partial class Admission
{
    [JsonPropertyName("appliedReductions")]
    public object[]? AppliedReductions { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("priceDetails")]
    public PriceDetails? PriceDetails { get; set; }

    [JsonPropertyName("pricingMode")]
    public string? PricingMode { get; set; }
}

public partial class PriceDetails
{
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("taxes")]
    public object? Taxes { get; set; }

    [JsonPropertyName("total")]
    public double Total { get; set; }

    [JsonPropertyName("totalFees")]
    public object? TotalFees { get; set; }

    [JsonPropertyName("totalTaxes")]
    public object? TotalTaxes { get; set; }
}

public partial class TotalPrice
{
    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
}