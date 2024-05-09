namespace FlurlWithPollyMAUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Policies.ApplyHttpPolicies();
    }
}