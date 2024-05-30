using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Models.Services;
using ExpedienteAcompanamiento.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ExpedienteAcompanamiento.Controllers
{
    public class DatosAdmisionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Server = WebConfigurationManager.AppSettings["SERVER"];

            return View();
        }

        [HttpGet]
        [ActionName("ObtenerInformacionAdmision")]
        public string Get(string id)
        {
            int PDIM = Convert.ToInt32(Session["pidm"]);
            ResultObject response = PersonalesService.ObtenerInformacionDatosAdmision(PDIM);
            return JsonConvert.SerializeObject(response);            
        }
    }
}