using System;
using System.Collections.Generic;

namespace ExpedienteAcompanamiento.Models.Entity.SIAT
{
    public partial class TblSiatMotivo
    {
        public TblSiatMotivo()
        {
            TblSiatAsignacions = new HashSet<TblSiatAsignacion>();
            TblSiatComentarios = new HashSet<TblSiatComentario>();
            TblSiatEstatusCats = new HashSet<TblSiatEstatusCat>();
        }

        public string CveMotivo { get; set; }
        public string Motivo { get; set; }
        public string Activo { get; set; }

        public virtual ICollection<TblSiatAsignacion> TblSiatAsignacions { get; set; }
        public virtual ICollection<TblSiatComentario> TblSiatComentarios { get; set; }
        public virtual ICollection<TblSiatEstatusCat> TblSiatEstatusCats { get; set; }
    }
}
