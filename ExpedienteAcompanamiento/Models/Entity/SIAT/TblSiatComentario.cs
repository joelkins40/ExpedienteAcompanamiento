using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExpedienteAcompanamiento.Models.Entity.SIAT
{
    public partial class TblSiatComentario
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int RespuestaId { get; set; }
        public string AreaId { get; set; }
        public string Comentario { get; set; }
        public string UserId { get; set; }
        public DateTime FechaComentario { get; set; }
        public string UserFullName { get; set; }
        public string UserIDBN { get; set; }

        [JsonIgnore]
        public virtual TblSiatMotivo Area { get; set; }
    }
}
