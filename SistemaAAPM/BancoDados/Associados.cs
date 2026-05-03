using Humanizer;
using System.Data;
using System.Data.SqlClient;


namespace SistemaAAPM.BancoDados
{
    public class Associados
    {

        //=================================================
        //          Atributos
        //=================================================
        public int? id_associado;
        public string? nome;
        public string? cpf;
        public int? id_curso;
        public string? senha_armario;
        public string? fone;
        public int? id_sala;


        SqlConnection con;


        //--------------------------------
        // Contrutor
        //--------------------------------
        public Associados()
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
                string cmdSQL = "Insert Into tb_associados" +
                    "(nome, cpf, id_curso, senha_armario, fone, id_sala)" +
                    "Values(@Nome, @Cpf, @IdCurso, @SenhaArmario, @Fone, @IdSala)";

                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Cpf", cpf);
                cmd.Parameters.AddWithValue("@IdCurso", id_curso);
                cmd.Parameters.AddWithValue("@SenhaArmario", senha_armario ?? Convert.DBNull);
                cmd.Parameters.AddWithValue("@Fone", fone);
                cmd.Parameters.AddWithValue("@IdSala", id_sala);


                // Abre a conexão com o BD
                con.Open();

                // Executa o comando SQL
                cmd.ExecuteNonQuery();

                // Fecha a conexão com o BD
                con.Close();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }

        public void Alterar()
        {
            try
            {
                // Prapara o comando SQL
                string cmdSQL = "Update tb_associados Set nome = @Nome, cpf = @Cpf, id_curso = @IdCurso, senha_armario = @SenhaArmario, fone = @Fone, id_sala = @IdSala " +
                                "WHERE id_associado = @IdAssociado";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdAssociado", id_associado);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Cpf", cpf);
                cmd.Parameters.AddWithValue("@IdCurso", id_curso);
                cmd.Parameters.AddWithValue("@SenhaArmario", senha_armario ?? Convert.DBNull);
                cmd.Parameters.AddWithValue("@Fone", fone);
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

        public void Excluir()
        {
            try
            {
                // Prapara o comando SQL
                string cmdSQL = "Delete From tb_associados Where id_associado = @IdAssociado";

                // Prepara SqlCommand
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                cmd.Parameters.AddWithValue("@IdAssociado", id_associado);

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
                string cmdSQL = "SELECT id_associado, nome, fone, cpf, senha_armario, C.sigla_curso As curso, S.nmr_sala As salas " +
                                "FROM tb_associados A LEFT JOIN tb_curso C ON A.id_curso = C.id_curso LEFT JOIN tb_salas S ON A.id_sala = S.id_sala " +
                                "ORDER By id_associado";

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
                string cmdSQL = "SELECT * FROM tb_associados " +
                                "WHERE id_associado = @IdAssociado";

                //Prepara o SqlDataAdapter
                SqlDataAdapter o_DataAdapter = new SqlDataAdapter(cmdSQL, con);

                o_DataAdapter.SelectCommand.Parameters.AddWithValue("@IdAssociado", id_associado);

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
