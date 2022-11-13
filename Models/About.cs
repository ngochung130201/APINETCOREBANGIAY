using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanGiay.Models
{
    [Table("GioiThieu")]
    public class About
    {
        [Key]
        public Guid Id { get; set; }
        public  string ?Name { get; set; }
        public string ?Content { get; set; }
        public string ?Facebook { get; set; }
        public string ?Intagram { get; set; }
        public string ?Github { get; set; }
        public string ?Image { get; set; }
    }
}
