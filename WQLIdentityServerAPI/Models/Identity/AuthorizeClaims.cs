using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Models.Identity
{
    public class AuthorizeClaims
    {
        public static List<string> GetClaimTypes
        {
            get
            {
                return new List<string> { Area, Permission };
            }
        }

        public static List<string> GetAreaValue
        {
            get
            {
                return new List<string> { PartDesin, Simulation, ProductFault, Config, Image, Log };
            }
        }

        public static List<string> GetPermissionValue
        {
            get
            {
                return new List<string> { Create, Read, Update, Delete };
            }
        }

        ///Type
        public const string Area = "Area";
        public const string Permission = "Permission";

        //PermissionValue
        public const string Create = "Create";
        public const string Read = "Read";
        public const string Update = "Update";
        public const string Delete = "Delete";

        //AreaValue
        public const string PartDesin = "PartDesin";
        public const string Simulation = "Simulation";
        public const string ProductFault = "ProductFault";
        public const string Config = "Config";
        public const string Image = "Image";
        public const string Log = "Log";
    }
}
