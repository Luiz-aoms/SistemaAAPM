using System.ComponentModel.DataAnnotations;

namespace SistemaAAPM.Models
{
    public class EventosViewModel
    {
        public int? IdEvento {  get; set; }

        [Required(ErrorMessage = "Digite o Nome do Evento")]
        public string? NomeEvento { get; set; }
        public string? DescricaoEvento {  get; set; }
        public DateTime? DataEvento { get; set; }
        public string? HorarioEvento { get; set; }
    }
}
