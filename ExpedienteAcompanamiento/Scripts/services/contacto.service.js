const getContactosInformation = async () => {
  try {
    const response = await axios.get(
      "/DatosAcademicos/ObtenerInformacionContactos"
    );    
    const {
      Contacto_Emergencia,
      Correo_De_Madre,
      Correo_De_Tutor,
      Correo_Del_Padre,
      Correo_Personal,
      Correo_Preferencia,
      Correo_UDEM,
      Direccion,
      Direccion_Metropolitana,
      Nombre_De_Madre,
      Nombre_Del_Padre,
      Nombre_Del_Tutor,
      SGMM_Particular,
      SGMM_UDEM,
      Tel_De_Madre,
      Tel_De_Tutor,     
      Tel_Del_Padre,      
      Tel_DomicilioPermanente,
      Tel_Emergencia,
      Telefono,      
    } = response.data.Value;

    return {
        telefono: Telefono,
        domicilioPermanente: Direccion,
        domicilioLocal: Direccion_Metropolitana,
        correo: Correo_Personal,
        correoUDEM: Correo_UDEM,
        nombrePadre: Nombre_Del_Padre,
        nombreMadre: Nombre_De_Madre,
        telefonoPadre: Tel_Del_Padre,
        telefonoMadre: Tel_De_Madre,
        correoPadre: Correo_Del_Padre,
        correoMadre: Correo_De_Madre,
        nombreTutor: Nombre_Del_Tutor,
        telefonoTutor: Tel_De_Tutor,
        correoTutor: Correo_De_Tutor,
        emergenciaNombre: Contacto_Emergencia,
        emergenciaTelefono: Tel_Emergencia,
    };
  } catch (err) {
    return undefined;
  }
};
