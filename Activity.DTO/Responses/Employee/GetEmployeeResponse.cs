using Employee.DTO.Base.Response;

namespace EmployeeApp.Application.Core.ApplicationContracts.Responses.Example
{
    public class GetEmployeeResponse : GenericResponse
    {
        public GetEmployeeResponse()
        {
            
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
