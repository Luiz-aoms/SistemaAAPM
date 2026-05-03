using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.Models;
using SistemaAAPM.BancoDados; // Importante para puxar as suas classes de banco
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace SistemaAAPM.Controllers
{
    [Authorize]
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            // 1. Instanciamos as suas classes do banco de dados
            Associados o_Associados = new Associados();
            Cursos o_Cursos = new Cursos();
            Salas o_Salas = new Salas();
            Eventos o_Eventos = new Eventos();

            // 2. Buscamos as tabelas e contamos quantas linhas (Rows.Count) cada uma tem
            var model = new DashboardViewModel
            {
                TotalAssociados = o_Associados.SelecionarTodos().Rows.Count,
                TotalCursos = o_Cursos.SelecionarTodos().Rows.Count,
                TotalSalas = o_Salas.SelecionarTodos().Rows.Count,
                TotalEventos = o_Eventos.SelecionarTodos().Rows.Count
            };

            // 3. Enviamos os números para a View
            return View(model);
        }
    }
}