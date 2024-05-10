using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class DatosInicio
    {
        public string nombreCompleto { get; set; }
        public string matricula { get; set; }
        public string fechaNacimiento { get; set; }
        public string ciudadOrigen { get; set; }
        public string estadoOrigen { get; set; }
        public string carreraEstudia { get; set; }
        public string carrerasInscrito { get; set; }
        public string semestre { get; set; }
        public string nacionalidad { get; set; }

    }
}