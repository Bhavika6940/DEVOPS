using System;
using System.ComponentModel.DataAnnotations;
namespace ProductService.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
public decimal Price { get; set; }
        [Required(ErrorMessage = "Units is required")]
        [StringLength(20)]
        public string Units { get; set; }
        [Url(ErrorMessage = "Please provide a valid URL for the picture")]
        public string Picture { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Units in stock must be 0 or greater")]
public int UnitsInStock { get; set; }
    }
}

