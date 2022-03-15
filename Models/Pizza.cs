using System.ComponentModel.DataAnnotations;

namespace ContosoPizzaNew.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
