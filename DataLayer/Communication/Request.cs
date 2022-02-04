using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Communication
{
    public class Request
    {
        public Route Route { get; set; }
        public object Data { get; set; }
    }
}
