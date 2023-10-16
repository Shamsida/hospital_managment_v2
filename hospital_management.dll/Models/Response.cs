using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hospital_management.DAL.Models
{
    public class Response
    {
        public Response()
        {
            Result = new object(); 
        }
        public HttpStatusCode StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public string? Error { get; set; }
        public bool? Success { get; set; }
        public object Result { get; set; }
    }
}
