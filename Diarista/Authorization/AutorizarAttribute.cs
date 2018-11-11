using Diarista.Models;
using System.Web;
using System.Web.Mvc;

namespace Diarista.Authorization
{
    public class AutorizarAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            var sessionValue = HttpContext.Current.Session["Usuario"];
            // Check for authorization
            if (sessionValue == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    var user = (User)sessionValue;
                    if (!Roles.Contains(user.TipoUsuario.ToString()))
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                    }
                }
                HttpContext.Current.Session["Usuario"] = sessionValue;
            }
        }

    }
}