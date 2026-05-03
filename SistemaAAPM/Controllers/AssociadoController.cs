using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.Models;
using SistemaAAPM.BancoDados;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace SistemaAAPM.Controllers
{
    public class AssociadoController : Controller
    {

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

        //-----------------------------------------------------------
        // INSERIR - EXIBIR
        //----------------------------------------------------------- 
        public IActionResult InserirExibir()
        {
            try
            {
                AssociadoViewModel o_AssociadoVM = new AssociadoViewModel();

                //===================
                //   Cursos
                //===================
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

        private List<SelectListItem> ObterCurso()
        {
            try
            {
                // Busca os dados de cargo no banco de dados
                Cursos o_Cursos = new Cursos();
                DataTable pesqCursos = o_Cursos.SelecionarTodos();


                // Filtra os dados de ID e a sigla do Curso
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
                // Busca os dados de cargo no banco de dados
                Salas o_Salas = new Salas();
                DataTable pesqSalas = o_Salas.SelecionarTodos();


                // Filtra os dados de ID e nome do cargo
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

        //-----------------------------------------------------------
        // INSERIR - PROCESSAR
        //-----------------------------------------------------------
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



                //===================
                //   Cursos e Salas
                //===================
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

        //-----------------------------------------------------------
        // ALTERAR - EXIBIR
        //-----------------------------------------------------------
        public IActionResult AlterarExibir(int? idAssociado)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Associados o_Associados = new Associados();


                o_Associados.id_associado = idAssociado;
                DataTable pesqAssociado = o_Associados.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
                AssociadoViewModel o_AssociadoVM = new AssociadoViewModel();
                o_AssociadoVM.IdAssociados = idAssociado;
                o_AssociadoVM.Nome = pesqAssociado.Rows[0]["Nome"].ToString();
                o_AssociadoVM.Cpf = pesqAssociado.Rows[0]["Cpf"].ToString();
                o_AssociadoVM.Fone = pesqAssociado.Rows[0]["Fone"].ToString();
                o_AssociadoVM.IdCurso = int.Parse(pesqAssociado.Rows[0]["id_curso"].ToString());
                o_AssociadoVM.IdSala = int.Parse(pesqAssociado.Rows[0]["id_sala"].ToString());




                //---------------------------------------------
                // Verificação de valores que podem ser nulos
                //---------------------------------------------

                //Descrição
                if (pesqAssociado.Rows[0]["Senha_Armario"] != DBNull.Value)
                {
                    o_AssociadoVM.SenhaArmario = pesqAssociado.Rows[0]["Senha_Armario"].ToString();
                }



                //===================
                //   Curso e Salas
                //===================
                o_AssociadoVM.Cursos = ObterCurso();
                o_AssociadoVM.Salas = ObterSalas();

                //---------------------------------------------
                // Enviar a Model para a View
                //---------------------------------------------
                return View("AlterarExibirView", o_AssociadoVM);
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
        public IActionResult AlterarProcessar(AssociadoViewModel o_AssociadoVM)
        {
            try
            {
                // Se os campos foram validados 
                if (ModelState.IsValid)
                {
                    Associados o_Associados = new Associados();

                    //passando os valores digitados no form

                    o_Associados.id_associado = o_AssociadoVM.IdAssociados;
                    o_Associados.nome = o_AssociadoVM.Nome;
                    o_Associados.cpf = o_AssociadoVM.Cpf;
                    o_Associados.fone = o_AssociadoVM.Fone;
                    o_Associados.senha_armario = o_AssociadoVM.SenhaArmario;
                    o_Associados.id_curso = o_AssociadoVM.IdCurso;
                    o_Associados.id_sala = o_AssociadoVM.IdSala;

                    //chamando o método
                    o_Associados.Alterar();

                    TempData["MsgSucesso"] = "Associado alterado com sucesso!";

                    return RedirectToAction("Selecionar");

                }

                //===================
                //   Cursos e Salas
                //===================
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

        //-----------------------------------------------------------
        // EXCLUIR - EXIBIR
        //----------------------------------------------------------- 
        public IActionResult ExcluirExibir(int? idAssociado)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Associados o_Associado = new Associados();


                o_Associado.id_associado = idAssociado;
                DataTable pesqAssociado = o_Associado.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
                AssociadoViewModel o_AssociadoVM = new AssociadoViewModel();
                o_AssociadoVM.IdAssociados = idAssociado;
                o_AssociadoVM.Nome = pesqAssociado.Rows[0]["Nome"].ToString();
                o_AssociadoVM.Cpf = pesqAssociado.Rows[0]["Cpf"].ToString();
                o_AssociadoVM.Fone = pesqAssociado.Rows[0]["Fone"].ToString();
                o_AssociadoVM.IdCurso = int.Parse(pesqAssociado.Rows[0]["id_curso"].ToString());
                o_AssociadoVM.IdSala = int.Parse(pesqAssociado.Rows[0]["id_sala"].ToString());




                //---------------------------------------------
                // Verificação de valores que podem ser nulos
                //---------------------------------------------

                //Descrição
                if (pesqAssociado.Rows[0]["Senha_Armario"] != DBNull.Value)
                {
                    o_AssociadoVM.SenhaArmario = pesqAssociado.Rows[0]["Senha_Armario"].ToString();
                }

                //===================
                //   Cursos e Salas
                //===================
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

        //-----------------------------------------------------------
        // EXCLUIR - PROCESSAR
        //-----------------------------------------------------------
        public IActionResult ExcluirProcessar(AssociadoViewModel o_AssociadoVM)
        {
            try
            {

                Associados o_Associado = new Associados();

                //passando o IdColaborador da Model para a classe do BD
                o_Associado.id_associado = o_AssociadoVM.IdAssociados;

                //chamando o método
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

    }
}
