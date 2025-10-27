using Microsoft.AspNetCore.Authentication;

public class AuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
    {
        var accessToken = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

        if (!string.IsNullOrEmpty(accessToken))
        {
            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await base.SendAsync(httpRequestMessage, cancellationToken);
    }
}