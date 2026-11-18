using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class NotFoundException : Exception
    {

            public string CustomMessage { get; set; }
            public NotFoundException(string customMessage) : base()
            {
                CustomMessage = customMessage;
            }
            public NotFoundException(string message, Exception innerException, string customMessage) : base(message, innerException)
            {
                CustomMessage = customMessage;
            }
        }
 
}
