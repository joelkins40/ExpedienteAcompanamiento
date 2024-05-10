using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ExpedienteAcompanamiento.Controllers
{
    public class ExpedienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }       
    }
}