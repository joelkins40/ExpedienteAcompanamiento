using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Configuration;

namespace ExpedienteAcompanamiento.Controllers
{
    public class ExpedienteController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Server = WebConfigurationManager.AppSettings["SERVER"];

            return View();
        }       
    }
}