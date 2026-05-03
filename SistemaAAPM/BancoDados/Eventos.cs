using Humanizer;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAAPM.BancoDados
{
    public class Eventos
    {
        public int? id_evento {  get; set; }
        public string? nome_evento { get; set; }
        public string? descricao_evento { get; set; }
        public DateTime? data_evento { get; set; }
        public string? horario_evento { get; set; }


        SqlConnection con;


        public Eventos()
        {
            try
            {
                // Ler o arquivo de config
                IConfigurationRoot o_Config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile(@".\appsettings.json")
                   .Build();

                string strConexao = o_Config.GetConnectionString(@"Default");

                // Prepara a conexão com o BD
                con = new SqlConnection(strConexao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //--------------------------------
        // Métodos
        //--------------------------------

        public void Inserir()
        {

            try
            {
                string cmdSQL = "Insert Into tb_eventos" +
                    "(nome_evento, descricao_evento, data_evento, horario_evento ) " +
                    "Values(@NomeEvento, @DescricaoEvento, @DataEvento, @HorarioEvento)";

                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@NomeEvento", nome_evento);
                cmd.Parameters.AddWithValue("@DescricaoEvento", descricao_evento);
                cmd.Parameters.AddWithValue("@DataEvento", data_evento);
                cmd.Parameters.AddWithValue("@HorarioEvento", horario_evento);

                // Abre a conexão com o BD
                con.Open();

                // Executa o comando SQL
                cmd.ExecuteNonQuery();

                // Fecha a conexão com o BD
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void Alterar()
        {
            try
            {
                // Prapara o comando SQL
                string cmdSQL = "Update tb_eventos Set nome_evento = @NomeEvento, descricao_evento = @DescricaoEvento, data_evento = @DataEvento, horario_evento = @HorarioEvento " +
                                 "Where id_evento = @IdEvento";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdEvento", id_evento);
                cmd.Parameters.AddWithValue("@NomeEvento", nome_evento);
                cmd.Parameters.AddWithValue("@DescricaoEvento", descricao_evento);
                cmd.Parameters.AddWithValue("@DataEvento", data_evento);
                cmd.Parameters.AddWithValue("@HorarioEvento", horario_evento);

                // Abre a conexão com o BD
                con.Open();

                // Executar o comando SQL
                cmd.ExecuteNonQuery();

                // Fecha a conexão com o BD
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Excluir()
        {
            try
            {
                // Prapara o comando SQL
                string cmdSQL = "Delete From tb_eventos Where id_evento = @IdEvento";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdEvento", id_evento);

                // Abre a conexão com o BD
                con.Open();

                // Executar o comando SQL
                cmd.ExecuteNonQuery();

                // Fecha a conexão com o BD
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SelecionarTodos()
        {
            try
            {
                // Prapara o comando SQL
                string cmdSQL = "SELECT * " +
                                 "FROM tb_eventos " +
                                 "ORDER BY id_evento";

                //Prepara o SqlDataAdapter
                SqlDataAdapter o_DataAdapter = new SqlDataAdapter(cmdSQL, con);

                // Abre a conexão com o BD
                con.Open();

                DataTable dtPesquisa = new DataTable();

                // Executa o Select no banco de dados
                int qtdLinhasAfetadas = o_DataAdapter.Fill(dtPesquisa);

                // Fecha a conexão com o BD
                con.Close();

                if (qtdLinhasAfetadas > 0)
                {
                    return dtPesquisa;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SelecionarUltimas()
        {
            try
            {
                // Prapara o comando SQL
                string cmdSQL = "SELECT TOP 3 * " +
                                 "FROM tb_eventos " +
                                 "ORDER BY id_evento DESC";

                //Prepara o SqlDataAdapter
                SqlDataAdapter o_DataAdapter = new SqlDataAdapter(cmdSQL, con);

                // Abre a conexão com o BD
                con.Open();

                DataTable dtPesquisa = new DataTable();

                // Executa o Select no banco de dados
                int qtdLinhasAfetadas = o_DataAdapter.Fill(dtPesquisa);

                // Fecha a conexão com o BD
                con.Close();

                if (qtdLinhasAfetadas > 0)
                {
                    return dtPesquisa;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SelecionarPorID()
        {
            try
            {
                // Prapara o comando SQL
                string cmdSQL = "SELECT * FROM tb_eventos " +
                                "WHERE id_evento = @IdEvento";

                //Prepara o SqlDataAdapter
                SqlDataAdapter o_DataAdapter = new SqlDataAdapter(cmdSQL, con);

                o_DataAdapter.SelectCommand.Parameters.AddWithValue("@IdEvento", id_evento);

                // Abre a conexão com o BD
                con.Open();

                DataTable dtPesquisa = new DataTable();

                // Executa o Select no banco de dados
                int qtdLinhasAfetadas = o_DataAdapter.Fill(dtPesquisa);

                // Fecha a conexão com o BD
                con.Close();

                if (qtdLinhasAfetadas > 0)
                {
                    return dtPesquisa;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }







    }


}
