using Domains.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Item : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [Required]
        [Range(1,1000)]
        public decimal Price { get; set; }
        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }
    }
}
