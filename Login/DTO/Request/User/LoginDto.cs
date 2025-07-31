namespace Login.DTO.Request.User
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginDto()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
        }
    }
}
