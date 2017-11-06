using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public class Response
    {
        public Response() { }
        public bool isSuccess { get; set; }
        public Object data { get; set; }
        public string message { get; set; }
    }
}
