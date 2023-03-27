using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.FilterAttributes
{
    public class MyValidator : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = context.Controller as ControllerBase;
            try
            {
                if (!context.ModelState.IsValid)
                {
                    var errors = context.ModelState.Where(x=> x.Value != null).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage) ;
                    context.Result = result.BadRequest(new { message = string.Join("\n ", errors) });
                    return;
                }
                await next();
            }
            catch (Exception ex)
            {
                context.Result = result.BadRequest(new { message = ex.Message });
            }
        }
    }
}
