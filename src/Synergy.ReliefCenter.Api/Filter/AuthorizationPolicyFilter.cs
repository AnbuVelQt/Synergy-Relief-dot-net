using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Synergy.ReliefCenter.Services.Abstraction;
using System;

namespace Synergy.ReliefCenter.Api.Filter
{
    public class AuthorizationPolicyFilter : IAuthorizationFilter
    {
        public string _policy;
        
        public AuthorizationPolicyFilter(string policy)
        {
            _policy = policy;
           
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
               
                //string token = context.HttpContext.Request.Headers["Authorization"];
                
                    if (IsAuthorized(context, _policy))
                    {
                        return;
                    }
                
                ReturnUnauthorizedResult(context);
            }
            catch (FormatException)
            {
                ReturnUnauthorizedResult(context);
            }
        }
        public bool IsAuthorized(AuthorizationFilterContext context, string policy)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationPolicyService>();
            return authService.Validate( policy);
           
        }
        private void ReturnUnauthorizedResult(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
