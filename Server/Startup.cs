using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            
            services
                .AddAuthentication("OAuth")
                .AddJwtBearer("OAuth",c => 
                c.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secret_words_fdafwfwfwerfdfgasyudgsaudsdsddQDUYd")),
                    ValidIssuer = "https: //localhost:5002",
                    ValidAudience = "https: //localhost:5002",

                }
            );
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
