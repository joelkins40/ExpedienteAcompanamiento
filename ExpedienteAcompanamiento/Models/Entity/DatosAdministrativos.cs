using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class DatosAdministrativos
    {
        public List<BecasPeriodos> becasPeriodos = new List<BecasPeriodos>();
        public List<DocumentosEntregados> DocumentosEntregados = new List<DocumentosEntregados>();
        public List<Administrativos> datosAdministrativos = new List<Administrativos>();
        public List<Bloqueos> bloqueos = new List<Bloqueos>();
    }
    public class BecasPeriodos
    {
        public string BECA_NOMBRE { get; set; }
        public string BECA_PORCENTAJE { get; set; }

        public string BECA_PERIODO { get; set; }
        public string BECA_TIPO { get; set; }


    }

    public class Bloqueos
    {
        public string stvhldd_desc { get; set; }


    }
    public class DocumentosEntregados
    {
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ORIGEN { get; set; }


    }


    public class Administrativos
    {
        public string SEGURO_UDEM { get; set; }
        public string SEGURO_PART { get; set; }
        public string BLOQUEO { get; set; }
        public string TERMINOS_COND { get; set; }
        public string TERMINOS_FECHA { get; set; }
        public string PROG_BECARIO { get; set; }
        public string ESTATUS_BECA { get; set; }
        public string NUM_PERIODO { get; set; }
        public string HORAS_BECA { get; set; }
        public string CREDITO_EDUCATIVO { get; set; }
        public string FORMADOR_NOMBRE { get; set; }
        public string FORMADOR_PUESTO { get; set; }
        public string FORMADOR_CORREO { get; set; }
        public string FORMADOR_PIDM { get; set; }

        

    }
}