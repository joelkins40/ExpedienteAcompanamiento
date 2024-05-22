﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class DatosAcademicos
    {
        public List<Academicos> datosAcademicos = new List<Academicos>();
        public List<EstatusAlumno> estatusAlumno = new List<EstatusAlumno>();
        public List<TOEFL> toefl = new List<TOEFL>();
       
    }
    public class Academicos
    {
        public string CREDITOS_REQUERIDOS { get; set; }
        public string CREDITOS_APROBADOS { get; set; }
        public string PROGRAMA { get; set; }
        public string TOEFL_IND { get; set; }
        public string SERV_SOCIAL { get; set; }
        public string MATERIAS_BAJA { get; set; }

    }

    public class EstatusAlumno
    {
        public string ESTATUS { get; set; }


    }
    public class TOEFL
    {
        public string SORTEST_TESC_CODE { get; set; }
        public string STVTESC_DESC { get; set; }
        public string SORTEST_TEST_DATE { get; set; }
        public string SORTEST_TEST_SCORE { get; set; }


    }

}