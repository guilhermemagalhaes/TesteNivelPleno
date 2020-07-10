using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using ConsoleProva.Models;
using System.Data.SqlClient;

namespace ConsoleProva
{
    public class Program
    {
        static void Main(string[] args)
        {
            ExibirMenu();
        }

        private static void ExibirMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Digite uma das opções:");
            Console.WriteLine("1- Importar Planilha");
            Console.WriteLine("2- Exibir Clientes");
            Console.WriteLine("3- Par ou Impar");
            Console.WriteLine("4- Anagrama");
            Console.WriteLine("");

            var acao = Console.ReadLine();


            switch (acao)
            {
                case "1":
                    ImportarPlanilha();
                    break;
                case "2":
                    ExibirClientes();
                    break;
                case "3":
                    ParOuImpar();
                    break;
                case "4":
                    Anagrama();
                    break;

            }
        }

        private static string connectionString = @"Data Source=localhost;Initial Catalog=Prova;Integrated Security=True;Pooling=False";

        #region Planilha
        private static List<Cliente> GetClienteByPlanilha(string filename)
        {
            FileInfo file = new FileInfo(filename);
            string conexao = string.Format(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties ='text;;HDR=Yes;FMT=Delimited''';", file.DirectoryName);
            string comando = string.Format(@"select * from[{0}]", file.Name);
            DataTable dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(conexao))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(comando, conn))
                {
                    using (OleDbDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                    }
                }
                conn.Close();
            }

            var clientes = new List<Cliente>();
            foreach (DataRow x in dt.Rows)
            {
                var linha = x.ItemArray[0].ToString();
                string[] valorLinha = linha.Split(';');
                if (valorLinha.Count() > 0)
                {
                    var cliente = new Cliente()
                    {
                        NR_CLIENTE = Convert.ToInt32(valorLinha[0]),
                        TX_CPF = valorLinha[1],
                        NM_CLIENTE = valorLinha[2],
                        DT_NASC = Convert.ToDateTime(valorLinha[3])
                    };

                    clientes.Add(cliente);
                }
            }

            return clientes;
        }

        private static void AddClienteADO(List<Cliente> clientes)
        {

            string query = "INSERT INTO dbo.TB_DADOS_CLIENTE (NR_CLIENTE, TX_CPF, NM_CLIENTE, DT_NASC) " +
                            "VALUES (@NR_CLIENTE, @TX_CPF, @NM_CLIENTE, @DT_NASC)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cn.Open();

                cmd.Parameters.Add("@NR_CLIENTE", SqlDbType.VarChar, 4);
                cmd.Parameters.Add("@TX_CPF", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@NM_CLIENTE", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@DT_NASC", SqlDbType.DateTime);


                foreach (var cliente in clientes)
                {
                    cmd.Parameters[0].Value = cliente.NR_CLIENTE;
                    cmd.Parameters[1].Value = cliente.TX_CPF;
                    cmd.Parameters[2].Value = cliente.NM_CLIENTE;
                    cmd.Parameters[3].Value = cliente.DT_NASC;

                    cmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }

        public static void ImportarPlanilha()
        {
            try
            {
                var clientes = GetClienteByPlanilha(@"C:\Temp\Arquivo_Importacao.csv");
                AddClienteADO(clientes);
                Console.WriteLine("Dados importados com sucesso");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro, tente novamente");
            }
            ExibirMenu();
        }
        #endregion

        #region Exibir Clientes

        private static List<Cliente> GetClientes()
        {
            var clientes = new List<Cliente>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SP_RETORNA_DADOS_CLIENTE", cn))
            {
                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cliente = new Cliente();
                        cliente.NR_CLIENTE = reader.GetInt32(reader.GetOrdinal("NR_CLIENTE"));
                        cliente.TX_CPF = ((reader["TX_CPF"] == DBNull.Value) ? "" : reader.GetString(reader.GetOrdinal("TX_CPF")));
                        cliente.NM_CLIENTE = ((reader["NM_CLIENTE"] == DBNull.Value) ? "" : reader.GetString(reader.GetOrdinal("NM_CLIENTE")));
                        cliente.DT_NASC = (reader["DT_NASC"] == DBNull.Value ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DT_NASC")));
                        clientes.Add(cliente);
                    }
                }
                cn.Close();
            }

            return clientes;
        }

        public static void ExibirClientes()
        {
            var clientes = GetClientes();

            string colunas1 = "NR_CLIENTE";
            string colunas2 = "TX_CPF";
            string colunas3 = "NM_CLIENTE";
            string colunas4 = "DT_NASC";


            Console.WriteLine(string.Format("{0}|{1}|{2}|{3}", colunas1, colunas2.PadRight(12, ' '), colunas3.PadRight(11, ' '), colunas4));

            foreach (var cliente in clientes)
            {
                Console.WriteLine(string.Format("{0}|{1}|{2}|{3}", cliente.NR_CLIENTE.ToString().PadRight(10, ' '), cliente.TX_CPF.PadRight(11, ' '), cliente.NM_CLIENTE.PadRight(11, ' '), cliente.DT_NASC.ToString("dd/MM/yyyy")));
            }

            ExibirMenu();
        }
        #endregion

        #region Par ou Impar
        public static void ParOuImpar()
        {
            Console.WriteLine("");
            Console.WriteLine("Digite o número:");
            int valor = Convert.ToInt32(Console.ReadLine());

            if ((valor & 1) == 0)
                Console.WriteLine("Par");
            else
                Console.WriteLine("Impar");

            ExibirMenu();
        }
        #endregion

        #region Anagrama
        private static bool VerificaAnagrama(string p1, string p2)
        {
            
            if (p1.Length != p2.Length)
                return false;


            foreach (char c in p2)
            {
                int ix = p1.IndexOf(c);
                if (ix >= 0)
                    p1 = p1.Remove(ix, 1);
                else
                    return false;
            }

            return string.IsNullOrEmpty(p1);
        }
        public static void Anagrama()
        {
            Console.WriteLine("Digite a primeira palavra:");
            var p1 = Console.ReadLine();

            Console.WriteLine("Digite a segunda palavra palavra:");
            var p2 = Console.ReadLine();

            if (string.IsNullOrEmpty(p1) || string.IsNullOrEmpty(p2))
            {
                Console.WriteLine("Valores incorretos");
                Console.Clear();
                Anagrama();
            }
            Console.WriteLine(VerificaAnagrama(p1, p2) ? "Verdadeiro" : "Falso");

            ExibirMenu();
        }
        #endregion
    }
}
