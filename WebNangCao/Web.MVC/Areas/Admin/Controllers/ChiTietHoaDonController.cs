using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.MVC.Models;

namespace Web.MVC.Areas.Admin.Controllers
{
    public class ChiTietHoaDonController : Controller
    {
        // GET: Admin/ChiTietHoaDon
        private readonly NhaSachEntities2 db = new NhaSachEntities2();
        // GET: Admin/CTHoaDon
        public ActionResult Index()
        {
            var lst = db.ChiTietHoaDons.ToList();
            return View(lst);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var HD = db.HoaDons.ToList();
            ViewBag.id = new SelectList(HD, "Id", "Id");

            var SP = db.SanPhams.ToList();
            ViewBag.MaSP = new SelectList(SP, "Id", "TenSP");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ChiTietHoaDon obj)
        {
            try
            {
                db.ChiTietHoaDons.Add(obj);
                db.SaveChanges();
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
            var obj = db.ChiTietHoaDons.Find(id);

            var HD = db.HoaDons.ToList();
            ViewBag.MaHD = new SelectList(HD, "Id", "MaHD", obj.HoaDonId);

            var SP = db.SanPhams.ToList();
            ViewBag.MaSP = new SelectList(SP, "Id", "TenSP", obj.SanPhamId);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Update(ChiTietHoaDon obj)
        {

            try
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        public ActionResult Delete(int id)
        {
            var obj = db.ChiTietHoaDons.Find(id);
            db.ChiTietHoaDons.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}