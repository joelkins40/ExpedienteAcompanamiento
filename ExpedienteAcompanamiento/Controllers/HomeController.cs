using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Models.Services;
using ExpedienteAcompanamiento.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
                Session["pidm"] = pidm;
            }
            
           
             
            return View();
        }

        [HttpGet]
        [ActionName("ObtenerInformacionPersonal")]
        public string Get(string id)
        {
            int valor = Convert.ToInt32(Session["pidm"]);
            ResultObject response = PersonalesService.ObtenerInformacionPersonal(valor);
            return JsonConvert.SerializeObject(response);
        }

        [HttpGet]
        [ActionName("ObtenerInformacionPersonalByPidm")]
        public  string GetPidm(string matricula)
        {

            string pidm =  AccesoService.ObtenerPIDM(matricula);
            Session["pidm"] = pidm;
            ResultObject response = PersonalesService.ObtenerInformacionPersonal(Convert.ToInt32(pidm));
            return JsonConvert.SerializeObject(response);
        }
    }    
}