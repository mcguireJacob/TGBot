using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TGBot.Areas.Identity.Data;
using TGBot.Data;

[assembly: HostingStartup(typeof(TGBot.Areas.Identity.IdentityHostingStartup))]
namespace TGBot.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TGBotDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TGBotDbContextConnection")));

                

                services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    
                    

                })
                .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<TGBotDbContext>();

                

            });


        }
    }
}