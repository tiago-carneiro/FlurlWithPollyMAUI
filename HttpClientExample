# How to use with HttpClient

This is an example to use the [Policy.cs](https://github.com/tiago-carneiro/FlurlWithPollyMAUI/blob/main/Infrastructure/Api/PolicyHandler.cs) with HttpClient:

**Create the HTTP Message Handler**

    public class PolicyHandler: DelegatingHandler
    {
         public PolicyHandler(HttpClientHandler  handler) : base(handler) { }
                      
         protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken  cancellationToken) 
              => Policies.PolicyStrategy.ExecuteAsync(() =>  base.SendAsync(request, cancellationToken));
    }

**Add it to HttpClient.**

    _httpClient  =  new  HttpClient(new  PolicyHandler(new  HttpClientHandler()));

