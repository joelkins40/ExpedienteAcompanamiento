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
    public class DatosAdmisionController : Controller
    {
        public ActionResult Index()
        {
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
        [HttpGet]
        [ActionName("ObtenerInformacionAdmisionByPidm")]
        public string GetPidm(string matricula)
        {

            string pidm = AccesoService.ObtenerPIDM(matricula);
            Session["pidm"] = pidm;
            ResultObject response = PersonalesService.ObtenerInformacionDatosAdmision(Convert.ToInt32(pidm));
            return JsonConvert.SerializeObject(response);
        }
    }
}