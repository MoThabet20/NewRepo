using Domains.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
