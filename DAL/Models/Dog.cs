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
        //TODO: write working regular expression for TailLength and Weight
        //[RegularExpression(@"/!\D+$/")]
        public long TailLength{ get; set; }
        [Required]
        //[RegularExpression(@"/^\d+$/")]
        public long Weight { get; set; }
    }
}
