using System;
using System.Collections.Generic;

namespace CoreApi.Entities.Master
{
    public partial class Entregas
    {
        public int IdEntrega { get; set; }
        public DateTime DtEntrega { get; set; }
        public string NmProduto { get; set; }
        public decimal VlUnitario { get; set; }
        public int QtdProduto { get; set; }
        public int IdLoteEntrega { get; set; }

        public virtual LoteEntregas IdLoteEntregaNavigation { get; set; }
    }
}
