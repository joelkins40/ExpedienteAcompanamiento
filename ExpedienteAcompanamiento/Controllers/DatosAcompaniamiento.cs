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
    public class DatosAcompaniamientoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Server = WebConfigurationManager.AppSettings["SERVER"];

            return View();
        }
        [HttpGet]
        [ActionName("ObtenerInformacionAcompaniamiento")]
        public string Get(string id)
        {
            int pidm = Convert.ToInt32(Session["pidm"]);
            string term = Session["term"].ToString();
            //string term = "";
            ResultObject response = ReportesService.ObtenerAlertas(pidm, term);
            return JsonConvert.SerializeObject(response);
        }
    }
}