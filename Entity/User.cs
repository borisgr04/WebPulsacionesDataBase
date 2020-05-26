using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
    }
}
