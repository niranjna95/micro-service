using Mongo.Web.Models;
using Mongo.Web.Models.Utility;
using Mongo.Web.Services.Interface;
using static Mongo.Web.Models.Utility.SD;

namespace Mongo.Web.Services
{
    public class AuthService:IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            this._baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegisterationRequestModel model)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            }, withBearer:false);
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestModel model)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/Login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegisterationRequestModel model)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/Register"
            }, withBearer: false);
        }
    }
}
