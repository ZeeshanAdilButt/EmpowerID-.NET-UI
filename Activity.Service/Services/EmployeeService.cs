using Employee.DTO.Base.Response;
using Employee.Service.Services.Interfaces;
using EmployeeApp.Application.Core.ApplicationContracts.Requests.Employee;
using EmployeeApp.Application.Core.ApplicationContracts.Requests.Example;
using EmployeeApp.Application.Core.ApplicationContracts.Responses.Example;
using Microsoft.Extensions.Configuration;
using WaitLess.Service.Common.HTTPClientFactory;
using WaitLess.Service.Services.Implementation;

namespace Employee.Service.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        string _baseAddress;
        public EmployeeService(IEmployeeHttpClientFactory EmployeeHttpClientFactory, IConfiguration configuration) : base(EmployeeHttpClientFactory, configuration)
        {
        }

        public async Task<CreateEmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest body)
        {
            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(_baseUrl != null ? _baseUrl.TrimEnd('/') : "").Append("/api/Employee");

            var response = await _EmployeeHttpClientFactory.PostAsyncReturnsObject<CreateEmployeeRequest, CreateEmployeeResponse>(body, urlBuilder.ToString());

            return response;
        }

        public async Task<GetEmployeeResponse> GetEmployeeByIdAsync(string id)
        {
            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(_baseUrl != null ? _baseUrl.TrimEnd('/') : "").Append("/api/Employee/{Id}");
            urlBuilder.Replace("{Id}", Uri.EscapeDataString(ConvertToString(id, System.Globalization.CultureInfo.InvariantCulture)));

            var response = await _EmployeeHttpClientFactory.GetAsyncReturnsObject<GetEmployeeResponse>(urlBuilder.ToString());

            return response;
        }

        public async Task<GetAllEmployeeResponse> GetAllEmployeesAsync(GetAllEmployeeRequest body)
        {
            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(_baseUrl != null ? _baseUrl.TrimEnd('/') : "").Append("/api/Employee?");

            if (body.searchItem != null)
            {
                urlBuilder.Append(Uri.EscapeDataString("searchItem") + "=").Append(Uri.EscapeDataString(body.searchItem));
            }

            urlBuilder.Length--;

            var response = await _EmployeeHttpClientFactory.GetAsyncReturnsObject<GetAllEmployeeResponse>(urlBuilder.ToString());

            return response;
        }

        public async Task<UpdateEmployeeResponse> UpdateEmployeeAsync(UpdateEmployeeRequest body)
        {
            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(_baseUrl != null ? _baseUrl.TrimEnd('/') : "").Append("/api/Employee");

            var response = await _EmployeeHttpClientFactory.PutAsyncReturnsObject<UpdateEmployeeRequest, UpdateEmployeeResponse>(body, urlBuilder.ToString());

            return response;
        }

        public async Task<GenericResponse> DeleteEmployeeAsync(DeleteEmployeeRequest body)
        {
            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(_baseUrl != null ? _baseUrl.TrimEnd('/') : "").Append("/api/Employee/{Id}");
            urlBuilder.Replace("{Id}", Uri.EscapeDataString(ConvertToString(body.Id, System.Globalization.CultureInfo.InvariantCulture)));

            var response = await _EmployeeHttpClientFactory.DeleteAsyncReturnsObject<GetEmployeeResponse>(urlBuilder.ToString());

            return response;
        }
    }
}
