using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Models.Services;
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
        [HttpGet]
        [ActionName("GetAllPersonales")]
        public string Get(string id)
        {

            Personales personales = PersonalesService.ObtenerPersonales(510830);
            string jsonData = JsonConvert.SerializeObject(personales);
            return jsonData;
        }
            public ActionResult Index()
        {
            return View();
        }       
    }
}