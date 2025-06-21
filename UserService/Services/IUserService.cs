namespace UserService.API.Services
{
    public interface IUserService
    {
        bool Register(string username, string phone, string login, string password);
    }
}
