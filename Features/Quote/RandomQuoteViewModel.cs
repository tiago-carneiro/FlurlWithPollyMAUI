
namespace FlurlWithPollyMAUI;

public partial class RandomQuoteViewModel : BaseViewModel
{
    readonly IRandomQuoteService _randomQuoteService;

    [ObservableProperty]
    QuoteModel _item;

    public RandomQuoteViewModel(IRandomQuoteService randomQuoteService)
        => _randomQuoteService = randomQuoteService;

    public override async void ApplyQueryAttributes(IDictionary<string, object> query)
        => await LoadDataAsync();

    async Task LoadDataAsync()
    {
        var result = await _randomQuoteService.GetAsync().Handle(this);
        if (result.Success)
            Item = result.Data;
    }

    [RelayCommand]
    Task OnUpdateAsync()
        => LoadDataAsync();
}
