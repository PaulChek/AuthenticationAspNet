using EmailVerification.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailVerification {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            #region Login decration with email virification!
            services.AddDbContext<AuthDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Sql")));

            services.AddIdentity<IdentityUser, IdentityRole>(c =>
            c.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(o => {
                o.LoginPath = "/login";
                o.Cookie.Name = "AuthT";
            });

            services.AddMailKit(c => {
                var options = Configuration.GetSection("Email").Get<MailKitOptions>();
                c.UseMailKit(options);
            });

            #endregion
            //Policy
            services.AddAuthorization(c => {
                c.AddPolicy("sgsfg", policy => {
                    policy.RequireClaim(ClaimTypes.Name);
                });
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
