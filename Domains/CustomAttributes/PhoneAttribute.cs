using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneAttribute : Attribute
    {
        public int Length { get; set; }

        public PhoneAttribute(int length) 
        {
            Length = length;
        }
    }
}
