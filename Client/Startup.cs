using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication(config => {
                // We check the cookie to confirm that we are authenticated
                config.DefaultAuthenticateScheme = "Cookie";
                // When we sign in we will deal out a cookie
                config.DefaultSignInScheme = "Cookie";
                // use this to check if we are allowed to do something.
                config.DefaultChallengeScheme = "OurServer";
            })
                .AddCookie("Cookie")
                .AddOAuth("OurServer", config => {
                    config.ClientId = "client_id";
                    config.ClientSecret = "client_secret_sdgsdfagdsfgdfsgdfsgdsfgsddvsdvscvxzccxzccsacx";
                    config.CallbackPath = "/oauth_callback";
                    config.TokenEndpoint = "https://localhost:5002/oauth"; //run Token method
                    config.AuthorizationEndpoint = "https://localhost:5002/oauth/authorize";
                    config.SaveTokens = true;
                });

                    services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
