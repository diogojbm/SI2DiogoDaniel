using ConferenceManager.Concrete;
using ConferenceManager.Model;
using System;
using System.Configuration;


namespace ConferenceManager
{
    class AppService
    {
        private ConferenciaMapper cm;
        private SubmissaoMapper sm;
        private RevisaoMapper rm;
        private ArtigoMapper am;
        private Context ctx = new Context(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
        public AppService()
        {
            cm = new ConferenciaMapper(ctx);
            sm = new SubmissaoMapper(ctx);
            rm = new RevisaoMapper(ctx);
            am = new ArtigoMapper(ctx);
        }

        public void UpdateRevisionLimitDate()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;

            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    c.DataLimiteRevisao = parameters[0];
                    c.Nome = parameters[1];
                    c.AnoRealizacao = Int32.Parse(parameters[2]);

                    cm.ExecUpdateRevisionLimitDate(ctx, c);

                    c = cm.Read(new Tuple<string, int>(c.Nome, c.AnoRealizacao));

                    if (c != null) Console.WriteLine("DATA LIMITE DE REVISAO: " + c.DataLimiteRevisao);
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
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA SUBMISSÃƒO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;

            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    c.DataLimiteSubmissao = parameters[0];
                    c.Nome = parameters[1];
                    c.AnoRealizacao = Int32.Parse(parameters[2]);

                    cm.ExecUpdateSubmissionLimitDate(ctx, c);

                    c = cm.Read(new Tuple<string, int>(c.Nome, c.AnoRealizacao));

                    if (c != null) Console.WriteLine("DATA LIMITE DE SUBMISSÃO: " + c.DataLimiteSubmissao);
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("ERRO NO FORMATO DA NOVA DATA. A NOVA DATA DEVE RESPEITAR 0 FORMATO YYYYMMDD. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }

        public void UpdateConferencePresident()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL PRESIDENTE> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();

            if (parameters.Length == 3)
            {
                c.EmailPresidente = parameters[0];
                c.Nome = parameters[1];
                c.AnoRealizacao = Int32.Parse(parameters[2]);

                cm.ExecUpdatePresident(ctx, c);

                c = cm.Read(new Tuple<string, int>(c.Nome, c.AnoRealizacao));

                if (c != null) Console.WriteLine("PRESIDENTE: " + c.EmailPresidente);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }

        public void AssignAuthorRole()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <RESPONSÁVEL> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Submissao s = new Submissao();

            if (parameters.Length == 5)
            {
                s.IDArtigo = Int32.Parse(parameters[0]);
                s.EmailAutor = parameters[1];
                if (parameters[2] == "1")
                {
                    s.Responsavel = true;
                }
                else
                {
                    s.Responsavel = false;
                }
                s.NomeConferencia = parameters[3];
                s.AnoConferencia = Int32.Parse(parameters[4]);

                sm.ExecNewAuthor(ctx, s);

                s = sm.Read(new Tuple<int, string, string, int>(s.IDArtigo, s.EmailAutor, s.NomeConferencia, s.AnoConferencia));

                if (s != null) Console.WriteLine("NOVO AUTOR: " + s.EmailAutor);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }

        public void AssignReviserRole()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 4)
            {
                r.IDArtigo = Int32.Parse(parameters[0]);
                r.EmailRevisor = parameters[1];
                r.NomeConferencia = parameters[2];
                r.AnoConferencia = Int32.Parse(parameters[3]);

                rm.ExecNewReviser(ctx, r);

                r = rm.Read(new Tuple<int, string, string, int>(r.IDArtigo, r.EmailRevisor, r.NomeConferencia, r.AnoConferencia));

                if (r != null) Console.WriteLine("NOVO REVISOR: " + r.EmailRevisor);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }

        public void AssignPresident()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL UTILIZADOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();

            if (parameters.Length == 3)
            {
                c.EmailPresidente = parameters[0];
                c.Nome = parameters[1];
                c.AnoRealizacao = Int32.Parse(parameters[2]);

                cm.ExecAssignPresident(ctx, c);

                c = cm.Read(new Tuple<string, int>(c.Nome, c.AnoRealizacao));

                if (c != null) Console.WriteLine("PRESIDENTE: " + c.EmailPresidente);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }
        }

        //TO-DO:
        public void ListRevisers()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

        /*
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
        */
    }

        public void AssignReviser()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL REVISOR> <ID ARTIGO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 4)
            {
                r.EmailRevisor = parameters[0];
                r.IDArtigo = Int32.Parse(parameters[1]);
                r.NomeConferencia = parameters[2];
                r.AnoConferencia = Int32.Parse(parameters[3]);

                rm.ExecAssignReviser(ctx, r);
                //ctx.Revisoes.Find()
                r = rm.Read(new Tuple<int, string, string, int>(r.IDArtigo, r.EmailRevisor, r.NomeConferencia, r.AnoConferencia));

                if (r != null) Console.WriteLine("REVISOR: " + r.EmailRevisor);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }
        
        //A DATA TEM DE SER ALGO COMO 20181112 PORQUE A DATA LIMITE DE REVISAO DA CONFERENCIA B 2018 FOI ALTERADA PARA 20181120
        public void RegisterRevision()
        {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOTA MÍNIMA> <NOTA> <TEXTO> <ID ARTIGO> <EMAIL REVISOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();
            Artigo a = new Artigo();

            if (parameters.Length == 8)
            {
                DateTime date;
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    r.NotaMinima = Int32.Parse(parameters[1]);
                    r.Nota = Int32.Parse(parameters[2]);
                    r.Texto = parameters[3];
                    r.IDArtigo = Int32.Parse(parameters[4]);
                    r.EmailRevisor = parameters[5];
                    r.NomeConferencia = parameters[6];
                    r.AnoConferencia = Int32.Parse(parameters[7]);

                    rm.ExecRegisterRevision(ctx, parameters[0], r);

                    r = rm.Read(new Tuple<int, string, string, int>(r.IDArtigo, r.EmailRevisor, r.NomeConferencia, r.AnoConferencia));
                    if (r != null)
                    {
                        a = am.Read(new Tuple<int, string, int>(r.IDArtigo, r.NomeConferencia, r.AnoConferencia));
                        if (a != null)
                        {
                            Console.WriteLine("REVISAO: " + r.NotaMinima + " " + r.Nota + " " + r.Texto + " " + r.IDArtigo + " " + r.EmailRevisor + " " + r.NomeConferencia + " " + r.AnoConferencia + " " + r.EmailRevisor + "\n");
                            Console.WriteLine("NOVO ESTADO DE ARTIGO: " + a.Estado);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("ERRO NO FORMATO DA DATA DE REVISÃO INSERIDA. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }
            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }

        public void CalcAcceptedSubmissionsRatio() {
            ctx.Open();

            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            string ratio = null;
            if (parameters.Length == 2)
            {
                c.Nome = parameters[0];
                c.AnoRealizacao = Int32.Parse(parameters[1]);

                ratio = cm.ExecCalcAcceptedSubmissionsRatio(ctx, c);

                if (ratio != null)
                {
                    Console.WriteLine("PERCENTAGEM DE SUBMISSÕES ACEITES: " + ratio + "%");
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("NÃO FOI POSSÍVEL CALCULAR A PERCENTAGEM. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");

            ctx.Dispose();
        }

        //TO-DO
        public void ChangeSubmissionState() { }
    }
}