using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartup(typeof(WebAPI.Startup))]
namespace WebAPI
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        static Startup() {
            OAuthOptions = new OAuthAuthorizationServerOptions {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                AllowInsecureHttp = true
            };

        }
        public void Configuration(IAppBuilder app) {
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}