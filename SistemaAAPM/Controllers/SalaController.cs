using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.Models;
using SistemaAAPM.BancoDados;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System;

namespace SistemaAAPM.Controllers
{
    [Authorize] // Bloqueia TODAS as ações para quem não está logado
    [Route("salas")] // Deixa a URL base bonitinha: /salas
    public class SalaController : Controller
    {
        // -----------------------------------------------------------
        // SELECIONAR (Ver lista) - Acessado via GET: /salas
        // -----------------------------------------------------------
        [HttpGet("")]
        public IActionResult Selecionar()
        {
            try
            {
                Salas o_Salas = new Salas();
                DataTable dtSalas = o_Salas.SelecionarTodos();
                return View("SelecionarView", dtSalas);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("SelecionarView");
            }
        }

        // -----------------------------------------------------------
        // INSERIR - EXIBIR (Abre a tela) - Acessado via GET: /salas/nova
        // ----------------------------------------------------------- 
        [HttpGet("nova")]
        public IActionResult InserirExibir()
        {
            try
            {
                SalaVM o_SalaVM = new SalaVM();
                return View("InserirExibirView", o_SalaVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("InserirExibirView");
            }
        }

        // -----------------------------------------------------------
        // INSERIR - PROCESSAR (Salva no BD) - Acessado via POST do form
        // ----------------------------------------------------------- 
        [HttpPost("nova")]
        public IActionResult InserirProcessar(SalaVM o_SalaVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Salas o_Salas = new Salas();
                    o_Salas.nmr_sala = o_SalaVM.NmrSala;
                    o_Salas.bloco_sala = o_SalaVM.BlocoSala;
                    o_Salas.Inserir();

                    TempData["MsgSucesso"] = "Sala inserida com sucesso!";
                    return RedirectToAction("Selecionar");
                }
                return View("InserirExibirView", o_SalaVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("InserirExibirView", o_SalaVM);
            }
        }

        // -----------------------------------------------------------
        // ALTERAR - EXIBIR (Abre a tela) - Acessado via GET: /salas/editar/5
        // -----------------------------------------------------------
        [HttpGet("editar/{idSala}")] // Mudei de PUT para GET (Navegador usa GET para abrir tela)
        public IActionResult AlterarExibir(int idSala)
        {
            try
            {
                Salas o_Salas = new Salas();
                o_Salas.id_sala = idSala;
                DataTable pesqCurso = o_Salas.SelecionarPorID();

                SalaVM o_SalaVM = new SalaVM();
                o_SalaVM.IdSala = idSala;
                o_SalaVM.NmrSala = int.Parse(pesqCurso.Rows[0]["nmr_sala"].ToString());
                o_SalaVM.BlocoSala = pesqCurso.Rows[0]["bloco_sala"].ToString();

                return View("AlterarExibirView", o_SalaVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView");
            }
        }

        // -----------------------------------------------------------
        // ALTERAR - PROCESSAR (Salva no BD) - Acessado via POST do form
        // -----------------------------------------------------------
        [HttpPost("editar/{idSala}")]
        public IActionResult AlterarProcessar(SalaVM o_SalaVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Salas o_Salas = new Salas();
                    o_Salas.id_sala = o_SalaVM.IdSala;
                    o_Salas.nmr_sala = o_SalaVM.NmrSala;
                    o_Salas.bloco_sala = o_SalaVM.BlocoSala;
                    o_Salas.Alterar();

                    TempData["MsgSucesso"] = "Sala alterada com sucesso!";
                    return RedirectToAction("Selecionar");
                }
                return View("AlterarExibirView", o_SalaVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView", o_SalaVM);
            }
        }

        // -----------------------------------------------------------
        // EXCLUIR - EXIBIR (Abre tela confirm) - Acessado via GET: /salas/excluir/5
        // ----------------------------------------------------------- 
        [HttpGet("excluir/{idSala}")] // Mudei de DELETE para GET
        public IActionResult ExcluirExibir(int idSala)
        {
            try
            {
                Salas o_Salas = new Salas();
                o_Salas.id_sala = idSala;
                DataTable pesqSala = o_Salas.SelecionarPorID();

                SalaVM o_SalaVM = new SalaVM();
                o_SalaVM.IdSala = idSala;
                o_SalaVM.NmrSala = int.Parse(pesqSala.Rows[0]["nmr_sala"].ToString());
                o_SalaVM.BlocoSala = pesqSala.Rows[0]["bloco_sala"].ToString();

                return View("ExcluirExibirView", o_SalaVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView");
            }
        }

        // -----------------------------------------------------------
        // EXCLUIR - PROCESSAR (Exclui no BD) - Acessado via POST do form
        // -----------------------------------------------------------
        [HttpPost("excluir/{idSala}")]
        public IActionResult ExcluirProcessar(SalaVM o_SalaVM)
        {
            try
            {
                Salas o_Salas = new Salas();
                o_Salas.id_sala = o_SalaVM.IdSala;
                o_Salas.Excluir();

                TempData["MsgSucesso"] = "Sala excluida com sucesso!";
                return RedirectToAction("Selecionar");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE constraint") || ex.Message.Contains("conflicted"))
                {
                    TempData["MsgErro"] = "Não é possível excluir! Existem associados vinculados a este registro. Remova os associados primeiro.";
                }
                else
                {
                    // Se for outro tipo de erro, mostra a mensagem original
                    TempData["MsgErro"] = $"Erro inesperado: {ex.Message}";
                }    
                return View("ExcluirExibirView", o_SalaVM);
            }
        }
    }
}