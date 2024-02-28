using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StoreApp.Models.Authentication;

public class Authentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //  kiểm tra nếu Session nếu null thì bắt vào trang login
        if(context.HttpContext.Session.GetString("UserName") == null) {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                    {"Controller", "Access"},
                    {"Action", "Login"}
                }
            );
        }
    }
}