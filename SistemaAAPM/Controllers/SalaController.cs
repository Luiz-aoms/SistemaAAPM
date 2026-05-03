using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.Models;
using SistemaAAPM.BancoDados;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace SistemaAAPM.Controllers
{
    public class SalaController : Controller
    {
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

        //-----------------------------------------------------------
        // INSERIR - EXIBIR
        //----------------------------------------------------------- 
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

        //-----------------------------------------------------------
        // ALTERAR - EXIBIR
        //-----------------------------------------------------------
        public IActionResult AlterarExibir(int? idSala)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Salas o_Salas = new Salas();


                o_Salas.id_sala = idSala;
                DataTable pesqCurso = o_Salas.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
                SalaVM o_SalaVM = new SalaVM();
                o_SalaVM.IdSala = idSala;
                o_SalaVM.NmrSala = int.Parse(pesqCurso.Rows[0]["nmr_sala"].ToString());
                o_SalaVM.BlocoSala = pesqCurso.Rows[0]["bloco_sala"].ToString();


                //---------------------------------------------
                // Enviar a Model para a View
                //---------------------------------------------
                return View("AlterarExibirView", o_SalaVM);
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
        public IActionResult AlterarProcessar(SalaVM o_SalaVM)
        {
            try
            {
                // Se os campos foram validados 
                if (ModelState.IsValid)
                {
                    Salas o_Salas = new Salas();

                    //passando os valores digitados no form

                    o_Salas.id_sala = o_SalaVM.IdSala;
                    o_Salas.nmr_sala = o_SalaVM.NmrSala;
                    o_Salas.bloco_sala = o_SalaVM.BlocoSala;

                    //chamando o método
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

        //-----------------------------------------------------------
        // EXCLUIR - EXIBIR
        //----------------------------------------------------------- 
        public IActionResult ExcluirExibir(int? idSala)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Salas o_Salas = new Salas();


                o_Salas.id_sala = idSala;
                DataTable pesqSala = o_Salas.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
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
        public IActionResult ExcluirProcessar(SalaVM o_SalaVM)
        {
            try
            {

                Salas o_Salas = new Salas();

                //passando o IdColaborador da Model para a classe do BD
                o_Salas.id_sala = o_SalaVM.IdSala;

                //chamando o método
                o_Salas.Excluir();

                TempData["MsgSucesso"] = "Sala excluida com sucesso!";

                return RedirectToAction("Selecionar");

            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView", o_SalaVM);
            }

        }
    }
}
