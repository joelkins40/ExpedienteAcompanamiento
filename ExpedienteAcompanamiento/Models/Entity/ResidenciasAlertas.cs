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
        public string Id_Aviso { get; set; }
        public string Matricula { get; set; }
        public string Fecha_Salida { get; set; }
        public string Fecha_Entrada { get; set; }
        public string Motivo { get; set; }
        public string FechaCreacion { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public string Comentario { get; set; }
        public string Proceso { get; set; }
        public string FechaDeFinalizacion { get; set; }
    }
}