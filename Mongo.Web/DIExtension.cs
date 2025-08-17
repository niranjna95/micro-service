using Mongo.Web.Services;
using Mongo.Web.Services.Interface;

namespace Mongo.Web
{
    public static class DIExtension
    {

        public static void AddDatabaseDependency(this IServiceCollection services)
        {
            services.AddTransient<ITokenProvider, TokenProvider>();
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<ICouponService, CouponService>();
            services.AddTransient<IAuthService, AuthService>();
        }

        public static void AddServicesDependency(this IServiceCollection services)
        {
         
            services.AddHttpClient<ICouponService, CouponService>();
            services.AddHttpClient<IAuthService, AuthService>();
        }
    }
}
