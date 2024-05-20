﻿using ExpedienteAcompanamiento.Models.Services;
using ExpedienteAcompanamiento.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpedienteAcompanamiento.Controllers
{
    public class DatosAdministrativosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [ActionName("ObtenerInformacionAdministrativos")]
        public string Get(string id)
        {
            int PDIM = Convert.ToInt32(Session["pidm"]);
            ResultObject response = PersonalesService.ObtenerInformacionDatosAdministrativos(PDIM);
            return JsonConvert.SerializeObject(response);
        }
    }
}