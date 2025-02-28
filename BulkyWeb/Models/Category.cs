using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")] // display name will be use at label on form create
        [MaxLength(30)] // validation server side for max input 30 char
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display Order must be between 1-100" )] // validation server side to only accept range 1 to 100 and custom error msg
        public int DisplayOrder { get; set; }
    }
}
