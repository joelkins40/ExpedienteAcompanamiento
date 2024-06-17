using System;
using System.Collections.Generic;

namespace ExpedienteAcompanamiento.Models.Entity.SIAT
{
    public partial class TblSiatEstatusCat
    {
        public TblSiatEstatusCat()
        {
            TblSiatRespuestaEstatuses = new HashSet<TblSiatRespuestaEstatus>();
        }

        public int Id { get; set; }
        /// <summary>
        /// ÁREA DE CANALIZACIÓN
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// ESTATUS DEL AREA
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// BORRADO LOGICO
        /// </summary>
        public string Activo { get; set; }

        public virtual TblSiatMotivo AreaNavigation { get; set; }
        public virtual ICollection<TblSiatRespuestaEstatus> TblSiatRespuestaEstatuses { get; set; }
    }
}
