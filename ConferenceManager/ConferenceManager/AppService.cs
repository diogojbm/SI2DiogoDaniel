using ConferenceManager.Concrete;
using ConferenceManager.Model;
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
        }

        public void UpdateRevisionLimitDate()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;

            if (parameters.Length == 3) {
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    c.DataLimiteRevisao = parameters[0];
                    c.Nome = parameters[1];
                    c.AnoRealizacao = Int32.Parse(parameters[2]);

                    cm.ExecUpdateRevisionLimitDate(ctx, c);
                    c = cm.Read(new Tuple<string, int>(c.Nome, c.AnoRealizacao));

                    if (c != null) Console.WriteLine("NOVA DATA LIMITE DE REVISAO: " + c.DataLimiteRevisao);
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("ERRO NO FORMATO DA NOVA DATA. A NOVA DATA DEVE RESPEITAR 0 FORMATO YYYYMMDD. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }

        public void UpdateSubmissionLimitDate()
        {
            /*Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA SUBMISSÃƒO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            cm.UpdateRevisionLimitDate(ctx, parameters);

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
            }*/
        }

        public void UpdateConferencePresident()
        {
            /*Console.WriteLine();
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
            }*/
        }

        public void AssignAuthorRole()
        {
            /*Console.WriteLine();
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
            }*/
        }
    }
}