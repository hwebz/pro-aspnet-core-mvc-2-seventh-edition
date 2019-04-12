using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class ViewResultDiagnostics : IActionFilter
    {
        private IFilterDiagnostics diagnostics;

        public ViewResultDiagnostics(IFilterDiagnostics diags) => diagnostics = diags;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            ViewResult vr;
            if ((vr = context.Result as ViewResult) != null)
            {
                diagnostics.AddMessage($"View name: {vr.ViewName}");
                diagnostics.AddMessage($@"Model Type: {vr.ViewData.Model.GetType().Name}");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
