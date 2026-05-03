using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.BancoDados;
using SistemaAAPM.Models;
using System;
using System.Data;

namespace SistemaAAPM.Controllers
{
    [Authorize] // Bloqueia o acesso para quem não estiver logado
    [Route("eventos")] // Padroniza a URL base para /eventos
    public class EventosController : Controller
    {
        // -----------------------------------------------------------
        // SELECIONAR (Lista) - Acessado via GET: /eventos
        // -----------------------------------------------------------
        [HttpGet("")]
        public IActionResult Selecionar()
        {
            try
            {
                Eventos o_Eventos = new Eventos();
                DataTable dtEventos = o_Eventos.SelecionarTodos();
                return View("SelecionarView", dtEventos);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("SelecionarView");
            }
        }

        // -----------------------------------------------------------
        // INSERIR - EXIBIR (Tela) - Acessado via GET: /eventos/novo
        // ----------------------------------------------------------- 
        [HttpGet("novo")]
        public IActionResult InserirExibir()
        {
            try
            {
                EventosViewModel o_EventosVM = new EventosViewModel();
                return View("InserirExibirView", o_EventosVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("InserirExibirView");
            }
        }

        // -----------------------------------------------------------
        // INSERIR - PROCESSAR (Salvar) - Acessado via POST do formulário
        // ----------------------------------------------------------- 
        [HttpPost("novo")]
        public IActionResult InserirProcessar(EventosViewModel o_EventosVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Eventos o_Eventos = new Eventos();
                    o_Eventos.nome_evento = o_EventosVM.NomeEvento;
                    o_Eventos.descricao_evento = o_EventosVM.DescricaoEvento;
                    o_Eventos.data_evento = o_EventosVM.DataEvento;
                    o_Eventos.horario_evento = o_EventosVM.HorarioEvento;
                    o_Eventos.Inserir();

                    TempData["MsgSucesso"] = "Evento inserido com sucesso!";
                    return RedirectToAction("Selecionar");
                }

                return View("InserirExibirView", o_EventosVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("InserirExibirView", o_EventosVM);
            }
        }

        // -----------------------------------------------------------
        // ALTERAR - EXIBIR (Tela) - Acessado via GET: /eventos/editar/5
        // -----------------------------------------------------------
        [HttpGet("editar/{idEvento}")]
        public IActionResult AlterarExibir(int? idEvento)
        {
            try
            {
                Eventos o_Eventos = new Eventos();
                o_Eventos.id_evento = idEvento;
                DataTable pesqEvento = o_Eventos.SelecionarPorID();

                EventosViewModel o_EventosVM = new EventosViewModel();
                o_EventosVM.IdEvento = idEvento;
                o_EventosVM.NomeEvento = pesqEvento.Rows[0]["Nome_Evento"].ToString();

                // Verificação de valores que podem ser nulos
                if (pesqEvento.Rows[0]["Data_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DataEvento = DateTime.Parse(pesqEvento.Rows[0]["Data_Evento"].ToString());
                }
                if (pesqEvento.Rows[0]["Descricao_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DescricaoEvento = pesqEvento.Rows[0]["Descricao_Evento"].ToString();
                }
                if (pesqEvento.Rows[0]["Horario_Evento"] != DBNull.Value)
                {
                    o_EventosVM.HorarioEvento = pesqEvento.Rows[0]["Horario_Evento"].ToString();
                }

                return View("AlterarExibirView", o_EventosVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView");
            }
        }

        // -----------------------------------------------------------
        // ALTERAR - PROCESSAR (Salvar) - Acessado via POST do formulário
        // -----------------------------------------------------------
        [HttpPost("editar/{idEvento}")]
        public IActionResult AlterarProcessar(EventosViewModel o_EventosVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Eventos o_Eventos = new Eventos();
                    o_Eventos.id_evento = o_EventosVM.IdEvento;
                    o_Eventos.nome_evento = o_EventosVM.NomeEvento;
                    o_Eventos.descricao_evento = o_EventosVM.DescricaoEvento;
                    o_Eventos.data_evento = o_EventosVM.DataEvento;
                    o_Eventos.horario_evento = o_EventosVM.HorarioEvento;
                    o_Eventos.Alterar();

                    TempData["MsgSucesso"] = "Evento alterado com sucesso!";
                    return RedirectToAction("Selecionar");
                }

                return View("AlterarExibirView", o_EventosVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView", o_EventosVM);
            }
        }

        // -----------------------------------------------------------
        // EXCLUIR - EXIBIR (Tela Confirmar) - Acessado via GET: /eventos/excluir/5
        // ----------------------------------------------------------- 
        [HttpGet("excluir/{idEvento}")]
        public IActionResult ExcluirExibir(int? idEvento)
        {
            try
            {
                Eventos o_Eventos = new Eventos();
                o_Eventos.id_evento = idEvento;
                DataTable pesqEvento = o_Eventos.SelecionarPorID();

                EventosViewModel o_EventosVM = new EventosViewModel();
                o_EventosVM.IdEvento = idEvento;
                o_EventosVM.NomeEvento = pesqEvento.Rows[0]["Nome_Evento"].ToString();

                // Verificação de valores que podem ser nulos
                if (pesqEvento.Rows[0]["Data_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DataEvento = DateTime.Parse(pesqEvento.Rows[0]["Data_Evento"].ToString());
                }
                if (pesqEvento.Rows[0]["Descricao_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DescricaoEvento = pesqEvento.Rows[0]["Descricao_Evento"].ToString();
                }
                if (pesqEvento.Rows[0]["Horario_Evento"] != DBNull.Value)
                {
                    o_EventosVM.HorarioEvento = pesqEvento.Rows[0]["Horario_Evento"].ToString();
                }

                return View("ExcluirExibirView", o_EventosVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView");
            }
        }

        // -----------------------------------------------------------
        // EXCLUIR - PROCESSAR (Excluir no BD) - Acessado via POST do formulário
        // -----------------------------------------------------------
        [HttpPost("excluir/{idEvento}")]
        public IActionResult ExcluirProcessar(EventosViewModel o_EventosVM)
        {
            try
            {
                Eventos o_Eventos = new Eventos();
                o_Eventos.id_evento = o_EventosVM.IdEvento;
                o_Eventos.Excluir();

                TempData["MsgSucesso"] = "Evento excluido com sucesso!";
                return RedirectToAction("Selecionar");
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView", o_EventosVM);
            }
        }
    }
}