using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class Personales
    {
        public string matricula { get; set; }
        public string nombreCompleto { get; set; }
        public string fechaNacimiento { get; set; }
        public string ciudadOrigen { get; set; }
        public string estadoOrigen { get; set; }
        public string domicilioPermanente { get; set; }
        public string telefono { get; set; }
        public string domicilioZona { get; set; }
        public string carreraEstudia { get; set; }
        public string carrerasInscrito { get; set; }
        public string dobleTitulacion { get; set; }
        public string dobleGrado { get; set; }
        public string correoPreferente { get; set; }
        public string correoPersonal { get; set; }
        public string correoUDEM { get; set; }
        public string seguroGMMUDEM { get; set; }
        public string seguroGMMParticular { get; set; }
        public string localForaneo { get; set; }
        public string genero { get; set; }
    }