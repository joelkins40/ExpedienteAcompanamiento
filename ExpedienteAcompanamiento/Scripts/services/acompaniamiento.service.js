const getAccompanimentInformation = async () => {
  try {
    const response = await axios.get(
        "/DatosAcompaniamiento/ObtenerInformacionAcompaniamiento"
    );

    const {
        alerts,
        areas,
    } = response.data.Value;

    return {
        alertas: alerts,
        areas: areas
    };
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
