using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using WaitLess.Core.Application.DTOs.Common;

namespace WaitLess.Service.Common.HTTPClientFactory
{
    public class EmployeeHttpClientFactory : IEmployeeHttpClientFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _baseAddress;

        public EmployeeHttpClientFactory(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _baseAddress = configuration["URIs:BaseURI"];
        }

        /// <summary>
        /// GetAsync makes an async GET request to the passed URI
        /// It returns Deserialized object of Type T.
        /// </summary>
        /// <typeparam name="T">Response deserializes to the passed T type.</typeparam>
        /// <param name="requestUri">HTTP Request endpoint url </param>
        /// <param name="scheme">Default null. Used for authorization. </param>
        /// <param name="schemeToken">Default null. Used for authorization.</param>
        /// <param name="acceptLangauage">Http AcceptLanguage header</param>
        /// <returns></returns>
        public async Task<T> GetData<T>(string requestUri, string scheme = "Bearer", string schemeToken = null, string acceptLangauage = null, List<Dictionary<string, string>> headers = null) where T : class
        {
            if (schemeToken == null)
            {
                //schemeToken = await _sessionManager.GetTokenAsync("id_token");
            }

            T data = null;

            using (var client = _clientFactory.CreateClient("EmployeeHttpClientFactory"))
            {
                client.BaseAddress = new Uri(_baseAddress);
                // check if langauage avaialble then add to httpclient request header.
                if (string.IsNullOrEmpty(acceptLangauage) == false)
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLangauage));
                }

                // check if scheme avaialble then add to httpclient request header.
                //if (string.IsNullOrEmpty(scheme) == false && string.IsNullOrEmpty(schemeToken) == false)
                //{
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, schemeToken);
                //}

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Keys.FirstOrDefault(), header[header.Keys.FirstOrDefault()]);
                    }
                }

                try
                {
                    var responseData = await client.GetStringAsync(requestUri);

                    if (!string.IsNullOrWhiteSpace(responseData))
                    {
                        data = JsonConvert.DeserializeObject<T>(responseData);
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return data;
        }

        /// <summary>
        /// GetAsyncReturnsObject makes an async GET request to the passed URI
        /// It returns Deserialized object of Type T.
        /// </summary>
        /// <typeparam name="T">Response deserializes to the passed T type.</typeparam>
        /// <param name="requestUri">HTTP Request endpoint url </param>
        /// <param name="scheme">Default null. Used for authorization. </param>
        /// <param name="schemeToken">Default null. Used for authorization.</param>
        /// <param name="acceptLangauage">Http AcceptLanguage header</param>
        /// <returns></returns>
        public async Task<T> GetAsyncReturnsObject<T>(string requestUri, string scheme = null, string schemeToken = null, string acceptLangauage = null, List<Dictionary<string, string>> headers = null) where T : IErrorResponse, new()
        {
            using (var client = _clientFactory.CreateClient("EmployeeHttpClientFactory"))
            {
                client.BaseAddress = new Uri(_baseAddress);

                // check if langauage avaialble then add to httpclient request header.
                if (string.IsNullOrEmpty(acceptLangauage) == false)
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLangauage));
                }

                //if (schemeToken == null)
                //{
                //    scheme = "Bearer";
                //    schemeToken = await _sessionManager.GetTokenAsync("id_token");
                //}

                // check if scheme avaialble then add to httpclient request header.
                if (string.IsNullOrEmpty(scheme) == false && string.IsNullOrEmpty(schemeToken) == false)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, schemeToken);
                }

                var response = await client.GetAsync(requestUri);

                //TODO: response headers headers
                //var responseHeader = response.Headers;

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(result);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return new T { IsSuccess = false, APIErrors = new List<string> { "Your session has been expired, please login and try again" }, StatusCode = 401 };
                    //throw new UnauthorizedAccessException();
                }
            }

            return default(T);
        }

        /// <summary>
        /// PostAsync makes an async POST request to the passed URI
        /// It deserializes the passed T type contentObject, and returns Deserialized object of Type U
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="contentObject"></param>
        /// <param name="URI"></param>
        /// <param name="token">Default null. Used for authorization. </param>
        /// <returns></returns>
        public async Task<U> PostAsyncReturnsObject<T, U>(T contentObject, string URI, string token = null, bool? checkResponse = true) where U : class
        {
            try
            {
                HttpResponseMessage response;
                using (var client = _clientFactory.CreateClient("EmployeeHttpClientFactory"))
                {
                    //if (token == null)
                    //{
                    //    token = await _sessionManager.GetTokenAsync("id_token");
                    //}
                    // check if Bearer token available then add to httpclient request header.
                    if (string.IsNullOrEmpty(token) == false)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    //if (token == null)
                    //{
                    //    token = await _sessionManager.GetTokenAsync("id_token");
                    //}

                    var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(URI, content);

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic convertedResponse = JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());

                        if (checkResponse != null && checkResponse.Value == false)
                            return convertedResponse;

                        //if (convertedResponse.Message.ToString() != "Record exists in database")

                        return convertedResponse;

                        //convertedResponse.StatusCode = "409";

                        //return convertedResponse;
                    }

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// PostAsync makes an async POST request to the passed URI
        /// It deserializes the passed T type contentObject, and returns Deserialized object of Type U
        /// </summary>
        /// <typeparam name="T"> type of the content object </typeparam>
        /// <typeparam name="U">type of the expected return object </typeparam>
        /// <param name="contentObject"></param>
        /// <param name="URI"></param>
        /// <returns></returns>
        public async Task<U> PostAsyncReturnsStruct<T, U>(T contentObject, string URI) where U : struct
        {
            U defaultResponse = default;

            HttpResponseMessage response;

            using (var client = _clientFactory.CreateClient("EmployeeHttpClientFactory"))
            {
                var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
                response = await client.PostAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            return defaultResponse;
        }

        /// <summary>
        /// PutAsyncReturnsObject makes an async PUT request to the passed URI
        /// It deserializes the passed T type contentObject, and returns Deserialized object of Type U
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="contentObject"></param>
        /// <param name="URI"></param>
        /// <param name="token">Default null. Used for authorization. </param>
        /// <returns></returns>
        public async Task<U> PutAsyncReturnsObject<T, U>(T contentObject, string URI, string token = null) where U : class
        {
            HttpResponseMessage response;
            using (var client = _clientFactory.CreateClient("EmployeeHttpClientFactory"))
            {
                // check if Bearer token available then add to httpclient request header.
                if (string.IsNullOrEmpty(token) == false)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
                response = await client.PutAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            return null;
        }

        /// <summary>
        /// DeleteAsyncReturnsObject makes an async Delete request to the passed URI
        /// It deserializes the passed T type contentObject, and returns Deserialized object of Type U
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="contentObject"></param>
        /// <param name="URI"></param>
        /// <param name="token">Default null. Used for authorization. </param>
        /// <returns></returns>
        public async Task<T> DeleteAsyncReturnsObject<T>(string URI, string token = null) where T : class
        {
            HttpResponseMessage response;
            using (var client = _clientFactory.CreateClient("EmployeeHttpClientFactory"))
            {
                //if (token == null)
                //{
                //    token = await _sessionManager.GetTokenAsync("id_token");
                //}
                // check if Bearer token available then add to httpclient request header.
                if (string.IsNullOrEmpty(token) == false)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                //var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
                response = await client.DeleteAsync(URI);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            return null;
        }


    }
}
