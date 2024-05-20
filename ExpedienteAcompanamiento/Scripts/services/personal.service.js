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
      semestre,
    } = response.data.Value;

    const fechahelper = new Date(fechaNacimiento);
    const fechaNacimientoAux = `${fechahelper.getDay()}/${
      fechahelper.getMonth() + 1
    }/${fechahelper.getFullYear()}`;

    return {
      nombre: nombreCompleto,
      carrera: carrera,
      otraCarrera: carrerasAnterior,
      matricula: matricula,
      fechaNacimiento: fechaNacimientoAux,
      ciudadOrigen: ciudadOrigen,
      semestre: semestre,
      nacionalidad: nacionalidad,
      estado: estadoOrigen,
      imagen:
        "https://www.pavilionweb.com/wp-content/uploads/2017/03/man-300x300.png",
    };
  } catch (err) {
    return undefined;
  }
};


const getPidm = async (pidm) => {
    const response = await axios.get(`Home/ObtenerPidm?pidm=${pidm}`);
    return { success: response.data };
}