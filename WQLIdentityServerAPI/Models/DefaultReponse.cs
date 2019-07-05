using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Models
{
    public class DefaultReponse<T>
    {
        public bool Result { get; set; }
        public T Data { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}
