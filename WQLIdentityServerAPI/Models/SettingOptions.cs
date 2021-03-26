using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Models
{
    public class SettingOptions
    {
        public const string Name = "Settings";

        public string DatabaseType { get; set; }

        public string SqlServerConnection { get; set; }


        public string MySqlConnection { get; set; }

        public bool UseMinProfiler { get; set; }
    }
}
