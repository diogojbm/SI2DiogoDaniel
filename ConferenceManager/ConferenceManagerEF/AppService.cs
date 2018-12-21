using ConferenceManagerEF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;


namespace ConferenceManager
{
    class AppService
    {
        private Conferencia c;
        private Submissao s;
        private Revisao r;
        private Artigo a;
        public AppService()
        {
            c = new Conferencia();
            s = new Submissao();
            r = new Revisao();
            a = new Artigo();
        }

        //ctx.Database.SQLQuery<Conferencia>();

        public void UpdateRevisionLimitDate()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;
            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    using (var ts = new TransactionScope())
                    {
                        try
                        {
                            using (var ctx = new ConferênciaAcadémicaEntities())
                            {
                                string aTuaMaeDe4 = parameters[1];
                                int aTuaMaeDe69 = Int32.Parse(parameters[2]);
                                Conferencia ic = ctx.Conferencias.Where(conf => conf.nome == aTuaMaeDe4 && conf.anoRealizacao == aTuaMaeDe69).SingleOrDefault();
                                Console.WriteLine("" + ic.nome + ic.anoRealizacao + "");
                                //ctx.AtualizarConferenciaDataLimiteRevisao(date, parameters[1], Int32.Parse(parameters[2]));
                                //c = cm.Read(new Tuple<string, int>(c.nome, c.anoRealizacao));
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    if (c != null) Console.WriteLine("DATA LIMITE DE REVISAO: " + c.dataLimiteRevisao);
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("ERRO NO FORMATO DA NOVA DATA. A NOVA DATA DEVE RESPEITAR 0 FORMATO YYYYMMDD. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void UpdateSubmissionLimitDate()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA SUBMISSÃƒO> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;

            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    c.dataLimiteSubmissao = date;
                    c.nome = parameters[1];
                    c.anoRealizacao = Int32.Parse(parameters[2]);

                    //c = cm.Read(new Tuple<string, int>(c.nome, c.anoRealizacao));

                    if (c != null) Console.WriteLine("DATA LIMITE DE SUBMISSÃO: " + c.dataLimiteSubmissao);
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("ERRO NO FORMATO DA NOVA DATA. A NOVA DATA DEVE RESPEITAR 0 FORMATO YYYYMMDD. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void UpdateConferencePresident()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL PRESIDENTE> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();

            if (parameters.Length == 3)
            {
                c.emailPresidente = parameters[0];
                c.nome = parameters[1];
                c.anoRealizacao = Int32.Parse(parameters[2]);

                //c = cm.Read(new Tuple<string, int>(c.nome, c.anoRealizacao));

                if (c != null) Console.WriteLine("PRESIDENTE: " + c.emailPresidente);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void AssignAuthorRole()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <RESPONSÁVEL> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Submissao s = new Submissao();

            if (parameters.Length == 5)
            {
                s.idArtigo = Int32.Parse(parameters[0]);
                s.emailAutor = parameters[1];
                if (parameters[2] == "1")
                {
                    s.responsavel = true;
                }
                else
                {
                    s.responsavel = false;
                }
                s.nomeConferencia = parameters[3];
                s.anoConferencia = Int32.Parse(parameters[4]);

                //s = sm.Read(new Tuple<int, string, string, int>(s.idArtigo, s.emailAutor, s.nomeConferencia, s.anoConferencia));

                if (s != null) Console.WriteLine("NOVO AUTOR: " + s.emailAutor);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }
        public void AssignReviserRole()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 4)
            {
                r.idArtigo = Int32.Parse(parameters[0]);
                r.emailRevisor = parameters[1];
                r.nomeConferencia = parameters[2];
                r.anoConferencia = Int32.Parse(parameters[3]);

                //r = rm.Read(new Tuple<int, string, string, int>(r.idArtigo, r.emailRevisor, r.nomeConferencia, r.anoConferencia));

                if (r != null) Console.WriteLine("NOVO REVISOR: " + r.emailRevisor);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void AssignPresident()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL UTILIZADOR> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();

            if (parameters.Length == 3)
            {
                c.emailPresidente = parameters[0];
                c.nome = parameters[1];
                c.anoRealizacao = Int32.Parse(parameters[2]);

                //c = cm.Read(new Tuple<string, int>(c.nome, c.anoRealizacao));

                if (c != null) Console.WriteLine("PRESIDENTE: " + c.emailPresidente);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }
        }

        //TO-DO:
        public void ListRevisers()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
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
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL REVISOR> <ID ARTIGO> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 4)
            {
                r.emailRevisor = parameters[0];
                r.idArtigo = Int32.Parse(parameters[1]);
                r.nomeConferencia = parameters[2];
                r.anoConferencia = Int32.Parse(parameters[3]);

                //r = rm.Read(new Tuple<int, string, string, int>(r.idArtigo, r.emailRevisor, r.nomeConferencia, r.anoConferencia));

                if (r != null) Console.WriteLine("REVISOR: " + r.emailRevisor);
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }
            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        //A DATA TEM DE SER ALGO COMO 20181112 PORQUE A DATA LIMITE DE REVISAO DA CONFERENCIA B 2018 FOI ALTERADA PARA 20181120
        public void RegisterRevision()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOTA MÍNIMA> <NOTA> <TEXTO> <ID ARTIGO> <EMAIL REVISOR> <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();
            Artigo a = new Artigo();

            if (parameters.Length == 8)
            {
                DateTime date;
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    r.notaMinima = Int32.Parse(parameters[1]);
                    r.nota = Int32.Parse(parameters[2]);
                    r.texto = parameters[3];
                    r.idArtigo = Int32.Parse(parameters[4]);
                    r.emailRevisor = parameters[5];
                    r.nomeConferencia = parameters[6];
                    r.anoConferencia = Int32.Parse(parameters[7]);

                    //r = rm.Read(new Tuple<int, string, string, int>(r.idArtigo, r.emailRevisor, r.nomeConferencia, r.anoConferencia));
                    if (r != null)
                    {
                        //a = am.Read(new Tuple<int, string, int>(r.idArtigo, r.nomeConferencia, r.anoConferencia));
                        if (a != null)
                        {
                            Console.WriteLine("REVISAO: " + r.notaMinima + " " + r.nota + " " + r.texto + " " + r.idArtigo + " " + r.emailRevisor + " " + r.nomeConferencia + " " + r.anoConferencia + " " + r.emailRevisor + "\n");
                            Console.WriteLine("NOVO ESTADO DE ARTIGO: " + a.estado);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("ERRO NO FORMATO DA DATA DE REVISÃO INSERIDA. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }
            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void CalcAcceptedSubmissionsRatio()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <nome CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            string ratio = null;
            if (parameters.Length == 2)
            {
                c.nome = parameters[0];
                c.anoRealizacao = Int32.Parse(parameters[1]);

                if (ratio != null)
                {
                    Console.WriteLine("PERCENTAGEM DE SUBMISSÕES ACEITES: " + ratio + "%");
                    Console.WriteLine();
                    Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
                }
                else Console.WriteLine("NÃO FOI POSSÍVEL CALCULAR A PERCENTAGEM. PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        //TO-DO
        public void ChangeSubmissionState() { }
    }
}