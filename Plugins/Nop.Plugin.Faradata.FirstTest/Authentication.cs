using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Plugin.Faradata.FirstTest.Services;
using Nop.Services.Customers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Faradata.FirstTest
{

    public class AuthenticationFilterAttribute : TypeFilterAttribute
    {

        public AuthenticationFilterAttribute() : base(typeof(AuthenticationFilter))
        {
        }

        public class AuthenticationFilter : IAuthorizationFilter
        {
            private readonly ICustomerService _customerService;
            private readonly ICustomerTokenService _customerTokenService;

            public AuthenticationFilter(ICustomerService customerService, ICustomerTokenService customerTokenService)
            {
                _customerService = customerService;
                _customerTokenService = customerTokenService;
            }

            #region old
            //private readonly string _claimType;
            //private readonly string _claimValue;

            //public AuthenticationFilter(/*IApplicationUserManager user,string claimeType, string claimValue, IOptionsSnapshot<SiteSettings> adminUserSeedOptions*/)
            //{
            //    //_user = user;
            //    //_claimType = claimeType;
            //    //_claimValue = claimValue;
            //    //_adminUserSeedOptions = adminUserSeedOptions;
            //}

            //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            //{
            //    // try
            //    //{
            //    var param = context.ActionArguments[_claimValue];
            //    // var y = context.RouteData.Values[_claimValue];
            //    if (param == default(object))
            //    {
            //        // startegy ???
            //        //context.Result = new BadRequestResult();
            //        context.Result = new ForbidResult();
            //        return;
            //    }
            //    //custome area
            //    var serialNumberClaim = context?.HttpContext?.User?
            //                        .FindFirst(ClaimTypes.SerialNumber)?.Value;
            //    if (serialNumberClaim == null)
            //    {
            //        context.Result = new ForbidResult();
            //        return;
            //    }

            //    //var user = await _user.FindBySerialNumber(serialNumberClaim);
            //    //// if superadmin
            //    //if (user.UserName == _adminUserSeedOptions.Value.AdminUserSeed.Username)
            //    //{
            //    //    await next();
            //    //    return;
            //    //}

            //    //var userClaim = await _user.GetClaimsAsync(user);
            //    //var claimValue = param.ToString();
            //    //var existUserClaim = userClaim.ToList().Any(x =>
            //    //                    (x.Type).ToLower() == (_claimType).ToLower()
            //    //                    && (x.Value).ToLower() == (claimValue).ToLower());
            //    //if (!existUserClaim)
            //    //{
            //    //    context.Result = new ForbidResult();
            //    //    return;
            //    //}

            //    await next();
            //} 
            #endregion

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString();
                token = token.Length > 0 ? token.Substring(6) : "";
                var user =_customerTokenService.GetByToken(token);
                if (user == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}


