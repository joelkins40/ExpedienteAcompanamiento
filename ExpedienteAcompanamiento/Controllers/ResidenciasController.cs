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
    public class ResidenciasController : Controller
    {
        // GET: Residencias
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("obtenerInformacionResidencias")]
        public string Get()
        {;
            string pidm = Convert.ToString(Session["pidm"]);
            string matricula = Convert.ToString(Session["matricula"]);
            ResultObject response = ResidenciasAlertasService.obtenerInformacionAlertas(matricula, pidm);
            return JsonConvert.SerializeObject(response);
        }
    }
}