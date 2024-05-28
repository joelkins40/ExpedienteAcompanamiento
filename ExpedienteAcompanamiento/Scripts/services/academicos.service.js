const getAcademicInformation = async () => {
  try {
    const response = await axios.get(
      "/DatosAcademicos/ObtenerInformacionDatosAcademicos"
    );
    const { datosAcademicos, estatusAlumno, toefl } = response.data.Value;

    return {
      datosAcademicos: {
        creditosAprobados: datosAcademicos[0].CREDITOS_APROBADOS,
        creditosRequeridos: datosAcademicos[0].CREDITOS_REQUERIDOS,
        materiasBajas: datosAcademicos[0].MATERIAS_BAJA,
        programa: datosAcademicos[0].PROGRAMA,
        servicioSocial: datosAcademicos[0].SERV_SOCIAL,
        toeflInd: datosAcademicos[0].TOEFL_IND,
        toeflCode: toefl[0].SORTEST_TESC_CODE,
        toeflTestDate: toefl[0].SORTEST_TEST_DATE,
        toeflTestScore: toefl[0].SORTEST_TEST_SCORE,
        toeflTestDesc: toefl[0].STVTESC_DESC,
      },
      estatusAlumno: estatusAlumno,
    };
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
