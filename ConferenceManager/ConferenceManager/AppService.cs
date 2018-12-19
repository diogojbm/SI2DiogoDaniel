using ConferenceManager.Concrete;
using System;
using System.Data.SqlClient;

namespace ConferenceManager
{
    class AppService
    {
        private ConferenciaMapper cm;
        private ArtigoMapper am;
        private Context ctx = new Context(@"Data Source=LAPTOP-VLEG347R;Initial Catalog=ConferênciaAcadémica;Integrated Security=True");
        public AppService()
        {
            cm = new ConferenciaMapper(ctx);
            am = new ArtigoMapper(ctx);
            
        }

        public void UpdateRevisionLimitDate()
        {
            ctx.Open();
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
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
                    cmd.CommandText = $"EXEC ConferênciaAcadémica.AtualizarConferenciaDataLimiteRevisao '{parameters[0]}', '{parameters[1]}', '{parameters[2]}'";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTA CONFERÊNCIA FOI ATUALIZADA.");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
            ctx.Dispose();
        }


        public void UpdateSubmissionLimitDate()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA SUBMISSÃƒO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
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
                    cmd.CommandText = $"EXEC ConferênciaAcadémica.AtualizarConferenciaDataLimiteSubmissao '{parameters[0]}', '{parameters[1]}', '{parameters[2]}'";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTA CONFERÊNCIA FOI ATUALIZADA.");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        public void UpdateConferencePresident(){
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL PRESIDENTE> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
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
                    cmd.CommandText = $"EXEC ConferênciaAcadémica.AtualizarConferenciaPresidente '{parameters[0]}', '{parameters[1]}', '{parameters[2]}'";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTA CONFERÊNCIA FOI ATUALIZADA.");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        public void AssignAuthorRole()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL PRESIDENTE> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
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
                    cmd.CommandText = $"EXEC ConferênciaAcadémica.AtualizarConferenciaPresidente '{parameters[0]}', '{parameters[1]}', '{parameters[2]}'";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTA CONFERÊNCIA FOI ATUALIZADA.");
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
}