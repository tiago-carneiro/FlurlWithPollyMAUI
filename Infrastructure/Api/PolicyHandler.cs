using System.Diagnostics;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace FlurlWithPollyMAUI;

public static class Policies
{
    static AsyncTimeoutPolicy<HttpResponseMessage> TimeoutPolicy =>
        Policy.TimeoutAsync<HttpResponseMessage>(30, (context, timeSpan, task) =>
        {
            Debug.WriteLine($"[App|Policy]: Timeout delegate fired after {timeSpan.Seconds} seconds");
            return Task.CompletedTask;
        });

    static AsyncRetryPolicy<HttpResponseMessage> RetryPolicy =>
        Policy.HandleResult<HttpResponseMessage>(r => r is
        {
            IsSuccessStatusCode: false,
            StatusCode: not System.Net.HttpStatusCode.Unauthorized
        })
        .Or<TimeoutRejectedException>()
        .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(7)
            },
            (delegateResult, retryCount) =>
            {
                Debug.WriteLine(
                    $"[App|Policy]: Retry delegate fired, attempt {retryCount}");
            });

    public static AsyncPolicyWrap<HttpResponseMessage> PolicyStrategy =>
         Policy.WrapAsync(RetryPolicy, TimeoutPolicy);

    public static void ApplyHttpPolicies()
    {
        //Flurl 3.x
        //FlurlHttp.GlobalSettings.HttpClientFactory = new PollyHttpClientFactory();

        //Flurl 4.x
        FlurlHttp.Clients.WithDefaults(builder => builder
            .AddMiddleware(() => new PolicyHandler()));
    }
}


//Flurl 3.x
/*
public class PollyHttpClientFactory : DefaultHttpClientFactory
{
    public override HttpMessageHandler CreateMessageHandler()
    {
        var pollyHandler = new PolicyHandler
        {
            InnerHandler = base.CreateMessageHandler(),
        };

        return pollyHandler;
    }
}
*/

public class PolicyHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        => Policies.PolicyStrategy.ExecuteAsync(ct => base.SendAsync(request, ct), cancellationToken);
}