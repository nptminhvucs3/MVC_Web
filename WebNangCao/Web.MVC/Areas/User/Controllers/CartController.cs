﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.MVC.Areas.User.Controllers
{
    public class CartController : Controller
    {
        // GET: User/Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}