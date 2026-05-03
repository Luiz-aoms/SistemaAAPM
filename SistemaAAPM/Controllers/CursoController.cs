using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.Models;
using SistemaAAPM.BancoDados;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace SistemaAAPM.Controllers
{
    public class CursoController : Controller
    {
        public IActionResult Selecionar()
        {
            try
            {
                Cursos  o_Curso = new Cursos();

                DataTable dtCursos = o_Curso.SelecionarTodos();

                return View("SelecionarView", dtCursos);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("SelecionarView");
            }
        }

        //-----------------------------------------------------------
        // INSERIR - EXIBIR
        //----------------------------------------------------------- 
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

        //-----------------------------------------------------------
        // ALTERAR - EXIBIR
        //-----------------------------------------------------------
        public IActionResult AlterarExibir(int? idCurso)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Cursos o_Cursos = new Cursos();


                o_Cursos.id_curso = idCurso;
                DataTable pesqCurso = o_Cursos.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
                CursoVM o_CursoVM = new CursoVM();
                o_CursoVM.IdCursos = idCurso;
                o_CursoVM.NomeCursos = pesqCurso.Rows[0]["Nome_Curso"].ToString();
                o_CursoVM.SiglaCurso = pesqCurso.Rows[0]["Sigla_Curso"].ToString();




                //---------------------------------------------
                // Verificação de valores que podem ser nulos
                //---------------------------------------------

                //Descrição
                if (pesqCurso.Rows[0]["Data_inicio"] != DBNull.Value)
                {
                    o_CursoVM.DataInicio = DateTime.Parse(pesqCurso.Rows[0]["Data_Inicio"].ToString());
                }
                if (pesqCurso.Rows[0]["Data_Termino"] != DBNull.Value)
                {
                    o_CursoVM.DataTermino = DateTime.Parse(pesqCurso.Rows[0]["Data_Termino"].ToString());
                }


                //---------------------------------------------
                // Enviar a Model para a View
                //---------------------------------------------
                return View("AlterarExibirView", o_CursoVM);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("AlterarExibirView");
            }
        }

        //-----------------------------------------------------------
        // ALTERAR - PROCESSAR
        //-----------------------------------------------------------
        public IActionResult AlterarProcessar(CursoVM o_CursoVM)
        {
            try
            {
                // Se os campos foram validados 
                if (ModelState.IsValid)
                {
                    Cursos o_Cursos = new Cursos();

                    //passando os valores digitados no form

                    o_Cursos.id_curso = o_CursoVM.IdCursos;
                    o_Cursos.nome_curso = o_CursoVM.NomeCursos;
                    o_Cursos.sigla_curso = o_CursoVM.SiglaCurso;
                    o_Cursos.data_inicio = o_CursoVM.DataInicio;
                    o_Cursos.data_termino = o_CursoVM.DataTermino;

                    //chamando o método
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
        //-----------------------------------------------------------
        // EXCLUIR - EXIBIR
        //----------------------------------------------------------- 
        public IActionResult ExcluirExibir(int? idCurso)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Cursos o_Cursos = new Cursos();


                o_Cursos.id_curso = idCurso;
                DataTable pesqCurso = o_Cursos.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
                CursoVM o_CursoVM = new CursoVM();
                o_CursoVM.IdCursos = idCurso;
                o_CursoVM.NomeCursos = pesqCurso.Rows[0]["Nome_Curso"].ToString();
                o_CursoVM.SiglaCurso = pesqCurso.Rows[0]["Sigla_Curso"].ToString();




                //---------------------------------------------
                // Verificação de valores que podem ser nulos
                //---------------------------------------------

                //Descrição
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
        public IActionResult ExcluirProcessar(CursoVM o_CursoVM)
        {
            try
            {

                Cursos o_Cursos = new Cursos();

                //passando o IdColaborador da Model para a classe do BD
                o_Cursos.id_curso = o_CursoVM.IdCursos;

                //chamando o método
                o_Cursos.Excluir();

                TempData["MsgSucesso"] = "Curso excluido com sucesso!";

                return RedirectToAction("Selecionar");

            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView", o_CursoVM);
            }

        }
    }
}
