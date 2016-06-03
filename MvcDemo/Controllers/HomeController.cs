using MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private htgisEntities1 db = new htgisEntities1();
        /// <summary>
        /// 登录加载
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //将cookie名为Login的值赋值NO，证明没有登录过系统这样跳转验证时判读期值
            var cookie = new HttpCookie("Login", "No");
            System.Web.HttpContext.Current.Response.SetCookie(cookie); 
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="UserCode">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns>成功跳转到学生信息页面，没有成功提示重新登录</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string UserCode,string PassWord)
        {
            //查询用户表
            var users = from Users in db.Users
                        where
                          Users.UserCode == UserCode &&
                          Users.PassWord == PassWord
                        select Users;
            //判断是否有此用户
            if(users.Count()>0)
            {
                //将cookie名为Login的值赋值Success，证明登录成功。
                var cookie = new HttpCookie("Login", "Success");
                System.Web.HttpContext.Current.Response.SetCookie(cookie);  
                //跳转学生信息
                return RedirectToAction("Index", "StudentInformation");
            }
            //return Content("用户名或密码错误！");
            //返回登录失败
            ViewBag.Err = "用户名或密码错误";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
