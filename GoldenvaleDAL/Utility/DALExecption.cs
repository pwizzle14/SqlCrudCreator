using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldenvaleDAL.Utility
{
    public class DALExecption : Exception
    {
        public DALExecption()
        {
        }

        public DALExecption(string? message) : base(message)
        {
        }

        public DALExecption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DALExecption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
