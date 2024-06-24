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
        [ActionName("ObtenerAlertas")]
        public string Get()
        {
            int pidm = Convert.ToInt32(Session["pidm"]);
            //string term = Session["term"].ToString();
            //string term = "";
            ResultObject response = ResidenciasAlertasService.ObtenerInformacionDatosAdmision(1);
            return JsonConvert.SerializeObject(response);
        }
    }
}