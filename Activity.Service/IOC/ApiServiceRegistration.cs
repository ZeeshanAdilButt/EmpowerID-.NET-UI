using Employee.Service.Services;
using Employee.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Service.IOC
{
    public static class ApiServiceRegistration
    {

        /// <summary>
        /// RegisterCommonServiceConnector registers Common Service Connector components.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
