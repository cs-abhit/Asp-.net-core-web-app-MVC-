using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asp_.net_core_web_app__MVC_.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]    //to see in the client side 
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display order must be between 1-100.")]
        public int DisplayOrder { get; set; }
    }
}
