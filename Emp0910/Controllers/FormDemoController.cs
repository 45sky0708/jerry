using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emp0910.Models;
using System.Data.Entity;

namespace Emp0910.Controllers
{
    public class FormDemoController : Controller
    {
        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// partail view
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ShowData(string empnumber ="" ) 
        {
            EmpEntity objEmpList = new EmpEntity();
            FormViewModel formvm = new FormViewModel();
            
            //表單檢核
            if (!string.IsNullOrWhiteSpace(empnumber))
            {
                //撈輸入empnumber的那筆資料
                int empid = int.Parse(empnumber);
                formvm.empdata = (from P in objEmpList.EmpPersonal
                               join U in objEmpList.EmpUnit on P.EmpNumber equals U.EmpNumber
                               where P.EmpNumber == empid
                                  select new EmpViewModel
                               {
                                   EmpNumber = P.EmpNumber,
                                   EmpName = P.EmpName,
                                   EmpSex = P.EmpSex,
                                   EmpBirthday = P.EmpBirthday,
                                   CreationDate = P.CreationDate,
                                   LastloginDate = P.LastloginDate,
                                   Seniority = P.Seniority,
                                   UnitNumber = U.UnitNumber,
                                   UnitName = U.UnitName,
                                   JobTitle = U.JobTitle
                               }).ToList();
                
                if (formvm.empdata.Count == 0)
                {
                    formvm.IsSuccess = false;
                    formvm.Msg = "No Data.";
                }
                else
                {
                    formvm.IsSuccess = true;
                    formvm.Msg = "搜尋成功";
                }
            }
            else
            {
                formvm.IsSuccess = true; //第一次，未有動作，顯示未搜尋
                formvm.Msg = "未搜尋";                                
                formvm.empdata = (from P in objEmpList.EmpPersonal
                                  join U in objEmpList.EmpUnit on P.EmpNumber equals U.EmpNumber                                 
                                  select new EmpViewModel
                                  {
                                      EmpNumber = P.EmpNumber,
                                      EmpName = P.EmpName,
                                      EmpSex = P.EmpSex,
                                      EmpBirthday = P.EmpBirthday,
                                      CreationDate = P.CreationDate,
                                      LastloginDate = P.LastloginDate,
                                      Seniority = P.Seniority,
                                      UnitNumber = U.UnitNumber,
                                      UnitName = U.UnitName,
                                      JobTitle = U.JobTitle
                                  }).ToList();
            }
            

            return PartialView(formvm);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Creat() 
        {
            EmpEntity emp = new EmpEntity();
            EmpViewModel empcreat = new EmpViewModel();
            empcreat.EmpNumber = emp.EmpPersonal.Select(x => x.EmpNumber).Max(); //找出編號最大，並加1
            empcreat.EmpNumber++;
            return View(empcreat);
        }

        [HttpPost]
        public JsonResult Creat(EmpPersonal Pdatacreat,EmpUnit Udatacreat)
        {
            using (EmpEntity db = new EmpEntity())
                {
                    db.EmpPersonal.Add(Pdatacreat);
                    db.EmpUnit.Add(Udatacreat);
                    db.SaveChanges();
                }            
            return Json(JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit (int empnumber) 
        {

            EmpEntity objEmpList = new EmpEntity();
            var emplist = (from P in objEmpList.EmpPersonal
                           join U in objEmpList.EmpUnit on P.EmpNumber equals U.EmpNumber
                           where P.EmpNumber == empnumber 
                           select new EmpViewModel
                           {
                               EmpNumber = P.EmpNumber,
                               EmpName = P.EmpName,
                               EmpSex = P.EmpSex,
                               EmpBirthday = P.EmpBirthday,
                               CreationDate = P.CreationDate,
                               LastloginDate = P.LastloginDate,
                               Seniority = P.Seniority,
                               UnitNumber = U.UnitNumber,
                               UnitName = U.UnitName,
                               JobTitle = U.JobTitle
                           }).FirstOrDefault();



            return View(emplist);
        }

        [HttpPost]
        public JsonResult Edit(EmpViewModel datasava)
        {           
            using (EmpEntity db = new EmpEntity())
            {
                EmpPersonal P = db.EmpPersonal.Where(x => x.EmpNumber == datasava.EmpNumber).FirstOrDefault();
                P.EmpName = datasava.EmpName;
                P.EmpSex = datasava.EmpSex;
                P.EmpBirthday = datasava.EmpBirthday;
                P.CreationDate = datasava.CreationDate;
                P.LastloginDate = datasava.LastloginDate;
                P.Seniority = datasava.Seniority;

                EmpUnit U = db.EmpUnit.Where(x => x.EmpNumber == datasava.EmpNumber).FirstOrDefault();
                U.UnitName = datasava.UnitName;
                U.UnitNumber = datasava.UnitNumber;
                U.JobTitle = datasava.JobTitle;


                db.SaveChanges();
            }

            return Json(JsonRequestBehavior.AllowGet);

        }
    }
}