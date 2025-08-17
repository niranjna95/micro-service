using Mongo.Web.Models;

namespace Mongo.Web.Services.Interface
{
    public interface IBaseService
    {
      Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer=true);
    }
}
