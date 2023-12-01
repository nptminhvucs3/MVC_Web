using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.MVC.Models;

namespace Web.MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class NhaSanXuatController : Controller
    {
        private readonly NhaSachEntities2 context = new NhaSachEntities2();
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NhaSanXuat obj)
        {
            try
            {
                context.NhaSanXuats.Add(obj);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
        }

        public ActionResult Index()
        {
            var NSX = context.NhaSanXuats.ToList();
            return View(NSX);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var obj = context.NhaSanXuats.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Update(NhaSanXuat obj)
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
       
        public ActionResult Delete(int id)
        {
            var sp = context.NhaSanXuats.Find(id);
            context.NhaSanXuats.Remove(sp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}