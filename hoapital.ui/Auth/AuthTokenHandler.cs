using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;

namespace hoapital.ui.Auth
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ProtectedSessionStorage sessionStorage;
        public AuthTokenHandler(ProtectedSessionStorage sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await sessionStorage.GetAsync<string>("authToken");
            
            if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.Value);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
