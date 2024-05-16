using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class Admisiones
    {
        public string Periodo_Inicio { get; set; }
        public string PAA { get; set; }
        public string Puntaje_Verbal { get; set; }
        public string Puntaje_Matematicas { get; set; }
        public string Promedio_Ingreso_Prepa { get; set; }
        public string Admision_Condicionada { get; set; }
             
    }
}