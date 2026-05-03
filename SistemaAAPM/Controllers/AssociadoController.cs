using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.Models;
using SistemaAAPM.BancoDados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaAAPM.Controllers
{
    [Authorize] // Protege todas as rotas deste Controller
    [Route("associados")] // Padroniza a URL base para /associados
    public class AssociadoController : Controller
    {
        // -----------------------------------------------------------
        // SELECIONAR (Lista) - Acessado via GET: /associados
        // -----------------------------------------------------------
        [HttpGet("")]
        public IActionResult Selecionar()
        {
            try
            {
                Associados o_Associado = new Associados();
                DataTable dtAssociado = o_Associado.SelecionarTodos();
                return View("SelecionarView", dtAssociado);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("SelecionarView");
            }
        }

        // -----------------------------------------------------------
        // INSERIR - EXIBIR (Tela) - Acessado via GET: /associados/novo
        // ----------------------------------------------------------- 
        [HttpGet("novo")]
        public IActionResult InserirExibir()
        {
            try
            {
                AssociadoViewModel o_AssociadoVM = new AssociadoViewModel();

                o_AssociadoVM.Cursos = ObterCurso();
                o_AssociadoVM.Salas = ObterSalas();

                return View("InserirExibirView", o_AssociadoVM);
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
        public IActionResult InserirProcessar(AssociadoViewModel o_AssociadoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Associados o_Associado = new Associados();

                    o_Associado.nome = o_AssociadoVM.Nome;
                    o_Associado.cpf = o_AssociadoVM.Cpf;
                    o_Associado.senha_armario = o_AssociadoVM.SenhaArmario;
                    o_Associado.id_curso = o_AssociadoVM.IdCurso;
                    o_Associado.fone = o_AssociadoVM.Fone;
                    o_Associado.id_sala = o_AssociadoVM.IdSala;
                    o_Associado.Inserir();

                    TempData["MsgSucesso"] = "Associado inserido com sucesso!";
                    return RedirectToAction("Selecionar");
                }

                o_AssociadoVM.Cursos = ObterCurso();
                o_AssociadoVM.Salas = ObterSalas();

                return View("InserirExibirView", o_AssociadoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("InserirExibirView", o_AssociadoVM);
            }
        }

        // -----------------------------------------------------------
        // ALTERAR - EXIBIR (Tela) - Acessado via GET: /associados/editar/5
        // -----------------------------------------------------------
        [HttpGet("editar/{idAssociado}")]
        public IActionResult AlterarExibir(int? idAssociado)
        {
            try
            {
                Associados o_Associados = new Associados();
                o_Associados.id_associado = idAssociado;
                DataTable pesqAssociado = o_Associados.SelecionarPorID();

                AssociadoViewModel o_AssociadoVM = new AssociadoViewModel();
                o_AssociadoVM.IdAssociados = idAssociado;
                o_AssociadoVM.Nome = pesqAssociado.Rows[0]["Nome"].ToString();
                o_AssociadoVM.Cpf = pesqAssociado.Rows[0]["Cpf"].ToString();
                o_AssociadoVM.Fone = pesqAssociado.Rows[0]["Fone"].ToString();
                o_AssociadoVM.IdCurso = int.Parse(pesqAssociado.Rows[0]["id_curso"].ToString());
                o_AssociadoVM.IdSala = int.Parse(pesqAssociado.Rows[0]["id_sala"].ToString());

                if (pesqAssociado.Rows[0]["Senha_Armario"] != DBNull.Value)
                {
                    o_AssociadoVM.SenhaArmario = pesqAssociado.Rows[0]["Senha_Armario"].ToString();
                }

                o_AssociadoVM.Cursos = ObterCurso();
                o_AssociadoVM.Salas = ObterSalas();

                return View("AlterarExibirView", o_AssociadoVM);
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
        [HttpPost("editar/{idAssociado}")]
        public IActionResult AlterarProcessar(AssociadoViewModel o_AssociadoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Associados o_Associados = new Associados();

                    o_Associados.id_associado = o_AssociadoVM.IdAssociados;
                    o_Associados.nome = o_AssociadoVM.Nome;
                    o_Associados.cpf = o_AssociadoVM.Cpf;
                    o_Associados.fone = o_AssociadoVM.Fone;
                    o_Associados.senha_armario = o_AssociadoVM.SenhaArmario;
                    o_Associados.id_curso = o_AssociadoVM.IdCurso;
                    o_Associados.id_sala = o_AssociadoVM.IdSala;

                    o_Associados.Alterar();

                    TempData["MsgSucesso"] = "Associado alterado com sucesso!";
                    return RedirectToAction("Selecionar");
                }

                o_AssociadoVM.Cursos = ObterCurso();
                o_AssociadoVM.Salas = ObterSalas();

                return View("AlterarExibirView", o_AssociadoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView", o_AssociadoVM);
            }
        }

        // -----------------------------------------------------------
        // EXCLUIR - EXIBIR (Tela Confirmar) - Acessado via GET: /associados/excluir/5
        // ----------------------------------------------------------- 
        [HttpGet("excluir/{idAssociado}")]
        public IActionResult ExcluirExibir(int? idAssociado)
        {
            try
            {
                Associados o_Associado = new Associados();
                o_Associado.id_associado = idAssociado;
                DataTable pesqAssociado = o_Associado.SelecionarPorID();

                AssociadoViewModel o_AssociadoVM = new AssociadoViewModel();
                o_AssociadoVM.IdAssociados = idAssociado;
                o_AssociadoVM.Nome = pesqAssociado.Rows[0]["Nome"].ToString();
                o_AssociadoVM.Cpf = pesqAssociado.Rows[0]["Cpf"].ToString();
                o_AssociadoVM.Fone = pesqAssociado.Rows[0]["Fone"].ToString();
                o_AssociadoVM.IdCurso = int.Parse(pesqAssociado.Rows[0]["id_curso"].ToString());
                o_AssociadoVM.IdSala = int.Parse(pesqAssociado.Rows[0]["id_sala"].ToString());

                if (pesqAssociado.Rows[0]["Senha_Armario"] != DBNull.Value)
                {
                    o_AssociadoVM.SenhaArmario = pesqAssociado.Rows[0]["Senha_Armario"].ToString();
                }

                o_AssociadoVM.Cursos = ObterCurso();
                o_AssociadoVM.Salas = ObterSalas();

                return View("ExcluirExibirView", o_AssociadoVM);
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
        [HttpPost("excluir/{idAssociado}")]
        public IActionResult ExcluirProcessar(AssociadoViewModel o_AssociadoVM)
        {
            try
            {
                Associados o_Associado = new Associados();
                o_Associado.id_associado = o_AssociadoVM.IdAssociados;
                o_Associado.Excluir();

                TempData["MsgSucesso"] = "Associado excluido com sucesso!";
                return RedirectToAction("Selecionar");
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView", o_AssociadoVM);
            }
        }

        // -----------------------------------------------------------
        // MÉTODOS PRIVADOS (Não precisam de rotas, são apenas auxiliares)
        // -----------------------------------------------------------
        private List<SelectListItem> ObterCurso()
        {
            try
            {
                Cursos o_Cursos = new Cursos();
                DataTable pesqCursos = o_Cursos.SelecionarTodos();

                List<SelectListItem> Cursos = (from DataRow dr in pesqCursos.Rows
                                               select new SelectListItem()
                                               {
                                                   Value = dr["id_curso"].ToString(),
                                                   Text = dr["sigla_curso"].ToString(),
                                               }).ToList();

                return Cursos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<SelectListItem> ObterSalas()
        {
            try
            {
                Salas o_Salas = new Salas();
                DataTable pesqSalas = o_Salas.SelecionarTodos();

                List<SelectListItem> Salas = (from DataRow dr in pesqSalas.Rows
                                              select new SelectListItem()
                                              {
                                                  Value = dr["id_sala"].ToString(),
                                                  Text = dr["nmr_sala"].ToString(),
                                              }).ToList();

                return Salas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}