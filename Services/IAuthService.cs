using warehouse_management_api.DTOs.Auth;

namespace warehouse_management_api.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}