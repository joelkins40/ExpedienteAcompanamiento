using ExpedienteAcompanamiento.Models.Entity.SIAT;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class ReporteEstatus : TblSiatRespuestaEstatus
    {
        public string EstatusDesc { get; set; }
        public string Area { get; set; }
    }
}