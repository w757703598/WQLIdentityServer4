using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace WQLIdentityServerAPI.Filters
{
    public class PermissionAuthorize
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
        public class PermissionAuthorzize : Attribute, IAuthorizationFilter
        {
            //权限名称
            public string Permission { get; set; }
            //作用域
            public string Area { get; set; }
            public void OnAuthorization(AuthorizationFilterContext context)
            {

                if (context.HttpContext.User != null)
                {
                    var AreaClaim = context.HttpContext.User.FindAll(_ => _.Type == Area);
                    //查找是否包含该域
                    if (AreaClaim != null)
                    {
                        var AreaPermission = AreaClaim.Select(d => d.Value);
                        //查找在该域是否具有权限
                        if (AreaPermission == null || !AreaPermission.Contains(Permission))
                        {
                            context.Result = new UnauthorizedResult();
                        }
                    }

                }


            }
        }
    }
}
