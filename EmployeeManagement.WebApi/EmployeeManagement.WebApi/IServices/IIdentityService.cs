using EmployeeManagement.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.IServices
{
    /// <summary>
    /// Login methods
    /// </summary>
    public interface IIdentityService
    {
        //
        //Task<AuthenticationResult> RegisterAsync(RegisterModel model);
        AuthenticationResult LoginAsync(LoginModel model);
    }
}
