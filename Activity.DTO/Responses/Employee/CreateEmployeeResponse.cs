using Employee.DTO.Base.Response;

namespace EmployeeApp.Application.Core.ApplicationContracts.Responses.Example
{
    public class CreateEmployeeResponse : GenericResponse
    {
        public CreateEmployeeResponse(int id)
        {
            Id = id;
        }

        public CreateEmployeeResponse()
        {

        }

        public int Id { get; set; }
       
    }
}
