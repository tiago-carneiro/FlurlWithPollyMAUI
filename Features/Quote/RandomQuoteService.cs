namespace FlurlWithPollyMAUI;

public interface IRandomQuoteService
{
    Task<QuoteModel> GetAsync();
}

public class RandomQuoteService : IRandomQuoteService
{
    public Task<QuoteModel> GetAsync()
        => ConfigurationManager.URL
            .AppendPathSegment("/quotes/random")
            .GetJsonAsync<QuoteModel>();
}