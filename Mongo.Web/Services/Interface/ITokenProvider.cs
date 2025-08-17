namespace Mongo.Web.Services.Interface
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void RemoveToken();
    }
}
