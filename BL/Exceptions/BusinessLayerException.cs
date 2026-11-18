using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BusinessLayerException: Exception
    {
        public BusinessLayerException() :base()
        {
            
        }

        public BusinessLayerException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
