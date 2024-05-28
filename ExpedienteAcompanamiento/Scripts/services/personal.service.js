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
    } = response.data.Value;

    const dateAux = fechaNacimiento
      ? moment(fechaNacimiento).format("DD/MMMM/YYYY")
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
      imagen:
        "https://www.pavilionweb.com/wp-content/uploads/2017/03/man-300x300.png",
    };
  } catch (err) {
    return undefined;
  }
};

const getPidm = async (pidm) => {
  const response = await axios.get(`Home/ObtenerPidm?matricula=${pidm}`);
  return { success: response.data };
};
