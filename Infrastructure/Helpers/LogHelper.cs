namespace FlurlWithPollyMAUI;

public static class LogHelper
{
    public static void Log(string TAG, Exception ex)
        => Log(TAG, ConcactException(ex));

    static string ConcactException(Exception ex, StringBuilder str = null)
    {
        if (str == null)
            str = new StringBuilder();

        str.AppendLine($"Message: {ex.Message}");
        str.AppendLine($"StackTrace: {ex.StackTrace}");

        if (ex.InnerException != null)
            str.AppendLine(ConcactException(ex.InnerException, str));

        return str.ToString();
    }

    public static void Log(string TAG, string msg)
    {
#if DEBUG
        Console.WriteLine($"[{TAG}] {msg}");
#endif
    }
}