using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCrudCreatorCore.Utilites
{
    
    public class SqlCrudCreatorExecption : Exception
        {
            public SqlCrudCreatorExecption() { }
            public SqlCrudCreatorExecption(string message) : base(message) { }
            public SqlCrudCreatorExecption(string message, Exception inner) : base(message, inner) { }
            protected SqlCrudCreatorExecption(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

}
