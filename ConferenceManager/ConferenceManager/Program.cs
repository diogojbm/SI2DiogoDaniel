using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager
{
    class App
    {
        private AppService service;
        private enum Function
        {
            Unknown = -1,
            Exit,
            UpdateConference,
            AssignJobToUser,
            ListCompatibleReviewers,
            AssignReviewerToRevision,
            RegisterRevision,
            AcceptedSubmissionsPercentage,
            ChangeSubmissionState
        }
        private App()
        {
            service = new AppService();
            mainOptions = new Dictionary<Function, MainMenuOptions>();
            mainOptions.Add(Function.UpdateConference, UpdateConference);
            mainOptions.Add(Function.AssignJobToUser, AssignJobToUser);
            mainOptions.Add(Function.ListCompatibleReviewers, ListCompatibleReviewers);
            mainOptions.Add(Function.AssignReviewerToRevision, AssignReviewerToRevision);
            mainOptions.Add(Function.RegisterRevision, RegisterRevision);
            mainOptions.Add(Function.AcceptedSubmissionsPercentage, AcceptedSubmissionsPercentage);
            mainOptions.Add(Function.ChangeSubmissionState, ChangeSubmissionState);
        }

        private static App __instance;
        public static App Instance
        {
            get
            {
                if (__instance == null)
                    __instance = new App();
                return __instance;
            }
            private set { }
        }
        
        private delegate void MainMenuOptions();
        private System.Collections.Generic.Dictionary<Function, MainMenuOptions> mainOptions;

        private void UpdateConference()
        {
            Console.WriteLine("ATUALIZAR CONFERÊNCIA");
            Console.WriteLine();
            Console.WriteLine("MENU DE OPÇÕES:");
            Console.WriteLine("1. ATUALIZAR DATA LIMITE DE REVISÃO");
            Console.WriteLine("2. ATUALIZAR DATA LIMITE DE SUBMISSÃƒO");
            Console.WriteLine("3. ATUALIZAR PRESIDENTE");
            Console.WriteLine();
            Console.WriteLine("SELECIONE A FUNCIONALIDADE QUE DESEJA E PRIMA A TECLA ENTER:");
            var op = Console.ReadLine().ToString();

            if (op == "1")
            {
                service.UpdateRevisionLimitDate();
            }

            else if (op == "2")
            {
                service.UpdateSubmissionLimitDate();
            }
            else if (op == "3")
            {
                service.UpdateConferencePresident();
            }
        }
        private void AssignJobToUser()
        {
            Console.WriteLine("ATRIBUIR PAPEL A UM UTILIZADOR REGISTADO");
            Console.WriteLine();
            Console.WriteLine("MENU DE OPÇÕES:");
            Console.WriteLine("1. ATRIBUIR PAPEL DE AUTOR");
            Console.WriteLine("2. ATRIBUIR PAPEL DE REVISOR");
            Console.WriteLine("3. ATRIBUIR PAPEL DE PRESIDENTE");
            Console.WriteLine();
            Console.WriteLine("SELECIONE A FUNCIONALIDADE QUE DESEJA E PRIMA A TECLA ENTER:");
            var op = Console.ReadLine().ToString();

            if (op == "1")
            {
                service.AssignAuthorRole();
                Console.WriteLine();
                Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <RESPONSÃVEL> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
                string[] parameters = Console.ReadLine().Split(' ');

                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = @"Data Source=LAPTOP-VLEG347R;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.Transaction = transaction;
                        cmd.CommandText = $"EXEC ConferênciaAcadémica.AtribuirPapelAutor '{parameters[0]}', '{parameters[1]}', '{parameters[2]}', '{parameters[3]}', '{parameters[4]}'";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTE UTILIZADOR Ã‰ AGORA UM DOS AUTORES DO ARTIGO.");
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }

            }
            else if (op == "2")
            {
                Console.WriteLine();
                Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
                string[] parameters = Console.ReadLine().Split(' ');

                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = @"Data Source=VLABSIAD;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.Transaction = transaction;
                        cmd.CommandText = $"EXEC ConferênciaAcadémica.AtribuirPapelRevisor '{parameters[0]}', '{parameters[1]}', '{parameters[2]}', '{parameters[3]}'";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTE UTILIZADOR Ã‰ AGORA REVISOR DO ARTIGO.");
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }

            }
            else if (op == "3")
            {
                Console.WriteLine();
                Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL UTILIZADOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
                string[] parameters = Console.ReadLine().Split(' ');

                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = @"Data Source=VLABSIAD;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.Transaction = transaction;
                        cmd.CommandText = $"EXEC ConferênciaAcadémica.AtribuirPapelPresidente '{parameters[0]}', '{parameters[1]}', '{parameters[2]}'";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTE UTILIZADOR Ã‰ AGORA PRESIDENTE DA CONFERÊNCIA.");
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
        }
        private void ListCompatibleReviewers()
        {
            Console.WriteLine("LISTAR REVISORES COMPATÍVEIS COM REVISÃO");
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = @"Data Source=VLABSIAD;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();    // default Isolation Level

                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = $"EXEC ConferênciaAcadémica.ListarRevisores '{parameters[0]}', '{parameters[1]}', '{parameters[2]}'";
                    SqlDataReader dr = cmd.ExecuteReader();
                    Console.WriteLine("LISTA DE REVISORES COMPATÍVEIS:");
                    while (dr.Read()) { Console.WriteLine(dr[0].ToString()); }
                    dr.Close();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
        private void AssignReviewerToRevision()
        {
            Console.WriteLine("ATRIBUIR REVISOR A UMA REVISÃO");
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL REVISOR> <ID ARTIGO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = @"Data Source=VLABSIAD;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);

                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = $"EXEC ConferênciaAcadémica.AtribuirRevisor '{parameters[0]}', '{parameters[1]}', '{parameters[2]}', '{parameters[3]}'";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTE REVISOR Ã‰ AGORA UM DOS REVISORES DO ARTIGO.");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
        private void RegisterRevision()
        {
            Console.WriteLine("REGISTAR REVISÃO");
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOTA MÍNIMA> <NOTA> <TEXTO> <ID ARTIGO> <EMAIL REVISOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = @"Data Source=VLABSIAD;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = $"EXEC ConferênciaAcadémica.RegistarRevisao '{parameters[0]}', '{parameters[1]}', '{parameters[2]}', '{parameters[3]}', '{parameters[4]}', '{parameters[5]}', '{parameters[6]}', '{parameters[7]}'";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! REVISÃO REGISTADA.");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
        private void AcceptedSubmissionsPercentage()
        {
            Console.WriteLine("PERCENTAGEM DE SUBMISSÕES ACEITES");
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = @"Data Source=VLABSIAD;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                con.Open();

                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = $"SELECT ConferênciaAcadémica.PercentagemSubmissoesAceites('{parameters[0]}', {parameters[1]})";
                    Console.WriteLine("PERCENTAGEM DE SUBMISSÕES ACEITES: " + cmd.ExecuteScalar().ToString() + "%");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void ChangeSubmissionState()
        {
            Console.WriteLine("ALTERAR O ESTADO DE TODAS AS SUBMISSÕES");
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

        }

        private Function DisplayMenu()
        {
            Function function = Function.Unknown;
            try
            {
                Console.WriteLine("CONFERÊNCIA ACADÉMICA");
                Console.WriteLine();
                Console.WriteLine("1. ATUALIZAR CONFERÊNCIA");
                Console.WriteLine("2. ATRIBUIR PAPEL A UM UTILIZADOR REGISTADO");
                Console.WriteLine("3. LISTAR REVISORES COMPATÍVEIS COM REVISÃO");
                Console.WriteLine("4. ATRIBUIR REVISOR A UMA REVISÃO");
                Console.WriteLine("5. REGISTAR REVISÃO");
                Console.WriteLine("6. PERCENTAGEM DE SUBMISSÕES ACEITES");
                Console.WriteLine("7. ALTERAR O ESTADO DE TODAS AS SUBMISSÕES");
                Console.WriteLine("0. SAIR");
                Console.WriteLine();
                Console.WriteLine("SELECIONE A FUNCIONALIDADE QUE DESEJA E PRIMA A TECLA ENTER:");
                var result = Console.ReadLine();
                function = (Function)Enum.Parse(typeof(Function), result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ERRO NA SELEÇÃO DE OPERAÇÃO: " + ex.Message);
            }

            return function;
        }
        public void Run()
        {
            Function userInput = Function.Unknown;
            do
            {
                Console.Clear();
                userInput = DisplayMenu();
                Console.Clear();
                try
                {
                    mainOptions[userInput]();
                    Console.ReadKey();
                }
                catch (KeyNotFoundException ex)
                {
                }

            } while (userInput != Function.Exit);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            App.Instance.Run();
        }
    }
}