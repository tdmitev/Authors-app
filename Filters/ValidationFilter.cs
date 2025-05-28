using Microsoft.AspNetCore.Mvc.Filters;
using  SportScore2.Api.Exception;

namespace SportScore2.Api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(kvp => kvp.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            throw new ValidationException(errors);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
