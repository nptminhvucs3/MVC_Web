using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.MVC.Models;
namespace Web.MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class HoaDonController : Controller
    {
        
        private readonly NhaSachEntities2 context = new NhaSachEntities2();
        public ActionResult Index()
        {
            var lst = context.HoaDons.ToList();
            return View(lst);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var nguoiDung = context.NguoiDungs.ToList();
            SelectList NguoiMuaSelectList = new SelectList(nguoiDung, "Id", "Ten");
            ViewBag.NguoiMuaSelectList = NguoiMuaSelectList;
            return View();
        }
        [HttpPost]
        public ActionResult Create(HoaDon obj)
        {
            try
            {
                context.HoaDons.Add(obj);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var nguoiMua = context.NguoiDungs.ToList();
            SelectList NguoiMuaSelectList = new SelectList(nguoiMua, "Id", "Ten");
            ViewBag.NguoiMuaSelectList = NguoiMuaSelectList;
            return View();
        }
        [HttpPost]
        public ActionResult Update(HoaDon obj)
        {

            try
            {
                context.Entry(obj).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var hoaDon = context.HoaDons.Where(item => item.Id == id).FirstOrDefault();
            return View(hoaDon);
        }
        [HttpPost]
        public ActionResult Delete(int id, HoaDon n)
        {
            try
            {
                var obj = context.HoaDons.Find(id);
                context.HoaDons.Remove(obj);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                return View();
            }
        }

    }
}