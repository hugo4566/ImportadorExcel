using System.ComponentModel.DataAnnotations;

namespace CoreApi.DTO
{
    public class DtoArquivo
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Base64 { get; set; }
    }
}
