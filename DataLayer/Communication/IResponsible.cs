using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Communication
{
    public interface IResponsible
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public object ReturnObject { get; set; }   
        public Exception Exception { get; set; }
    }
}
