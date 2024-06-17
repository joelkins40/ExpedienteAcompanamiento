using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExpedienteAcompanamiento.Models.Entity.SIAT
{
    public partial class TblSiatAsignacion
    {
        /// <summary>
        /// Id unico del registro
        /// </summary>
        [JsonIgnore]
        public int IdAsig { get; set; }
        /// <summary>
        /// ID del reporte de SIAT (TBL_RESPUESTAS - Identificador)
        /// </summary>
        [JsonIgnore]
        public int IdRepSiat { get; set; }
        /// <summary>
        /// Area asignada para seguimiento
        /// </summary>
        public string AreaAsig { get; set; }
        /// <summary>
        /// Fecha de asignación
        /// </summary>
        public DateTime? FechaAsig { get; set; }

        [JsonIgnore]
        public virtual TblSiatMotivo AreaAsigNavigation { get; set; }
    }
}
