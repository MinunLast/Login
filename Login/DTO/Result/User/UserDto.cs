using Login.Models;

namespace Login.DTO.Result.User
{
    public class UserDto : BaseResult
    {
        public Login.Models.User User { get; set; }
        public string Token { get; set; }

    }
}
