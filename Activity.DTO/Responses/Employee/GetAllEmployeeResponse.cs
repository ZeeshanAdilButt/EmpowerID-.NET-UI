using Employee.DTO.Base.Response;

namespace EmployeeApp.Application.Core.ApplicationContracts.Responses.Example
{
    public class GetAllEmployeeResponse : GenericResponse
    {
        public List<GetEmployeeResponse> Data;
    }
}
