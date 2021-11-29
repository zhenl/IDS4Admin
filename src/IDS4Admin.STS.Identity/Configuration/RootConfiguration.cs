using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;
using IDS4Admin.STS.Identity.Configuration.Interfaces;

namespace IDS4Admin.STS.Identity.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {      
        public AdminConfiguration AdminConfiguration { get; } = new AdminConfiguration();
        public RegisterConfiguration RegisterConfiguration { get; } = new RegisterConfiguration();
    }
}







