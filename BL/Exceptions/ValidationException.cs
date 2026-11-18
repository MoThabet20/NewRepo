using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ValidationException : Exception
    {
        public string CustomMessage { get; set; }
        public ValidationException(string customMessage) : base()
        {
            CustomMessage = customMessage;
        }
        public ValidationException(string message,Exception innerException,string customMessage) : base(message,innerException) 
        {
            CustomMessage = customMessage;
        }
    }
}
