const getAdministrativeInformation = async () => {
  try {
      const response = await axios.get("/DatosAdministrativos/ObtenerInformacionAdministrativos");      
     const {
         DocumentosEntregados,
         becasPeriodos,
         datosAdministrativos
     } = response.data.Value;

    return {
        documentosEntregados: DocumentosEntregados,
        becasPeriodos,
        datosAdministrativos: {
            bloqueo: datosAdministrativos[0].BLOQUEO,
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
            terminosFecha: datosAdministrativos[0].TERMINOS_FECHA,
        }
    };
  } catch (err) {
      console.error(err);
    return undefined;
  }
};
