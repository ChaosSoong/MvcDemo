using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;
using System.Data.Entity;
using System.Data;

namespace MvcDemo.Controllers
{
    public class ExaminationInformationController : Controller
    {
        //
        // GET: /ExaminationInformation/
        private htgisEntities1 db = new htgisEntities1();
        /// <summary>
        /// 加载学生分数
        /// </summary>
        /// <param name="Id">学生信息表的ID对应成绩表的StudentId</param>
        /// <returns></returns>
        public ActionResult Index(int Id)
        {
            ViewBag.cid = "d";
            ViewBag.id = Id;
            //因为需要级联学生名所以新建EIList实体
            IQueryable<EIList> AdList = from a in db.ExaminationInformation
                                        from b in db.Code
                                        where
                                          a.Discipline == b.Id && a.StudentId==Id
                                          orderby
                                        a.ExaminationTime,
                                        a.Discipline
                                        select new EIList
                                        {
                                            Id=a.Id,
                                            StudentId=a.StudentId,
                                            DisciplineName=b.Name,
                                            ExaminationTime=a.ExaminationTime,
                                            Fraction=a.Fraction
                                        };
            //查找学生名
            var name = from StudentInformation in db.StudentInformation
                       where
                         StudentInformation.Id == Id
                       select new
                       {
                           StudentInformation.Name
                       };
            ViewBag.name = name.ToList()[0].Name;
            return View(AdList.ToList());
        }
        /// <summary>
        /// 创建成绩
        /// </summary>
        /// <param name="id">ExaminationInformation中的唯一标识</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            var name = from StudentInformation in db.StudentInformation
                       where
                         StudentInformation.Id == id
                       select new
                       {
                           StudentInformation.Name
                       };
            ViewBag.name = name.ToList()[0].Name;
            var dcode = from Code in db.Code
                        where
                          Code.Type == "学科"
                        orderby
                          Code.Name
                        select new
                        {
                            Code.Name,
                            Code.Id
                        };
            ViewBag.DisciplineCode = new SelectList(dcode, "Id", "Name");
            ExaminationInformation st = new ExaminationInformation();
            st.Discipline = 5;
            st.StudentId=id;
            st.ExaminationTime = System.DateTime.Now;
            return View(st);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExaminationInformation st)
        {
            db.ExaminationInformation.Add(st);
            db.SaveChanges();
            return RedirectToAction("Index/"+st.StudentId);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var st = from m in db.ExaminationInformation
                                        where m.Id == Id
                                        select m;
            ExaminationInformation stt = st.ToList()[0];
            db.ExaminationInformation.Remove(stt);
            db.SaveChanges();
            return RedirectToAction("Index/" + stt.StudentId);
        }
        public ActionResult Details(int Id)
        {

            ViewBag.id = Id;
            IQueryable<EIList> AdList = from a in db.ExaminationInformation
                                        from b in db.Code
                                        where
                                          a.Discipline == b.Id && a.Id == Id
                                        orderby
                                      a.ExaminationTime,
                                      a.Discipline
                                        select new EIList
                                        {
                                            Id = a.Id,
                                            StudentId = a.StudentId,
                                            DisciplineName = b.Name,
                                            ExaminationTime = a.ExaminationTime,
                                            Fraction = a.Fraction
                                        };
            int studentId=AdList.ToList()[0].StudentId;
            var name = from StudentInformation in db.StudentInformation
                       where
                         StudentInformation.Id == studentId
                       select new
                       {
                           StudentInformation.Name
                       };
            ViewBag.name = name.ToList()[0].Name;
            EIList st = new EIList();

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

        public ActionResult Edit(int Id = 0)
        {
            var dcode = from Code in db.Code
                        where
                          Code.Type == "学科"
                        orderby
                          Code.Name
                        select new
                        {
                            Code.Name,
                            Code.Id
                        };
            ViewBag.DisciplineCode = new SelectList(dcode, "Id", "Name");
            var st = from m in db.ExaminationInformation
                     where m.Id == Id
                     select m;
            ExaminationInformation stt = st.ToList()[0];
            int studentId = stt.StudentId;
            ViewBag.studentId = studentId;
            var name = from StudentInformation in db.StudentInformation
                       where
                         StudentInformation.Id == studentId
                       select new
                       {
                           StudentInformation.Name
                       };
            ViewBag.name = name.ToList()[0].Name;
            if (stt == null)
            {
                return HttpNotFound();
            }

            return View(stt);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExaminationInformation st)
        {
            if (ModelState.IsValid)
            {


                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/"+st.StudentId);
            }
            return View(st);
        }
    }
}
