using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace hoapital.ui.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage sessionStorage;
        private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        #region // Constructor
        public CustomAuthStateProvider(ProtectedSessionStorage sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }
        #endregion


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var result = await sessionStorage.GetAsync<string>("authToken");
                var token = result.Success ? result.Value : null;

                if(string.IsNullOrWhiteSpace(token))
                {
                    return new AuthenticationState(anonymous);
                }

                var claims = ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "jwt");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {
                return new AuthenticationState(anonymous);
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await sessionStorage.SetAsync("authToken", token);

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await sessionStorage.DeleteAsync("authToken");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }


        private static List<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims.ToList();
        }
    }
}
