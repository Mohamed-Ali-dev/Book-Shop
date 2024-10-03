using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Book Name")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [DisplayName("List Price")]
        public double ListPrice { get; set; }
     
        [Required]
        [DisplayName("Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required]
        [DisplayName("Price for more 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [DisplayName("Price for more 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }
        [ForeignKey(("Category"))]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }

    }
}
