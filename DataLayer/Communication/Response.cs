using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Communication
{
    public class Response : IResponsible
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public object ReturnObject { get; set; }
        public Exception Exception { get; set; }

        public Response()
        {

        }

        public Response(string message, bool status)
        {
            Message = message;
            Status = status;
        }

        public Response(string message, bool status, Exception exception)
        {
            Message = message;
            Status = status;
            Exception = exception;
        }

        public Response(string message, bool status, object returnObject)
        {
            Message=message;
            Status = status;
            ReturnObject = returnObject;
        }

        public Response(string message, bool status, object returnObject, Exception exception)
        {
            Message = message;
            Status = status;
            ReturnObject = returnObject;
            Exception = exception;
        }
    }
}
