using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.DTO
{
    public class DtoLoteImportacao
    {
        public int Id { get; set; }

        [JsonProperty("DataImportacao")]
        public DateTime DtImportacao { get; set; }

        [JsonProperty("NumeroRegistros")]
        public int NrRegistros { get; set; }

        [JsonProperty("NumeroTotalProdutos")]
        public int NrTotalProdutos { get; set; }

        [JsonProperty("ValorTotal")]
        public decimal VlTotalImportado { get; set; }

        public DateTime MenorDataEntrega { get; set; }
    }
}
