using Employee.DTO.Base.Response;
using EmployeeApp.Application.Core.ApplicationContracts.Requests.Employee;
using EmployeeApp.Application.Core.ApplicationContracts.Requests.Example;
using EmployeeApp.Application.Core.ApplicationContracts.Responses.Example;

namespace Employee.Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<CreateEmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest body);
        //Task<ObjectResponse<DeleteEmployeeResponseDTO>> DeleteEmployeeAsync(string Id, CancellationToken cancellationToken);
        Task<GetAllEmployeeResponse> GetAllEmployeesAsync(GetAllEmployeeRequest body);
        Task<GetEmployeeResponse> GetEmployeeByIdAsync(string id);
        Task<UpdateEmployeeResponse> UpdateEmployeeAsync(UpdateEmployeeRequest body);
        Task<GenericResponse> DeleteEmployeeAsync(DeleteEmployeeRequest body);
    }
}