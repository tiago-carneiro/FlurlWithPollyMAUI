namespace FlurlWithPollyMAUI;

public record QuoteModel
{
    public int Id { get; set; }
    public string Author { get; set; }

    //Flurl 3.x
    //[JsonProperty("quote")]

    //Flurl 4.x
    [JsonPropertyName("quote")]
    public string Text { get; set; }
}