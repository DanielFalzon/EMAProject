using System;
using EMAProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EMAProject.Areas.Identity.IdentityHostingStartup))]
namespace EMAProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EMAProjectContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<EMAProjectContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

                services.AddControllers(config =>
                {
                    // using Microsoft.AspNetCore.Mvc.Authorization;
                    // using Microsoft.AspNetCore.Authorization;
                    var policy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                });
            });
        }
    }
}