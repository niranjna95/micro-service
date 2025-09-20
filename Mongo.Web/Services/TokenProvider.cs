using Mongo.Web.Models.Utility;
using Mongo.Web.Services.Interface;
using Newtonsoft.Json.Linq;

namespace Mongo.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public void RemoveToken()
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }
        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }
        public void SetToken(string token)
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie,token);
        }
    }
}
