const getAdmisionInformation = async () => {
  try {
    const response = await axios.get(
      "/DatosAdmision/ObtenerInformacionAdmision"
    );
      const {
          Admision_Condicionada,         
          PAA,          
          Periodo_Inicio,            
          Promedio_Ingreso_Prepa,      
          Puntaje_Matematicas,
          Puntaje_Verbal,
          admisiones,      
      } = response.data.Value;

      return {
          admisionCondicionada: Admision_Condicionada,
          PAA,
          promedioInicio: Periodo_Inicio,
          promedioIngresoPrepa: Promedio_Ingreso_Prepa,
          puntajeMatematicas: Puntaje_Matematicas,
          puntajeVerbal: Puntaje_Verbal,
          admisiones
      };
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
