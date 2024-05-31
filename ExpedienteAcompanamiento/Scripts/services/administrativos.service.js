const getAdministrativeInformation = async () => {
  try {
    const response = await axios.get(
      "/DatosAdministrativos/ObtenerInformacionAdministrativos"
    );
    const {
      DocumentosEntregados,
      becasPeriodos,
      datosAdministrativos,
      bloqueos,
    } = response.data.Value;

    const dateAux = datosAdministrativos[0].TERMINOS_FECHA
      ? moment(datosAdministrativos[0].TERMINOS_FECHA).format("DD/MMMM/YYYY")
      : "";

    return {
      documentosEntregados: DocumentosEntregados,
      becasPeriodos,
      bloqueos: bloqueos,
      datosAdministrativos: {
        creditoEducativo: datosAdministrativos[0].CREDITO_EDUCATIVO,
        estatusBeca: datosAdministrativos[0].ESTATUS_BECA,
        formadorCorreo: datosAdministrativos[0].FORMADOR_CORREO,
        formadorNombre: datosAdministrativos[0].FORMADOR_NOMBRE,
        formadorPuesto: datosAdministrativos[0].FORMADOR_PUESTO,
        horasBeca: datosAdministrativos[0].HORAS_BECA,
        numPeriodo: datosAdministrativos[0].NUM_PERIODO,
        progBecario: datosAdministrativos[0].PROG_BECARIO,
        seguroUdem: datosAdministrativos[0].SEGURO_UDEM,
        terminosCond: datosAdministrativos[0].TERMINOS_COND,
        terminosFecha: dateAux,
      },
    };
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
