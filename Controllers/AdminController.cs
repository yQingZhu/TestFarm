﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFarm.Controllers
{
    public class AdminController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
