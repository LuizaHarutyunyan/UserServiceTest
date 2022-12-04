using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceTest.Model.NewFolder
{
    internal class CommonResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Body { get; set; }
        public string Content { get; set; }
    }
}
