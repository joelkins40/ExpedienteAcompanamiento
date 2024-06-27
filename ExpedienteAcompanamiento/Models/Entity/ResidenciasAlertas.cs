using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    //public class ResidenciasAlertas
    //{
    //    public List<Alertas> residencias_alertas { get; set; }
    //}
    public class ResidenciasAlertas
    {
        
        public string Fecha_Salida { get; set; }
        public string Fecha_Entrada { get; set; }
        public string Comentario { get; set; }
        public string EstatusAviso { get; set; }
        public string Motivo { get; set; }
        public string Fecha_Alta { get; set; }

    }
}