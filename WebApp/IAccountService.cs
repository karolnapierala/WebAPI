using WebApp.Models;

namespace WebApp
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
    }
}
