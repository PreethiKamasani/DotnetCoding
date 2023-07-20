using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Contracts
{
    public class ProductRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        [Range(0,10000,ErrorMessage ="Price Cannot be more tthan 10000")]
        public decimal Price { get; set; }

        [Required]
        public int UserId { get; set; }      
    }
}
