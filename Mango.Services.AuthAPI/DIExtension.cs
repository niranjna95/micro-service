using Mango.Services.AuthAPI.Service;
using Mango.Services.AuthAPI.Service.IService;

namespace Mango.Services.AuthAPI
{
    public static class DIExtension
    {
        public static void AddDatabaseDependency(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
           
            
        }
    }
}
