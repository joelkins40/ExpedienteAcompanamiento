const getAcademicInformation = async () => {
  try {
    const response = await axios.get(
      "/DatosAcademicos/ObtenerInformacionDatosAcademicos"
    );
    const { datosAcademicos, estatusAlumno, toefl, UrlHorarios } =
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
        materiasBajas: datosAcademicos[0].MATERIAS_BAJA,
        programa: datosAcademicos[0].PROGRAMA,
        servicioSocial: datosAcademicos[0].SERV_SOCIAL,
        toeflInd: datosAcademicos[0].TOEFL_IND,
        toeflCode: toefl[0].SORTEST_TESC_CODE,
        toeflTestDate: dateAux,
        toeflTestScore: toefl[0].SORTEST_TEST_SCORE,
        toeflTestDesc: toefl[0].STVTESC_DESC,
      },
      estatusAlumno: estatusAlumno,
      urlHorarios: UrlHorarios,
    };
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
