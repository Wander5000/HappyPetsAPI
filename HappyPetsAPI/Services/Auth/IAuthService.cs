using HappyPetsAPI.DTOs.Auth;

namespace HappyPetsAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<ResponseDTO> LoginAsync(LoginDTO request);
        Task<ResponseDTO> RegisterAsync(RegisterDTO request);
    }
}
