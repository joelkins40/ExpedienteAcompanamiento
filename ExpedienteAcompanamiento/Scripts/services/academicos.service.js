const getAcademicInformation = async () => {
    try {
        const response = await axios.get("/DatosAcademicos/ObtenerInformacionDatosAcademicos");
        debugger;
        const {
            DocumentosEntregados,
            becasPeriodos,
            datosAdministrativos,
            bloqueos
        } = response.data.Value;

        //const dateAux = datosAdministrativos[0].TERMINOS_FECHA ? moment(datosAdministrativos[0].TERMINOS_FECHA).format("DD/MMMM/YYYY") : "";
        return {
        };
    } catch (err) {
        console.error(err);
        return undefined;
    }
};
