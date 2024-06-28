using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Entity
{
    public class Residencias
    {
        public string Consecutivo_Periodo_Ingreso { get; set; }
        public string Reservacion { get; set; }
        public string Estancia { get; set; }
        public string Companero_Cuarto { get; set; }
        public string Habitacion_Asignada { get; set; }
        public string Tiene_Carro { get; set; }
        public string Condicion_Suspencion { get; set; }
        public string Aviso_Ausencia { get; set; }
        public string Reporte_Conducta { get; set; }
        public string Descuentos { get; set; }
        public string Esquema_Pagos { get; set; }
        public List<EstadoCuenta> EstadoCuentas { get; set; }
        public List<ReporteConducta> ReportesConducta { get; set; }
        public List<Quejas> Quejas { get; set; }
    }
}