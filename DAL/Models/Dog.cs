using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Dog
    {
        [Key]
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Color { get; set; } = string.Empty;
        [Required]
        //TODO: write working regular expression for TailLength and Weight
        //[RegularExpression(@"/!\D+$/")]
        public int TailLength{ get; set; }
        [Required]
        //[RegularExpression(@"/^\d+$/")]
        public int Weight { get; set; }
    }
}
