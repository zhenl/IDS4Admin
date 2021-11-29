using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skoruba.AuditLogging.EntityFramework.Entities;
using IDS4Admin.Admin.Configuration.Database;
using IDS4Admin.Admin.EntityFramework.Shared.DbContexts;
using IDS4Admin.Admin.EntityFramework.Shared.Entities.Identity;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;
using IDS4Admin.Shared.Dtos;
using IDS4Admin.Shared.Dtos.Identity;

namespace IDS4Admin.Admin
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            HostingEnvironment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Adds the IdentityServer4 Admin UI with custom options.
            services.AddIdentityServer4AdminUI<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext,
            AdminLogDbContext, AdminAuditLogDbContext, AuditLog, IdentityServerDataProtectionDbContext,
                UserIdentity, UserIdentityRole, UserIdentityUserClaim, UserIdentityUserRole,
                UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken, string,
                IdentityUserDto, IdentityRoleDto, IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
                IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
                IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>(ConfigureUIOptions);

            // Add email senders which is currently setup for SendGrid and SMTP
            services.AddEmailSenders(Configuration);

            //20211109
            services.AddSameSiteSupport();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //20211109
            app.UseCookiePolicy();

            //20211124
            app.UsePathBase(Configuration.GetValue<string>("BasePath"));

            app.UseRouting();

            app.UseIdentityServer4AdminUI();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapIdentityServer4AdminUI();
                endpoint.MapIdentityServer4AdminUIHealthChecks();
            });
        }

        public virtual void ConfigureUIOptions(IdentityServer4AdminUIOptions options)
        {
            // Applies configuration from appsettings.
            options.BindConfiguration(Configuration);
            if (HostingEnvironment.IsDevelopment())
            {
                options.Security.UseDeveloperExceptionPage = true;
            }
            else
            {
                options.Security.UseHsts = true;
            }

            // Set migration assembly for application of db migrations
            var migrationsAssembly = MigrationAssemblyConfiguration.GetMigrationAssemblyByProvider(options.DatabaseProvider);
            options.DatabaseMigrations.SetMigrationsAssemblies(migrationsAssembly);

            // Use production DbContexts and auth services.
            options.Testing.IsStaging = false;
        }
    }
}







