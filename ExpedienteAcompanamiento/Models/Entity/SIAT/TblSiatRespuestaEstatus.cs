using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExpedienteAcompanamiento.Models.Entity.SIAT
{
    public partial class TblSiatRespuestaEstatus
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int RespuestaId { get; set; }
        public int EstatusId { get; set; }
        public DateTime FechaEstatus { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserIDBN { get; set; }

        [JsonIgnore]
        public virtual TblSiatEstatusCat Estatus { get; set; }
    }
}
