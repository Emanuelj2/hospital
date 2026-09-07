using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace hoapital.ui.Auth
{
    // Reads the JWT from protected session storage so components can attach it to outgoing
    // API calls themselves. This must be injected directly into a component and called from
    // an event handler or OnAfterRenderAsync — NOT from OnInitializedAsync, and NOT from a
    // DelegatingHandler resolved through IHttpClientFactory's own internal handler-building
    // scope. Neither of those has a working JS interop channel in Blazor Server, so
    // ProtectedSessionStorage always throws there regardless of handler pooling settings.
    public class AuthTokenProvider
    {
        private readonly ProtectedSessionStorage sessionStorage;

        public AuthTokenProvider(ProtectedSessionStorage sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }

        public async Task<string?> GetTokenAsync()
        {
            var result = await sessionStorage.GetAsync<string>("authToken");
            return result.Success ? result.Value : null;
        }
    }
}
