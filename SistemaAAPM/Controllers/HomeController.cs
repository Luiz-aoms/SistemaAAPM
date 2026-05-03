using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.BancoDados;
using SistemaAAPM.Models;
using System.Data;
using System.Diagnostics;

namespace SistemaAAPM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                Eventos o_Eventos = new Eventos();

                DataTable dtEventos = o_Eventos.SelecionarUltimas();

                return View("Index", dtEventos);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
