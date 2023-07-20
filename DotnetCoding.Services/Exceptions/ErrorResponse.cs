using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Exceptions
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Title { get; set; }

        public string Exception { get; set; }
        
    }
}
