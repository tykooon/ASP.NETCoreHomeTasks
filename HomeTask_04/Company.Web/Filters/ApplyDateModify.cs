using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Company.Web.Filters;

public class ApplyDateModify : Attribute, IPageFilter
{
    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    { }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    { }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        if (context?.HandlerMethod.MethodInfo.Name == "OnPost")
        {
            var form = new Dictionary<string, StringValues>(context.HttpContext.Request.Form)
            {
                ["Candidate.StartDate"] = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd")
            };
            context.HttpContext.Request.Form = new FormCollection(form);
        }
    }
}
