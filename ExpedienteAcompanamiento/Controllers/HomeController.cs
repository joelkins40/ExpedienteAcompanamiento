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
        public async Task<ActionResult> Index()
        {
           string matricula= await  AccesoService.obtenerMatriculaToken();

            FormsAuthenticationTicket sesionTicket = new FormsAuthenticationTicket(1, matricula, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), true, String.Join(";", "sesion.Roles"));
            string encryptedTicket = FormsAuthentication.Encrypt(sesionTicket);
            HttpCookie galletaSesion = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(galletaSesion);

            // Guardamos el token de sesión en una galleta
            Response.Cookies["_token"].Value = "sesion.Token";
            Response.Cookies["_token"].Expires = galletaSesion.Expires;
            return View();
        }

        [HttpGet]
        [ActionName("ObtenerInformacionPersonal")]
        public string Get(string id)
        {

            ResultObject response = PersonalesService.ObtenerInformacionPersonal(510830);
            return JsonConvert.SerializeObject(response);
        }
    }    
}