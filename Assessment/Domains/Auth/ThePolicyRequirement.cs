using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Domains.Data;

namespace Assessment.Domains.Auth
{
    public class ThePolicyRequirement : IAuthorizationRequirement
    {
        public const string ThePolicy = nameof(ThePolicy);

        AppDbContext _context;
        IHttpContextAccessor _contextAccessor;

        public async Task<bool> Pass(AppDbContext context, IHttpContextAccessor contextAccessor, string area, string controller, string action, string id)
        {
            _context = context;
            _contextAccessor = contextAccessor;

            //authorization logic goes here
            var isAuth = _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;
            return await Task.FromResult( isAuth.HasValue && isAuth.Value ); 
        }

        public async Task<bool> Pass(AppDbContext context, IHttpContextAccessor contextAccessor, Record r)
        {
            _context = context;
            _contextAccessor = contextAccessor;

            //authorization logic goes here
            var isAuth = _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;
            return await Task.FromResult(isAuth.HasValue && isAuth.Value);
        }
    }
}
