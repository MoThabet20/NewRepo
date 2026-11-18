using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessException : Exception
    {
        public string CustomMessage { get; set; }
        public DataAccessException(string custommessage):base()
        {
            CustomMessage = custommessage;
        }
        public DataAccessException(string message, string custommessage) : base(message)
        {
            CustomMessage = custommessage;
        }
        public DataAccessException(string message, Exception innerException, string custommessage) : base(message, innerException)
        {
            CustomMessage = custommessage;
        }
        public DataAccessException(string message, Exception innerException) : base(message, innerException)
        {
           
        }
    }
}
