using Employee.DTO.Base.Response;
using System;

namespace EmployeeApp.Application.Core.ApplicationContracts.Requests.Example
{
    public class GetAllEmployeeRequest : BaseRequest
    {
        public string searchItem { get; set; }

    }
}
