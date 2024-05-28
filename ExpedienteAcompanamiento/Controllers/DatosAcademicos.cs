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
    public class DatosAcademicosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("ObtenerInformacionDatosAcademicos")]
        public string Get(string id)
        {
            int PDIM = Convert.ToInt32(Session["pidm"]);
            ResultObject response = PersonalesService.ObtenerInformacionDatosAcademicos(PDIM, Convert.ToString(Session["token"]));
            return JsonConvert.SerializeObject(response);            
        }

        
    }
}