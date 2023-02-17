using System.Collections.Generic;
using System.Threading.Tasks;
using WaitLess.Core.Application.DTOs.Common;

namespace WaitLess.Service.Common.HTTPClientFactory
{
    public interface IEmployeeHttpClientFactory
    {
        Task<U> PostAsyncReturnsStruct<T, U>(T contentObject, string URI) where U : struct;
        Task<U> PostAsyncReturnsObject<T, U>(T contentObject, string URI, string token = null, bool? checkResponse = true) where U : class;
        Task<T> GetData<T>(string requestUri, string scheme = null, string schemeToken = null, string acceptLangauage = null, List<Dictionary<string, string>> headers = null) where T : class;
        Task<T> GetAsyncReturnsObject<T>(string requestUri, string scheme = null, string schemeToken = null, string acceptLangauage = null, List<Dictionary<string, string>> headers = null) where T : IErrorResponse, new();

        Task<T> DeleteAsyncReturnsObject<T>(string URI, string token = null) where T : class;
        Task<U> PutAsyncReturnsObject<T, U>(T contentObject, string URI, string token = null) where U : class;
    }
}
