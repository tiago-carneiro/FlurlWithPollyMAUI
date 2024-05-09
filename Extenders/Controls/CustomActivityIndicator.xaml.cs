namespace FlurlWithPollyMAUI;

public partial class CustomActivityIndicator
{
    public static BindableProperty IsActiveProperty =
       BindableProperty.Create(
           nameof(IsActive),
           typeof(bool),
           typeof(CustomActivityIndicator),
           defaultValue: false,
           defaultBindingMode: BindingMode.OneWay,
           propertyChanged: IsActiveChanged);

    public bool IsActive
    {
        get { return (bool)GetValue(IsActiveProperty); }
        set { SetValue(IsActiveProperty, value); }
    }

    public CustomActivityIndicator()
    {
        InitializeComponent();
        IsVisible = false;
    }

    private static void IsActiveChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomActivityIndicator element &&
            newValue is bool value)
            element.IsVisible = element.activityIndicator.IsRunning = value;
    }
}