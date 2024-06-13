using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class DatosAcademicos
    {
        public List<Academicos> datosAcademicos = new List<Academicos>();
        public List<TOEFL> toefl = new List<TOEFL>();
        public List<HISTORICO> historico = new List<HISTORICO>();
        public string UrlHorarios { get; set; }
    }
    public class Academicos
    {
        public string CREDITOS_REQUERIDOS { get; set; }
        public string CREDITOS_APROBADOS { get; set; }
        public string CREDITOS_PORCENTAJE { get; set; }
        public string PROGRAMA { get; set; }
        public string TOEFL_IND { get; set; }
        public string SERV_SOCIAL { get; set; }
        public string MATERIAS_BAJA { get; set; }
        public string PROM_INTEGRADO { get; set; }
        public string ESTATUS { get; set; }
        public int MATERIAS_REP_1ER_PARCIAL { get; set; }
        public int MATERIAS_REP_2DO_PARCIAL { get; set; }
        public int FALTAS_1ER_PARCIAL { get; set; }
        public int FALTAS_2DO_PARCIAL { get; set; }
        public string PORCEN_MAT_REP_1ER_PARCIAL { get; set; }
        public string PORCEN_MAT_REP_2DO_PARCIAL { get; set; }


     
    }

  
    public class TOEFL
    {
        public string SORTEST_TESC_CODE { get; set; }
        public string STVTESC_DESC { get; set; }
        public string SORTEST_TEST_DATE { get; set; }
        public string SORTEST_TEST_SCORE { get; set; }


    }
    public class HISTORICO
    {
        public string GPA { get; set; }
        public string PERIODO { get; set; }


    }
}