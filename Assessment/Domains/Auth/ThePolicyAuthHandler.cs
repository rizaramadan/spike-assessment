using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Domains.Data;

namespace Assessment.Domains.Auth
{
    public class ThePolicyAuthHandler : AuthorizationHandler<ThePolicyRequirement>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Guid Guid;

        public ThePolicyAuthHandler(AppDbContext c, IHttpContextAccessor ca)
        {
            _context = c;
            _contextAccessor = ca;
            Guid = Guid.NewGuid();
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ThePolicyRequirement req)
        {
            if (context.Resource is AuthorizationFilterContext filterContext)
            {
                var area = (filterContext.RouteData.Values["area"] as string)?.ToLower();
                var controller = (filterContext.RouteData.Values["controller"] as string)?.ToLower();
                var action = (filterContext.RouteData.Values["action"] as string)?.ToLower();
                var id = (filterContext.RouteData.Values["id"] as string)?.ToLower();
                if (await req.Pass(_context, _contextAccessor, area, controller, action, id))
                {
                    context.Succeed(req);
                }
            }
            if (context.Resource is DefaultHttpContext httpContext && httpContext is not null)
            {
                var area = httpContext.Request.RouteValues["area"]?.ToString();
                var controller = httpContext.Request.RouteValues["controller"].ToString();
                var action = httpContext.Request.RouteValues["action"].ToString();
                var id = httpContext.Request.RouteValues["id"]?.ToString();
                if (await req.Pass(_context, _contextAccessor, area, controller, action, id))
                {
                    context.Succeed(req);
                }
            }
            if (context.Resource is Record r) 
            {
                if (await req.Pass(_context, _contextAccessor, r))
                {
                    context.Succeed(req);
                }
            }
        }
    }
}
