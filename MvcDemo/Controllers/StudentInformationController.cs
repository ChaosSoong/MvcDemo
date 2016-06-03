using MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using MvcDemo.CS;

namespace MvcDemo.Controllers
{
    public class StudentInformationController : Controller
    {
        //
        // GET: /StudentInformation/
        private htgisEntities1 db = new htgisEntities1();
        /// <summary>
        /// 学生信息加载
        /// </summary>
        /// <param name="Dp">系别</param>
        /// <param name="SearchString">姓名</param>
        /// <returns>默认显示全部</returns>
        [MemberValidation]
        public ActionResult Index(string Dp, string SearchString)
        {
            //查询代码表系别分类并赋值给View的系别DropDownList
            var Dp1 = from d in db.Code
                       where 
                     d.Type=="系别"
                           orderby d.Name                         
                           select new
                           {
                               d.Name
                           };


            ViewBag.Dp = new SelectList(Dp1,"Name","Name");

            //将linq查询内容赋给非数据库实体类
            IQueryable<STList>  AdList = from a in db.StudentInformation
                     from b in db.Code
                     where
                       a.Department == b.Id
                     orderby
                       b.Name,
                       a.Name
                               select new STList
                     {
                         Id=a.Id,
                         Name=a.Name,
                         Sex=a.Sex,
                         DepartmentName = b.Name,
                         EntranceTime=a.EntranceTime
                     };
            //由于ID是数字所以生成删除等按钮时input的ID位数字会发生找不到的问题。
            ViewBag.id = "d";
            //判读是否有查询条件
            if (!String.IsNullOrEmpty(SearchString))
            {
                AdList = AdList.Where(s => s.Name.Contains(SearchString));
            }
            //判读是否有查询条件
            if (string.IsNullOrEmpty(Dp))
                return View(AdList.ToList());
            else
            {
                return View(AdList.Where(x => x.DepartmentName == Dp));
            }

           // return View(AdList.ToList());
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        [MemberValidation]
        public ActionResult Create()
        {
            //查询代码表系别分类并赋值给View的系别DropDownList
            var dcode = from Code in db.Code
                        where
                          Code.Type == "系别"
                        orderby
                          Code.Name
                        select new
                        {
                            Code.Name,
                            Code.Id
                        };
            //设置 DropDownList的VALUE和TEXT
            ViewBag.DepartmentCode = new SelectList(dcode, "Id", "Name");

            //设置默认值
            StudentInformation st = new StudentInformation();
            st.Sex = "1";
            st.Department = 1;
            st.EntranceTime = System.DateTime.Now;
            return View(st);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentInformation st)
        {
            db.StudentInformation.Add(st);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            StudentInformation st = db.StudentInformation.Find(Id);
            db.StudentInformation.Remove(st);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {

            IQueryable<STList> AdList = from a in db.StudentInformation
                                        from b in db.Code
                                        where
                                          a.Department == b.Id && a.Id==id
                                        orderby
                                          b.Name,
                                          a.Name
                                        select new STList
                                        {
                                            Id = a.Id,
                                            Name = a.Name,
                                            Sex = a.Sex,
                                            DepartmentName = b.Name,
                                            EntranceTime = a.EntranceTime
                                        };
           STList st = new STList();
          
           if (AdList.Count() > 0)
           {
               st = AdList.ToList()[0];
           }
           else
           {
               return HttpNotFound();
           }
            return View(st);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">学生信息的唯一ID值</param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            //查询代码表系别分类并赋值给View的系别DropDownList
            var dcode = from Code in db.Code
                        where
                          Code.Type == "系别"
                        orderby
                          Code.Name
                        select new
                        {
                            Code.Name,
                            Code.Id
                        };
            ViewBag.DepartmentCode = new SelectList(dcode, "Id", "Name");
            StudentInformation st = db.StudentInformation.Find(id);
            if (st == null)
            {
                return HttpNotFound();
            }
            
            return View(st);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentInformation st)
        {
            if (ModelState.IsValid)
            {

                
                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            return View(st);
        }
    }
}
