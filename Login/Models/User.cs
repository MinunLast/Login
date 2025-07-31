using System.ComponentModel.DataAnnotations.Schema;

namespace Login.Models
{
    [Table("users")]
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        

        public User()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.Email = string.Empty;
        }
    }
}
