using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaAAPM.Models
{
    public class AssociadoViewModel
    {

        public int? IdAssociados {  get; set; }

        [Required(ErrorMessage = "Digite o NOME do Associado.")]
        [MaxLength(100)]
        public string? Nome {  get; set; }

        [Required(ErrorMessage = "Digite o CPF do Associado.")]
        [MaxLength(50)]
        public string? Cpf {  get; set; }

        public int? IdCurso {  get; set; }

        public string? SenhaArmario { get; set; }

        [Required(ErrorMessage = "Digite a Data de Nascimento do Associado.")]
        [MaxLength(50)]
        public string? Fone {  get; set; }
        public int IdSala { get; set; }

        

        public List<SelectListItem>? Cursos {  get; set; } 

        public List<SelectListItem>? Salas { get; set; }


    }
}
