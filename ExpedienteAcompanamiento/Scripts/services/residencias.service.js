const getResidenciasInformation = async () => {
  try {
    const response = await axios.get(
      "/Residencias/obtenerInformacionResidencias"
      );
    
      return (response?.data?.Value || []).map(a => ({
          fechaSalida: a.Fecha_Salida,
          fechaEntrada: a.Fecha_Entrada,
          motivo: a.Motivo,
          estatus: a.Estatus,
          comentario: a.Comentario,
      }));
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
