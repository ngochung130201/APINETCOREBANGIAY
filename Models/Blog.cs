using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanGiay.Models
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string SLug { get; set; }
        public string Gallery { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public bool isStatus { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
