const getResidenciasInformation = async () => {
    try {
        const response = await axios.get(
            "/Residencias/obtenerInformacionResidencias"
        );
        function formato_fecha(fecha) {
            var FechaIni = fecha ? moment(fecha, "DD-MM-YYYYTHH:mm:ssZ").format(
                "DD/MMMM/YYYY"
            )
                : "";
            return FechaIni;
        };
        
        return (response?.data?.Value || []).map(a => ({
            fechaSalida: formato_fecha(a.Fecha_Salida),
            fechaEntrada: formato_fecha(a.Fecha_Entrada),
            motivo: a.Motivo,
            estatus: a.EstatusAviso,            
        }));
    } catch (err) {
        console.error(err);
        return undefined;
    }
};
