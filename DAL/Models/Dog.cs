using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Dog
    {
        [Key]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Color { get; set; }
        [Required]
        [RegularExpression(@"/^\d+$/", ErrorMessage = "Tail height is a negative number or is not a number")]
        public long TailLength{ get; set; }
        [Required]
        [RegularExpression(@"/^\d+$/", ErrorMessage = "Weight is a negative number or is not a number")]
        public long Weight { get; set; }
    }
}
