using MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;


namespace MvcDemo.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        private htgisEntities1 db = new htgisEntities1();
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        /// <summary>
        /// 创建用户，因为需要输入确认密码，所以新建一个非数据库实体Register加入确认密码
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Register register = new Register();
            register.Sex = "1";
            return View(register);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Register register)
        {
            //判读登录名是否重复，其实不用因为这个事主键，但是我给大家演示提交后判读验证值得方法
            var user = from Users in
                           (from Users in db.Users
                            where
                              Users.UserCode == register.UserCode
                            select new
                            {
                                Dummy = "x"
                            })
                       group Users by new { Users.Dummy } into g
                       select new
                       {
                           c = g.Count()
                       };
            if (user.Count() > 0)
            {

                if (user.ToList()[0].c > 0)
                {
                    ViewBag.uc = "登陆名重复请重输。";
                    return View(register);
                }

            }
            //将register的值赋给Users并保存到数据库
            Users u = new Users();
            u.UserCode=register.UserCode;
            u.UserName = register.UserName;
            u.PassWord = register.PassWord;
            u.Sex = register.Sex;
            db.Users.Add(u);
            db.SaveChanges();
            //添加完成跳转到Index方法
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string UserCode)
        {
            Users users = db.Users.Find(UserCode);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string UserCode = null)
        {
            Users users = db.Users.Find(UserCode);
            if (users == null)
            {
                return HttpNotFound();
            }
            Register register = new Register();
            register.UserCode = users.UserCode;
            register.UserName = users.UserName;
            register.Sex = users.Sex;
            return View(register);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Register register)
        {
            if (ModelState.IsValid)
            {

                List<Users> userS = (from Users in db.Users
                                     where
                                       Users.UserCode == register.UserCode
                                     select Users).ToList();
                if (userS.Count > 0)
                {
                    Users user = userS[0];
                    user.UserName = register.UserName;
                    user.PassWord = register.PassWord;
                    user.Sex = register.Sex;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
            return View(register);
        }
        public ActionResult Details(string id)
        {
            Users users=db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

    }
   
}
