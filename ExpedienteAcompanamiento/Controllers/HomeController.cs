using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Models.Services;
using ExpedienteAcompanamiento.Utils;
using Newtonsoft.Json;
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
            return View();
        }

        [HttpGet]
        [ActionName("ObtenerInformacionPersonal")]
        public string Get(string id)
        {
            ResultObject response = PersonalesService.ObtenerInformacionPersonal(510830);
            return JsonConvert.SerializeObject(response);
        }
    }    
}