using System;
using CESystemApi.Providers;
using CESystemServicesExtensibility.Services;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(UserStore.App_Start.Startup))]

namespace UserStore.App_Start
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        private readonly IUserService userService;

        public Startup(IUserService userService)
        {
            this.userService = userService;
        }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(() => userService);
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(userService),
                AuthorizeEndpointPath = new PathString("/api/User/Authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }
    }
}