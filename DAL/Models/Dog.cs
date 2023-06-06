using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Dog
    {
        [Key]
        [Required]
        [StringLength(200)]
        [RegularExpression(@"([A-Z]){1}([a-z])*")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Color { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int TailLength{ get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Weight { get; set; }
    }
}
