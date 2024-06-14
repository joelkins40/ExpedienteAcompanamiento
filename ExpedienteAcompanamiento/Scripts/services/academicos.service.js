const getAcademicInformation = async () => {
  try {
    const response = await axios.get(
      "/DatosAcademicos/ObtenerInformacionDatosAcademicos"
    );
    const { datosAcademicos, estatusAlumno, toefl, UrlHorarios, historico } =
      response.data.Value;

    const dateAux = toefl[0].SORTEST_TEST_DATE
      ? moment(toefl[0].SORTEST_TEST_DATE, "YYYY-MM-DDTHH:mm:ssZ").format(
          "DD/MMMM/YYYY"
        )
      : "";

    return {
      datosAcademicos: {
        creditosAprobados: datosAcademicos[0].CREDITOS_APROBADOS,
        creditosRequeridos: datosAcademicos[0].CREDITOS_REQUERIDOS,
        porcentaje: datosAcademicos[0].CREDITOS_PORCENTAJE,
        materiasBajas: datosAcademicos[0].MATERIAS_BAJA,
        programa: datosAcademicos[0].PROGRAMA,
        servicioSocial: datosAcademicos[0].SERV_SOCIAL,
        toeflInd: datosAcademicos[0].TOEFL_IND,
        toeflCode: toefl[0].SORTEST_TESC_CODE,
        toeflTestDate: dateAux,
        toeflTestScore: toefl[0].SORTEST_TEST_SCORE,
        toeflTestDesc: toefl[0].STVTESC_DESC,
        materiasReprobadas1erParcial: datosAcademicos[0].MATERIAS_REP_1ER_PARCIAL,
        materiasReprobadas2erParcial: datosAcademicos[0].MATERIAS_REP_2DO_PARCIAL,
        porcentajeReprobadas1erParcial: datosAcademicos[0].PORCEN_MAT_REP_1ER_PARCIAL,
        porcentajeReprobadas2erParcial: datosAcademicos[0].PORCEN_MAT_REP_2DO_PARCIAL,
        faltasReprobadas1erParcial: datosAcademicos[0].FALTAS_1ER_PARCIAL,
        faltasReprobadas2erParcial: datosAcademicos[0].FALTAS_2DO_PARCIAL,
      },
      estatusAlumno: estatusAlumno,
      urlHorarios: UrlHorarios,
      porcentajes: historico
    };
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
