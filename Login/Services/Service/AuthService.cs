using Login.DataBase;
using Login.DTO.Request.User;
using Login.DTO.Result.User;
using Login.Helper.Hasher;
using Login.Models;
using Login.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace Login.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly Context _context;
        private readonly TokenService _tokenService;
        private readonly HasherService _hasherService;
        public AuthService(Context context, TokenService tokenService, HasherService hasherService)
        {
            _context = context;
            _tokenService = tokenService;
            _hasherService = hasherService;
        }
        public async Task<UserDto> Register(NewUserRegisterDto register)
        {
            var result = new UserDto();

            var validations = new Dictionary<string, string>
    {
        { register.FirstName, "El primer nombre está llegando vacío" },
        { register.LastName, "El apellido está llegando vacío" },
        { register.UserName, "El nombre de usuario está llegando vacío" },
        { register.Password, "La contraseña está llegando vacía" },
        { register.Email, "El mail está llegando vacío" }
    };

            foreach (var item in validations)
            {
                if (string.IsNullOrWhiteSpace(item.Key))
                {
                    result.SetError(item.Value, HttpStatusCode.BadRequest);
                    return result;
                }
            }

            register.Password = await _hasherService.Hasher(register.Password);

            var userModel = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName,
                Password = register.Password,
                Email = register.Email
            };
            await _context.AddAsync(userModel);
            await _context.SaveChangesAsync();

            result.User = userModel;
            return result;
        }
        public async Task<UserDto> Login(LoginDto login)
        {
            var result = new UserDto();

            try
            {
                var validation = new Dictionary<string, string>
        {
            { login.UserName, "El nombre de usuario llega vacío." },
            { login.Password, "La contraseña está llegando vacía." }
        };

                foreach (var item in validation)
                {
                    if (string.IsNullOrEmpty(item.Key))
                    {
                        result.SetError(item.Value, HttpStatusCode.BadRequest);
                        return result;
                    }
                }

                var user = await _context.Users
                    .Where(u => u.UserName == login.UserName)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    result.SetError("Usuario no encontrado", HttpStatusCode.NotFound);
                    return result;
                }

                bool isValid = await _hasherService.Verify(login.Password, user.Password);

                if (!isValid)
                {
                    result.SetError("Contraseña incorrecta", HttpStatusCode.Unauthorized);
                    return result;
                }

                string token = _tokenService.GenerateToken(user);

                result.User = user;
                result.Token = token;
                return result;
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, HttpStatusCode.InternalServerError);
                return result;
            }
        }

    }
}
