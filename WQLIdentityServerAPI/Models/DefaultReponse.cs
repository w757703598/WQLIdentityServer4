using System.Collections.Generic;

namespace WQLIdentityServerAPI.Models
{
    public class DefaultReponse<T>
    {
        public bool Result { get; set; }
        public T Data { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}
