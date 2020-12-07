using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Chapter07_Samples
{
    public class MyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IHttpContextAccessor httpContextAccessor;

        public MyAuthenticationHandler(
                IHttpContextAccessor httpContextAccessor,
                IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var password =
                    httpContextAccessor.HttpContext.Request.Query["password"].ToString();
                if (password == "admin")
                {
                    ClaimsIdentity identity = new ClaimsIdentity();
                    identity.AddClaim(new Claim(ClaimTypes.Name, "admin"));
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    AuthenticationTicket ticket =
                        new AuthenticationTicket(principal, Scheme.Name);
                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    return Task.FromResult(AuthenticateResult.NoResult());
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "身份认证发生错误");
                return Task.FromResult(AuthenticateResult.Fail(ex));
            }
        }
    }
}
