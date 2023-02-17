using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WaitLess.DTO.DTOs.Common;
using WaitLess.Service.Common.HTTPClientFactory;
using static WaitLess.DTO.DTOs.Common.Response;

namespace WaitLess.Service.Services.Implementation
{
    public partial class BaseService
    {
        
        protected HttpClient _httpClient;
        public System.Lazy<JsonSerializerSettings> _settings;
        public bool ReadResponseAsString { get; set; }


        protected string _baseUrl;
        protected readonly IEmployeeHttpClientFactory _EmployeeHttpClientFactory;
        protected IConfiguration _configuration;

        public BaseService(IEmployeeHttpClientFactory EmployeeHttpClientFactory,  IConfiguration configuration )
        {
            this._EmployeeHttpClientFactory = EmployeeHttpClientFactory;

            this._configuration = configuration;
            _baseUrl = configuration["URIs:BaseURI"];
            _settings = new System.Lazy<JsonSerializerSettings>(CreateSerializerSettings);
            _httpClient = new HttpClient();
        }

        private JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            //UpdateJsonSerializerSettings(settings);
            return settings;
        }

        public void UpdateJsonSerializerSettings(JsonSerializerSettings settings) { 
        }
        public void PrepareRequest(HttpClient client, HttpRequestMessage request, string url) { }
        public void PrepareRequest(HttpClient client, HttpRequestMessage request, System.Text.StringBuilder urlBuilder) { }
        public void ProcessResponse(HttpClient client, HttpResponseMessage response) { }

        protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = JsonConvert.DeserializeObject<T>(responseText, _settings.Value);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        var serializer = JsonSerializer.Create(_settings.Value);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }


        protected string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return null;
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    return System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                }
            }
            else if (value is bool)
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return (result is null) ? string.Empty : result;
        }
    }
}