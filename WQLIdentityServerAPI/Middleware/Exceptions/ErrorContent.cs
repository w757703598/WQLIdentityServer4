using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Middleware.Exceptions
{
    public class ErrorContent
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this,new JsonSerializerSettings { ContractResolver=new CamelCasePropertyNamesContractResolver()}); ;
        }
    }
}
