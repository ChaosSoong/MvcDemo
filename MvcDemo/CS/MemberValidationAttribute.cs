using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;

namespace MvcDemo.CS
{
    public class MemberValidationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 判读是否非法跳转（没有登录）
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //获取Cookies中的Login
            var memberValidation = System.Web.HttpContext.Current.Request.Cookies.Get("Login");
            //如果memberValidation为null  或者 memberValidation不等于Success
            if (memberValidation == null || memberValidation.Value != "Success")
            {
                //页面跳转到 登录页面
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }
            //通过验证
            return;
        }
    }
}