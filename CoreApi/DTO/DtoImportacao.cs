using System;

namespace CoreApi.DTO
{
    public class DtoImportacao
    {
        public DateTime DtEntrega { get; set; }

        public string NmProduto { get; set; }

        public decimal VlUnitario { get; set; }

        public int QtdProduto { get; set; }

        public decimal VlTotal => VlUnitario * QtdProduto;
    }
}
