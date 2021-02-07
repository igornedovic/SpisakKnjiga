using System;
using EPOSProjektni.Areas.Identity.Data;
using EPOSProjektni.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EPOSProjektni.Areas.Identity.IdentityHostingStartup))]
namespace EPOSProjektni.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            // kreiranje baze koja ce sadrzati kredencijale korisnika koji se koriste prilikom
            // registracije i prijave
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<User>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                 })
                    .AddEntityFrameworkStores<AuthDbContext>();
            });
        }
    }
}