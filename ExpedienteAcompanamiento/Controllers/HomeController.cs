using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Models.Services;
using ExpedienteAcompanamiento.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace ExpedienteAcompanamiento.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(string token)
        {
            
            if (token != null)
            {
                string pidm = await AccesoService.obtenerMatriculaToken(token);
                if(pidm != null)
                {
                    Session["pidm"] = pidm;
                    Session["token"] = token;
                }
               
            }
          
            ViewBag.Server = WebConfigurationManager.AppSettings["SERVER"];
            //ViewBag.Visible1 = false;
            return View();
        }

        [HttpGet]
        [ActionName("ObtenerInformacionPersonal")]
        public string Get(string id)
        {
            int valor = Convert.ToInt32(Session["pidm"]);
            ResultObject response = PersonalesService.ObtenerInformacionPersonal(valor);
            
            if(response.Success && Session["term"] == null)
            {
                var data = (DatosInicio)response.Value;
                Session["term"] = data.periodoActual;
            }

            return JsonConvert.SerializeObject(response);
        }

        [HttpGet]
        [ActionName("ObtenerPidm")]
        public  string GetPidm(string matricula)
        {

            string pidm =  AccesoService.ObtenerPIDM(matricula);
            Session["pidm"] = pidm;
            Session["matricula"] = matricula;

            return JsonConvert.SerializeObject(new {success = Convert.ToInt32(pidm) > 0 ? true : false});
        }
    }    
}