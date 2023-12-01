﻿using System;
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
    public class SanPhamController : Controller
    {
        private NhaSachEntities2 db = new NhaSachEntities2();
        public ActionResult Index()
        {
            var sanPham = db.SanPhams.ToList();
            return View(sanPham);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.NSXSelectList = new SelectList(db.NhaSanXuats, "ID", "TenNSX");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SanPham obj)
        {

            NhaSachEntities2 db = new NhaSachEntities2();
            try
            {
                var f = Request.Files["FileAnh"];
                if (f != null && f.ContentLength > 0)
                {
                    string fName = f.FileName;
                    string folder = Server.MapPath("/Assets/Upload/") + fName;
                    f.SaveAs(folder);
                    obj.Anh = "/Assets/Upload/" + fName;
                }
                db.SanPhams.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }
            finally
            {
                ViewBag.NSXSelectList = new SelectList(db.NhaSanXuats, "ID", "TenNSX");

            }

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var dsNSX = db.NhaSanXuats.ToList();
            SelectList NSXSelectList = new SelectList(dsNSX, "Id", "TenNSX");
            ViewBag.NSXSelectList = NSXSelectList;
            var sanPham = db.SanPhams.Where(item => item.Id == id).FirstOrDefault();
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult Update(SanPham sanPham, HttpPostedFileBase Anh)
        {
            var result = db.SanPhams.Find(sanPham.Id);
            try
            {
                if (Anh != null && Anh.ContentLength > 0)
                {
                    string fName = Anh.FileName;
                    string folder = Server.MapPath("/Assets/Upload/") + fName;
                    Anh.SaveAs(folder);
                    sanPham.Anh = "/Assets/Upload/" + fName;
                    result.Anh = sanPham.Anh;
                }
                result.TenSP = sanPham.TenSP;
                result.SoLuong = sanPham.SoLuong;
                result.DonGiaBan = sanPham.DonGiaBan;
                result.DonGiaNhap = sanPham.DonGiaNhap;
                result.NgayNhap = sanPham.NgayNhap;
                result.NhaSanXuatId = sanPham.NhaSanXuatId;

                //db.Entry(result).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Khong the them san pham, vui long thu lai");
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sanPham = db.SanPhams.Where(item => item.Id == id).FirstOrDefault();
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult Delete(int id, SanPham n)
        {
            try
            {
                var sanPham = db.SanPhams.Where(item => item.Id == id).FirstOrDefault();
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                return View();
            }
        }
    }
}