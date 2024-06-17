using ExpedienteAcompanamiento.Models.Entity.SIAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class ReportResult
    {
        public int IdReporte { set; get; }
        public string Matricula { set; get; }
        public string NombreAlumno { set; get; }
        public string Carrera { set; get; }
        public string Escuela { set; get; }
        public string CorreoAlumno { set; get; }
        public string Telefono { set; get; }
        public string Beca { set; get; }
        public List<ReporteArea> Canalizacion { set; get; }
        public string Grado { set; get; }
        public string GeneroReporte { set; get; }
        public DateTime? FechaRegistro { set; get; }
        public string Crn { set; get; }
        public string Materia { set; get; }
        public string MatriculaMaestro { set; get; }
        public string NombreMaestro { set; get; }
        public string CorreoMaestro { set; get; }
        public string TipoRegistro { set; get; }
        public List<string> Motivos { set; get; }
        public string AlumnoEnterado { set; get; }
        public string ComentariosMaestro { set; get; }
        public List<ReporteEstatus> Estatus { get; set; }
        public List<TblSiatComentario> Comentarios { get; set; }
        //AG: 08/02/2024 | Se agrega correo DPA 
        public string CorreoDPA { set; get; }
        //AG: 08/02/2024 | Se valor para conocer si tiene apoyo Linde. 
        public string ApoyoLINDE { set; get; }
    }
}