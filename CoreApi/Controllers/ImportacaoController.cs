using AutoMapper;
using ClosedXML.Excel;
using CoreApi.Contexts;
using CoreApi.DTO;
using CoreApi.Entities.Master;
using CoreApi.Utils;
using CoreApi.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportacaoController : ControllerBase
    {
        private readonly MasterContext _context;
        private readonly IMapper _mapper;

        public ImportacaoController(MasterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista os dados gerais das importacoes realizadas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DtoLoteImportacao>> GetImportacoes()
        {
            var listaLotes = _context.LoteEntregas.ToList();
            return Ok(_mapper.Map<List<DtoLoteImportacao>>(listaLotes));
        }

        /// <summary>
        /// Lista os dados de uma importacao especifica.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DtoImportacao>> GetImportacao(int id)
        {
            var entregas = _context.Entregas.Where(e => e.IdLoteEntrega == id).ToList();

            if (!entregas.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<DtoImportacao>>(entregas));
        }

        /// <summary>
        /// Recebe os dados do arquivo excel para inserir na base.
        /// </summary>
        /// <param name="dtoArquivo"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PostEntregas(DtoArquivo dtoArquivo)
        {
            
            var listaEntregas = ExtrairEntregas(Convert.FromBase64String(dtoArquivo.Base64));

            var lote = new LoteEntregas()
            {
                NmArquivoLote = dtoArquivo.Nome,
                DtImportacao = DateTime.Now,
                NrRegistros = listaEntregas.Count,
                NrTotalProdutos = listaEntregas.Sum(e => e.QtdProduto),
                VlTotalImportado = listaEntregas.Sum(e => e.VlUnitario * e.QtdProduto),
                DtEntregaMenor = listaEntregas.Min(e => e.DtEntrega),
                Entregas = listaEntregas,
            };

            _context.LoteEntregas.Add(lote);
            _context.SaveChanges();

            return Ok(new { Id = lote.IdLoteEntrega });
        }


        /// <summary>
        /// Obtem os dados de entrega do excel.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        private List<Entregas> ExtrairEntregas(byte[] buffer)
        {
            MemoryStream excelStream = new MemoryStream(buffer);
            var lista = new List<Entregas>();

            try
            {
                using var wb = new XLWorkbook(excelStream);
                var workSheet = wb.Worksheet(1);

                if(workSheet.Tables.Count() == 0)
                    throw new ApiException($"Não foram encontrados dados de entregas no arquivo.");

                var dtt = workSheet.Table(0).AsNativeDataTable();

                var colunasEncontradas = dtt.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToArray();
                var colunasEsperadas = new string[] { "Data Entrega" , "Nome do Produto" , "Quantidade", "Valor Unitário" };
                var nrColunasIguais = colunasEncontradas.Intersect(colunasEsperadas).Count();

                if (nrColunasIguais != colunasEsperadas.Count())
                    throw new ApiException($"O cabecalho do arquivo não condiz com o esperado. Verifique se os seguintes cabeçalhos estão presentes: {string.Join(", ", colunasEsperadas)}.");

                if(dtt.Rows.Count == 0)
                    throw new ApiException($"Não foram encontrados dados de entregas no arquivo.");

                var listErrors = new List<ValidationFailure>();
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    var validator = new EntregasValidator($"Linha {i+2} : ");
                    var row = dtt.Rows[i];

                    var dtEntrega = row.Field<DateTime>("Data Entrega");
                    var nmProduto = row.Field<string>("Nome do Produto");
                    var qtd = row.Field<double>("Quantidade");
                    var vlUnitario = Convert.ToDecimal(row.Field<double>("Valor Unitário"));

                    Entregas entrega = new Entregas()
                    {
                        DtEntrega = dtEntrega,
                        NmProduto = nmProduto,
                        QtdProduto = Convert.ToInt32(qtd),
                        VlUnitario = decimal.Round(vlUnitario, 2)
                    };

                    var results = validator.Validate(entrega);
                    
                    if (!results.IsValid)
                        listErrors.AddRange(results.Errors);

                    lista.Add(entrega);
                }

                if (listErrors.Count > 0)
                    throw new ApiException("Foram encontrados dados fora das especificações.",listErrors);
            }
            catch (FileFormatException)
            {
                throw new ApiException("Formato inválido de arquivo.");
            }

            return lista;
        }
    }
}
