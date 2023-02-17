using Employee.DTO.Base.Response;
using System;

namespace EmployeeApp.Application.Core.ApplicationContracts.Requests.Employee
{
    public class DeleteEmployeeRequest : BaseRequest
    {
        public int Id { get; set; }

    }
}
