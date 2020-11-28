using System;
using System.Collections.Generic;

namespace CoreApi.Entities.Master
{
    public partial class LoteEntregas
    {
        public LoteEntregas()
        {
            Entregas = new HashSet<Entregas>();
        }

        public int IdLoteEntrega { get; set; }
        public string NmArquivoLote { get; set; }
        public DateTime DtImportacao { get; set; }
        public int NrRegistros { get; set; }
        public int NrTotalProdutos { get; set; }
        public decimal VlTotalImportado { get; set; }
        public DateTime DtEntregaMenor { get; set; }

        public virtual ICollection<Entregas> Entregas { get; set; }
    }
}
