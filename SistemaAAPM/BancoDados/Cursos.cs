using System.Data;
using System.Data.SqlClient;

namespace SistemaAAPM.BancoDados
{
    public class Cursos
    {
        public int? id_curso {  get; set; }
        public string? nome_curso { get; set; }
        public string? sigla_curso { get; set; }
        public DateTime? data_inicio { get; set; }
        public DateTime? data_termino { get; set; }

        SqlConnection con;

        //--------------------------------
        // Contrutor
        //--------------------------------
        public Cursos()
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
                string cmdSQL = "Insert Into tb_curso" +
                    "(nome_curso, sigla_curso, data_inicio, data_termino) " +
                    "Values(@NomeCurso, @SiglaCurso, @DtInicio, @DtTermino)";

                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@NomeCurso", nome_curso);
                cmd.Parameters.AddWithValue("@SiglaCurso", sigla_curso);
                cmd.Parameters.AddWithValue("@DtInicio", data_inicio);
                cmd.Parameters.AddWithValue("@DtTermino", data_termino);

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
                string cmdSQL = "Update tb_curso Set nome_curso = @NomeCurso, sigla_curso = @SiglaCurso, data_inicio = @DtInicio, data_termino = @DtTermino " +
                                "Where id_curso = @IdCurso";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdCurso", id_curso);
                cmd.Parameters.AddWithValue("@NomeCurso", nome_curso);
                cmd.Parameters.AddWithValue("@SiglaCurso", sigla_curso);
                cmd.Parameters.AddWithValue("@DtInicio", data_inicio);
                cmd.Parameters.AddWithValue("@DtTermino", data_termino);

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
                string cmdSQL = "Delete From tb_curso Where id_curso = @IdCurso";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdCurso", id_curso);

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
                                 "FROM tb_curso " +
                                 "ORDER BY id_curso";

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
                string cmdSQL = "SELECT * FROM tb_curso " +
                                "WHERE id_curso = @IdCurso";

                //Prepara o SqlDataAdapter
                SqlDataAdapter o_DataAdapter = new SqlDataAdapter(cmdSQL, con);

                o_DataAdapter.SelectCommand.Parameters.AddWithValue("@IdCurso", id_curso);

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


  

