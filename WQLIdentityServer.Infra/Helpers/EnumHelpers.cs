using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentityServer.Infra.Helpers
{
    public class EnumHelpers
    {
        public static List<SelectItemDto> ToSelectList<T>() where T : struct, IComparable
        {
            var selectItems = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new SelectItemDto(Convert.ToInt16(x).ToString(), x.ToString())).ToList();

            return selectItems;
        }
    }
}
