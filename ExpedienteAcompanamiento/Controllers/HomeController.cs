﻿using ExpedienteAcompanamiento.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpedienteAcompanamiento.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var isEmployee = PersonalesService.getPersonales(510830);
            return View();
        }      
        

    
    }

}