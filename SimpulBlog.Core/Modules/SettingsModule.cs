using Autofac;
using Microsoft.Extensions.Configuration;
using SimpulBlog.Core.Settings;

namespace SimpulBlog.Core.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var jwtSettings = _configuration.GetSection(typeof(JwtSettings).Name).Get<JwtSettings>();
            builder.Register(p => jwtSettings).SingleInstance();

            base.Load(builder);
        }
    }
}
