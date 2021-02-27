using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Common
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
       
        public bool Success { get; set; }
    }
}
