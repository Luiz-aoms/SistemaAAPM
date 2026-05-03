using System.Data;
using System.Data.SqlClient;

namespace SistemaAAPM.BancoDados
{
    public class Salas
    {
        public int? id_sala {  get; set; }

        public int? nmr_sala { get; set; }

        public string? bloco_sala { get; set; }


        SqlConnection con;

        //--------------------------------
        // Contrutor
        //--------------------------------
        public Salas()
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
                string cmdSQL = "Insert Into tb_salas" +
                    "(nmr_sala, bloco_sala) " +
                    "Values(@NmrSala, @BlocoSala)";

                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@NmrSala", nmr_sala);
                cmd.Parameters.AddWithValue("@BlocoSala", bloco_sala);

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
                string cmdSQL = "Update tb_salas Set nmr_sala = @NmrSala, bloco_sala = @BlocoSala " +
                                 "Where id_sala = @IdSala";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdSala", id_sala);
                cmd.Parameters.AddWithValue("@NmrSala", nmr_sala);
                cmd.Parameters.AddWithValue("@BlocoSala", bloco_sala);

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
                string cmdSQL = "Delete From tb_salas Where id_sala = @IdSala";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdSala", id_sala);

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
                                 "FROM tb_salas " +
                                 "ORDER BY id_sala";

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
                string cmdSQL = "SELECT * FROM tb_salas " +
                                "WHERE id_sala = @IdSala";

                //Prepara o SqlDataAdapter
                SqlDataAdapter o_DataAdapter = new SqlDataAdapter(cmdSQL, con);

                o_DataAdapter.SelectCommand.Parameters.AddWithValue("@IdSala", id_sala);

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
