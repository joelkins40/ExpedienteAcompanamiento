const getPersonalInformation = async () => {
  try {
    const response = await axios.get("/Home/ObtenerInformacionPersonal");
    const {
      carrera,
      carrerasAnterior,
      ciudadOrigen,
      estadoOrigen,
      fechaNacimiento,
      matricula,
      nacionalidad,
      nombreCompleto,
      preparatoriaProcedencia,
      semestre,
      Foto,
    } = response.data.Value;

    const dateAux = fechaNacimiento
        ? moment(fechaNacimiento, "MM/DD/YYYYTHH:mm:ssZ").format("DD/MMMM/YYYY")
      : "";

    return {
      nombre: nombreCompleto,
      carrera: carrera,
      otraCarrera: carrerasAnterior,
      matricula: matricula,
      fechaNacimiento: dateAux,
      ciudadOrigen: ciudadOrigen,
      semestre: semestre,
      nacionalidad: nacionalidad,
      estado: estadoOrigen,
      preparatoriaProcedencia,
      imagen: Foto,
    };
  } catch (err) {
    return undefined;
  }
};

const getPidm = async (pidm) => {
  const response = await axios.get(`Home/ObtenerPidm?matricula=${pidm}`);
  return { success: response.data };
};
