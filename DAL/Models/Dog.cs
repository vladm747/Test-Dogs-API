using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Dog
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public long TailLength{ get; set; }
        public long Weight { get; set; }
    }
}
