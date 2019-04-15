using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Entities;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace WebAPI
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                User u = BAL.UserBAL.validateUser(username, password);
                if (u != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, u.login),
                        new Claim("UserID", u.userid.ToString())
                    };
                    ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties() { }));
                }

                else {
                    context.SetError("invalid Grant", "Error");
                }

            });
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null) {
                context.Validated();
            }
            return Task.FromResult < Object > (null);
        }
    }
}