using System.Web;
using System.Web.Mvc;
using Data.BLL;

namespace Web.Lib
{
    public class LoginControlAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);

            var user = new Base().USER;

            if (user == null)
            {
                filterContext.Result = new RedirectResult("~/?goBack=" + HttpContext.Current.Request.Url.PathAndQuery);
            }
            else
            {
                if (!user.ACTIVE)
                {
                    HttpContext.Current.Session.Abandon();
                    filterContext.Result = new RedirectResult("~/?goBack=" + HttpContext.Current.Request.Url.PathAndQuery);
                }                
            }



        }
    }

    public class HttpHeaderAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public HttpHeaderAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader(Name, Value);
            base.OnResultExecuted(filterContext);
        }
    }
}