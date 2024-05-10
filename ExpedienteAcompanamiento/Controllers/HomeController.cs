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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            PersonalesService personalesService = new PersonalesService();

            return View();
        }


        [HttpGet]
        [ActionName("GetAllPersonales")]
        public string Get(string id)
        {

            DatosInicio personales = PersonalesService.ObtenerDatosPrincipales(510830);
            string jsonData = JsonConvert.SerializeObject(personales);
            return jsonData;
        }

    }

}