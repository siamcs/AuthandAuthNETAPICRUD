using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Practice7.Provider;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(Practice7.Startup))]

namespace Practice7
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan=TimeSpan.FromMinutes(50),
                Provider=new ServerProvider()


            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);

        }
    }
}
