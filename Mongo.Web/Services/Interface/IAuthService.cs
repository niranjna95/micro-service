using Mongo.Web.Models;

namespace Mongo.Web.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestModel model);
        Task<ResponseDto?> RegisterAsync(RegisterationRequestModel model);
        Task<ResponseDto?> AssignRoleAsync(RegisterationRequestModel model);
    }
}
