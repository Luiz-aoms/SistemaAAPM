using System.ComponentModel.DataAnnotations;

namespace SistemaAAPM.Models
{
    public class SalaVM
    {
        public int? IdSala {  get; set; }

        [Required(ErrorMessage = "Informe o Número da Sala")]
        public int? NmrSala { get; set; }

        [Required(ErrorMessage = "Informe o Bloco da Sala")]
        public string? BlocoSala {  get; set; }

    }
}
