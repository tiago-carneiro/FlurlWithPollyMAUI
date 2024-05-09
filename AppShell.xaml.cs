namespace FlurlWithPollyMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	    => InitializeComponent();	

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);
        if (args.Source is ShellNavigationSource.ShellItemChanged or
                        ShellNavigationSource.ShellSectionChanged &&
            CurrentPage?.BindingContext is BaseViewModel baseViewModel)        
            baseViewModel.ApplyQueryAttributes(new Dictionary<string, object>());        
    }
}