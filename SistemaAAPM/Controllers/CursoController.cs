using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.Models;
using SistemaAAPM.BancoDados;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System;

namespace SistemaAAPM.Controllers
{
    [Authorize]
    [Route("cursos")] // Deixa a URL base como /cursos
    public class CursoController : Controller
    {
        // -----------------------------------------------------------
        // SELECIONAR (Lista) - Acessado via GET: /cursos
        // -----------------------------------------------------------
        [HttpGet("")]
        public IActionResult Selecionar()
        {
            try
            {
                Cursos o_Curso = new Cursos();
                DataTable dtCursos = o_Curso.SelecionarTodos();
                return View("SelecionarView", dtCursos);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("SelecionarView");
            }
        }

        // -----------------------------------------------------------
        // INSERIR - EXIBIR (Tela) - Acessado via GET: /cursos/novo
        // ----------------------------------------------------------- 
        [HttpGet("novo")]
        public IActionResult InserirExibir()
        {
            try
            {
                CursoVM o_CursoVM = new CursoVM();
                return View("InserirExibirView", o_CursoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("InserirExibirView");
            }
        }

        // -----------------------------------------------------------
        // INSERIR - PROCESSAR (Salvar) - Acessado via POST do form
        // ----------------------------------------------------------- 
        [HttpPost("novo")]
        public IActionResult InserirProcessar(CursoVM o_CursoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cursos o_Cursos = new Cursos();
                    o_Cursos.nome_curso = o_CursoVM.NomeCursos;
                    o_Cursos.sigla_curso = o_CursoVM.SiglaCurso;
                    o_Cursos.data_inicio = o_CursoVM.DataInicio;
                    o_Cursos.data_termino = o_CursoVM.DataTermino;
                    o_Cursos.Inserir();

                    TempData["MsgSucesso"] = "Curso inserido com sucesso!";
                    return RedirectToAction("Selecionar");
                }

                return View("InserirExibirView", o_CursoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("InserirExibirView", o_CursoVM);
            }
        }

        // -----------------------------------------------------------
        // ALTERAR - EXIBIR (Tela) - Acessado via GET: /cursos/editar/5
        // -----------------------------------------------------------
        [HttpGet("editar/{idCurso}")]
        public IActionResult AlterarExibir(int? idCurso)
        {
            try
            {
                Cursos o_Cursos = new Cursos();
                o_Cursos.id_curso = idCurso;
                DataTable pesqCurso = o_Cursos.SelecionarPorID();

                CursoVM o_CursoVM = new CursoVM();
                o_CursoVM.IdCursos = idCurso;
                o_CursoVM.NomeCursos = pesqCurso.Rows[0]["Nome_Curso"].ToString();
                o_CursoVM.SiglaCurso = pesqCurso.Rows[0]["Sigla_Curso"].ToString();

                if (pesqCurso.Rows[0]["Data_inicio"] != DBNull.Value)
                {
                    o_CursoVM.DataInicio = DateTime.Parse(pesqCurso.Rows[0]["Data_Inicio"].ToString());
                }
                if (pesqCurso.Rows[0]["Data_Termino"] != DBNull.Value)
                {
                    o_CursoVM.DataTermino = DateTime.Parse(pesqCurso.Rows[0]["Data_Termino"].ToString());
                }

                return View("AlterarExibirView", o_CursoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView");
            }
        }

        // -----------------------------------------------------------
        // ALTERAR - PROCESSAR (Salvar) - Acessado via POST do form
        // -----------------------------------------------------------
        [HttpPost("editar/{idCurso}")]
        public IActionResult AlterarProcessar(CursoVM o_CursoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cursos o_Cursos = new Cursos();
                    o_Cursos.id_curso = o_CursoVM.IdCursos;
                    o_Cursos.nome_curso = o_CursoVM.NomeCursos;
                    o_Cursos.sigla_curso = o_CursoVM.SiglaCurso;
                    o_Cursos.data_inicio = o_CursoVM.DataInicio;
                    o_Cursos.data_termino = o_CursoVM.DataTermino;
                    o_Cursos.Alterar();

                    TempData["MsgSucesso"] = "Curso alterado com sucesso!";
                    return RedirectToAction("Selecionar");
                }

                return View("AlterarExibirView", o_CursoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView", o_CursoVM);
            }
        }

        // -----------------------------------------------------------
        // EXCLUIR - EXIBIR (Tela Confirmar) - Acessado via GET: /cursos/excluir/5
        // ----------------------------------------------------------- 
        [HttpGet("excluir/{idCurso}")]
        public IActionResult ExcluirExibir(int? idCurso)
        {
            try
            {
                Cursos o_Cursos = new Cursos();
                o_Cursos.id_curso = idCurso;
                DataTable pesqCurso = o_Cursos.SelecionarPorID();

                CursoVM o_CursoVM = new CursoVM();
                o_CursoVM.IdCursos = idCurso;
                o_CursoVM.NomeCursos = pesqCurso.Rows[0]["Nome_Curso"].ToString();
                o_CursoVM.SiglaCurso = pesqCurso.Rows[0]["Sigla_Curso"].ToString();

                if (pesqCurso.Rows[0]["Data_inicio"] != DBNull.Value)
                {
                    o_CursoVM.DataInicio = DateTime.Parse(pesqCurso.Rows[0]["Data_Inicio"].ToString());
                }
                if (pesqCurso.Rows[0]["Data_Termino"] != DBNull.Value)
                {
                    o_CursoVM.DataTermino = DateTime.Parse(pesqCurso.Rows[0]["Data_Termino"].ToString());
                }

                return View("ExcluirExibirView", o_CursoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView");
            }
        }

        // -----------------------------------------------------------
        // EXCLUIR - PROCESSAR (Excluir no BD) - Acessado via POST do form
        // -----------------------------------------------------------
        [HttpPost("excluir/{idCurso}")]
        public IActionResult ExcluirProcessar(CursoVM o_CursoVM)
        {
            try
            {
                Cursos o_Cursos = new Cursos();
                o_Cursos.id_curso = o_CursoVM.IdCursos;
                o_Cursos.Excluir();

                TempData["MsgSucesso"] = "Curso excluido com sucesso!";
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
                return View("ExcluirExibirView", o_CursoVM);
            }
        }
    }
}