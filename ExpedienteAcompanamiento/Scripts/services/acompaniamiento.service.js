const getAccompanimentInformation = async () => {
  try {
    const response = await axios.get(
        "/DatosAcompaniamiento/ObtenerInformacionAcompaniamiento"
    );

    return {
        alertas: response.data.Value
    };
  } catch (err) {
    console.error(err);
    return undefined;
  }
};
