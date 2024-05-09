using CommunityToolkit.Maui.Alerts;

namespace FlurlWithPollyMAUI;

public static class TaskExtensions
{
    public const string NoInternetConnection = "Something went wrong with the internet connection, please try again later.";
    public const string GenericError = "Something went wrong, please try again later";

    public static async Task<(bool Success, T Data)> Handle<T>(this Task<T> self, BaseViewModel vm = null)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            ShowToast(NoInternetConnection);
            return (false, default(T));
        }

        vm?.SetLoading(true);

        try
        {
            var result = await self.ConfigureAwait(false);
            return (true, result);
        }
        catch (FlurlHttpException flurlEx)
        {
            HandleException(flurlEx);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            vm?.SetLoading(false);
        }

        return (false, default(T));
    }

    static void HandleException(Exception ex)
    {
        LogHelper.Log(nameof(TaskExtensions), ex);
        ShowToast(GenericError);
    }

    static void ShowToast(string message)
        => MainThread.BeginInvokeOnMainThread(() => Toast.Make(message).Show());
}