﻿using Mercy.Models;
using Mercy.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHttpRecorder httpRecorder)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}