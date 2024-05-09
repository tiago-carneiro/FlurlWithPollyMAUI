namespace FlurlWithPollyMAUI;

public partial class RandomQuotePage
{
    public RandomQuotePage(RandomQuoteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}