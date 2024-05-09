namespace FlurlWithPollyMAUI;

public abstract partial class BaseViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    bool _isLoading;

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    public void SetLoading(bool value)
        => IsLoading = value;
}
