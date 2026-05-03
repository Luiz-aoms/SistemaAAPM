using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaAAPM.Models
{
    public class CursoVM
    {

        public int? IdCursos {  get; set; }

        [Required(ErrorMessage = "Digite o NOME do Curso.")]
        public string? NomeCursos { get; set; }

        [Required(ErrorMessage = "Digite a SIGLA do Curso.")]
        public string? SiglaCurso { get; set; }

        [Required(ErrorMessage = "Adicione uma Data de Inicio.")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Adicione uma Data de Termino.")]
        public DateTime? DataTermino { get; set; }

    }
}
