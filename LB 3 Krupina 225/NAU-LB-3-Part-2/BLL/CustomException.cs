using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    internal class CustomException: System.Exception
    {
        public CustomException() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, CustomException inner) : base(message, inner) { }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}

/*
 using System;
using System.Runtime.Serialization;

namespace DALWorckWithDataBases
{
    [Serializable]
    internal class ExceptionFielNotFound : Exception
    {
        public ExceptionFielNotFound()
        {
        }

        public ExceptionFielNotFound(string message) : base(message)
        {
        }

        public ExceptionFielNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExceptionFielNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
 */