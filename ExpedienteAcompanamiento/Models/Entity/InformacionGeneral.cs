using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class InformacionGeneral
    {                 
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Direccion_Metropolitana { get; set; }
        public string Correo_Preferencia { get; set; }
        public string Correo_Personal { get; set; }
        public string Correo_UDEM { get; set; }
        public string SGMM_UDEM { get; set; }
        public string SGMM_Particular { get; set; }
        public string Nombre_Del_Padre { get; set; }
        public string Nombre_De_Madre{ get; set; }
        public string Nombre_Del_Tutor { get; set; }
        public string Correo_Del_Padre { get; set; }
        public string Correo_De_Madre { get; set; }
        public string Correo_De_Tutor { get; set; }
        public string Tel_Del_Padre { get; set; }
        public string Tel_De_Madre { get; set; }
        public string Tel_De_Tutor { get; set; }
        public string Contacto_Emergencia { get; set; } 
        public string Tel_Emergencia { get; set; } 
        public string Tel_DomicilioPermanente { get; set; }
    }
}