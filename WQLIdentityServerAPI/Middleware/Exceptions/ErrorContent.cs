using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WQLIdentityServerAPI.Middleware.Exceptions
{
    public class ErrorContent
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }); ;
        }
    }
}
