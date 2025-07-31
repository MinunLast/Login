using Login.DTO.Request.User;
using Login.DTO.Result;
using Login.DTO.Result.User;

namespace Login.Services.Interface
{
    public interface IAuthService
    {
        Task<UserDto> Register(NewUserRegisterDto register);
        Task<UserDto> Login(LoginDto login);
    }
}
