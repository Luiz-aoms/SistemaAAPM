using Microsoft.AspNetCore.Mvc;
using SistemaAAPM.BancoDados;
using SistemaAAPM.Models;
using System.Data;

namespace SistemaAAPM.Controllers
{
    public class EventosController : Controller
    {
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

        //-----------------------------------------------------------
        // INSERIR - EXIBIR
        //----------------------------------------------------------- 
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

        //-----------------------------------------------------------
        // ALTERAR - EXIBIR
        //-----------------------------------------------------------
        public IActionResult AlterarExibir(int? idEvento)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Eventos o_Eventos = new Eventos();


                o_Eventos.id_evento = idEvento;
                DataTable pesqEvento = o_Eventos.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
                EventosViewModel o_EventosVM = new EventosViewModel();
                o_EventosVM.IdEvento = idEvento;
                o_EventosVM.NomeEvento = pesqEvento.Rows[0]["Nome_Evento"].ToString();




                //---------------------------------------------
                // Verificação de valores que podem ser nulos
                //---------------------------------------------

                //Descrição
                if (pesqEvento.Rows[0]["Data_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DataEvento = DateTime.Parse(pesqEvento.Rows[0]["Data_Evento"].ToString());
                }
                if (pesqEvento.Rows[0]["Descricao_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DescricaoEvento = (pesqEvento.Rows[0]["Descricao_Evento"].ToString());
                }
                if (pesqEvento.Rows[0]["Horario_Evento"] != DBNull.Value)
                {
                    o_EventosVM.HorarioEvento = (pesqEvento.Rows[0]["Horario_Evento"].ToString());
                }


                //---------------------------------------------
                // Enviar a Model para a View
                //---------------------------------------------
                return View("AlterarExibirView", o_EventosVM);
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
        public IActionResult AlterarProcessar(EventosViewModel o_EventosVM)
        {
            try
            {
                // Se os campos foram validados 
                if (ModelState.IsValid)
                {
                    Eventos o_Eventos = new Eventos();

                    //passando os valores digitados no form

                    o_Eventos.id_evento = o_EventosVM.IdEvento;
                    o_Eventos.nome_evento = o_EventosVM.NomeEvento;
                    o_Eventos.descricao_evento = o_EventosVM.DescricaoEvento;
                    o_Eventos.data_evento = o_EventosVM.DataEvento;
                    o_Eventos.horario_evento = o_EventosVM.HorarioEvento;

                    //chamando o método
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

        //-----------------------------------------------------------
        // EXCLUIR - EXIBIR
        //----------------------------------------------------------- 
        public IActionResult ExcluirExibir(int? idEvento)
        {
            try
            {

                //-----------------------------------------------
                //Buscar dados do colaborador no banco de dados
                //-----------------------------------------------
                Eventos o_Eventos = new Eventos();


                o_Eventos.id_evento = idEvento;
                DataTable pesqEvento = o_Eventos.SelecionarPorID();

                //---------------------------------------------
                // Preencher a Model com o Banco de Dados
                //---------------------------------------------
                EventosViewModel o_EventosVM = new EventosViewModel();
                o_EventosVM.IdEvento = idEvento;
                o_EventosVM.NomeEvento = pesqEvento.Rows[0]["Nome_Evento"].ToString();




                //---------------------------------------------
                // Verificação de valores que podem ser nulos
                //---------------------------------------------

                //Descrição
                if (pesqEvento.Rows[0]["Data_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DataEvento = DateTime.Parse(pesqEvento.Rows[0]["Data_Evento"].ToString());
                }
                if (pesqEvento.Rows[0]["Descricao_Evento"] != DBNull.Value)
                {
                    o_EventosVM.DescricaoEvento = (pesqEvento.Rows[0]["Descricao_Evento"].ToString());
                }
                if (pesqEvento.Rows[0]["Horario_Evento"] != DBNull.Value)
                {
                    o_EventosVM.HorarioEvento = (pesqEvento.Rows[0]["Horario_Evento"].ToString());
                }



                return View("ExcluirExibirView", o_EventosVM);

            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Erro: {ex.Message}";
                return View("ExcluirExibirView");
            }

        }
        public IActionResult ExcluirProcessar(EventosViewModel o_EventosVM)
        {
            try
            {

                Eventos o_Eventos = new Eventos();

                //passando o IdColaborador da Model para a classe do BD
                o_Eventos.id_evento = o_EventosVM.IdEvento;

                //chamando o método
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
