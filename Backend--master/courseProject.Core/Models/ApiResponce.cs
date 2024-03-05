using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.Models
{
    public class ApiResponce
    {
        public ApiResponce()
        {
            ErrorMassages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMassages { get; set; }
        public object Result { get; set; }

    }
}
