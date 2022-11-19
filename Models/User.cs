

using System.ComponentModel.DataAnnotations;

namespace BanGiay.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHast { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }

    }
}
