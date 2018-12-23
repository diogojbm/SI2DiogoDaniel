using ConferenceManager.Concrete;
using ConferenceManager.Model;
using System;
using System.Configuration;


namespace ConferenceManager
{
    class AppService
    {
        private const string dateFormat = "yyyyMMdd";
        private const string invalidNumberOfArgumentsMessage = "INVALID NUMBER OF ARGUMENTS. PRESS ANY KEY TO TRY AGAIN.";
        private const string pressAnyKeyForANewCommandMessage = "PRESS ANY KEY FOR A NEW COMMAND.";
        private const string invalidNewDateFormatMessage = "ERROR ON NEW DATE'S FORMAT. THE NEW DATE MUST RESPECT YYYYMMDD FORMAT. PRESS ANY KEY TO START AGAIN.";
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

            Console.WriteLine("\nFILL THE FIELDS: <REVISION DATE> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;

            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    c.DataLimiteRevisao = parameters[0];
                    c.Nome = parameters[1];
                    c.AnoRealizacao = Int32.Parse(parameters[2]);

                    cm.ExecUpdateRevisionLimitDate(ctx, c);

                    foreach (var conf in ctx.Conferencias.Find(rev => ((rev.DataLimiteRevisao == parameters[0]) && (rev.Nome == parameters[1]) && (rev.AnoRealizacao == Int32.Parse(parameters[2])))))
                    {
                        Console.WriteLine($"NEW REVISION LIMIT DATE: {conf.DataLimiteRevisao}");
                    }
                    Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
                }
                else Console.WriteLine($"{invalidNewDateFormatMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        public void UpdateSubmissionLimitDate()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <SUBMISSION DATE> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;

            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    c.DataLimiteSubmissao = parameters[0];
                    c.Nome = parameters[1];
                    c.AnoRealizacao = Int32.Parse(parameters[2]);

                    cm.ExecUpdateSubmissionLimitDate(ctx, c);

                    foreach (var conf in ctx.Conferencias.Find(rev => ((rev.DataLimiteSubmissao == parameters[0]) && (rev.Nome == parameters[1]) && (rev.AnoRealizacao == Int32.Parse(parameters[2])))))
                    {
                        Console.WriteLine($"NEW SUBMISSION LIMIT DATE: {conf.DataLimiteSubmissao}");
                    }
                    Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
                }
                else Console.WriteLine($"{invalidNewDateFormatMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        public void UpdateConferencePresident()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <PRESIDENT EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();

            if (parameters.Length == 3)
            {
                c.EmailPresidente = parameters[0];
                c.Nome = parameters[1];
                c.AnoRealizacao = Int32.Parse(parameters[2]);

                cm.ExecUpdatePresident(ctx, c);

                foreach (var conf in ctx.Conferencias.Find(rev => ((rev.EmailPresidente == parameters[0]) && (rev.Nome == parameters[1]) && (rev.AnoRealizacao == Int32.Parse(parameters[2])))))
                {
                    Console.WriteLine($"NEW PRESIDENT: {conf.EmailPresidente}");
                }

                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        public void AssignAuthorRole()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <ARTICLE ID> <USER EMAIL> <RESPONSIBLE(0/1)> <CONFERENCE NAME> <CONFERENCE YEAR>");
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

                foreach (var autor in ctx.Submissoes.Find(rev => ((rev.IDArtigo == Int32.Parse(parameters[0])) && (rev.EmailAutor == parameters[1]) && (rev.NomeConferencia == parameters[3]) && (rev.AnoConferencia == Int32.Parse(parameters[4])))))
                {
                    Console.WriteLine($"{autor.EmailAutor} IS NOW AN AUTHOR");
                }

                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        public void AssignReviserRole()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <ARTICLE ID> <USER EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 4)
            {
                r.IDArtigo = Int32.Parse(parameters[0]);
                r.EmailRevisor = parameters[1];
                r.NomeConferencia = parameters[2];
                r.AnoConferencia = Int32.Parse(parameters[3]);

                rm.ExecNewReviser(ctx, r);

                foreach (var revisor in ctx.Revisoes.Find(rev => ((rev.IDArtigo == Int32.Parse(parameters[0])) && (rev.EmailRevisor == parameters[1]) && (rev.NomeConferencia == parameters[2]) && (rev.AnoConferencia == Int32.Parse(parameters[3])))))
                {
                    Console.WriteLine($"{revisor.EmailRevisor} IS NOW A REVISER");
                }
                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        public void AssignPresidentRole()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <USER EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();

            if (parameters.Length == 3)
            {
                c.EmailPresidente = parameters[0];
                c.Nome = parameters[1];
                c.AnoRealizacao = Int32.Parse(parameters[2]);

                cm.ExecAssignPresident(ctx, c);

                foreach (var presidente in ctx.Conferencias.Find(rev => ((rev.EmailPresidente == parameters[0]) && (rev.Nome == parameters[1]) && (rev.AnoRealizacao == Int32.Parse(parameters[2])))))
                {
                    Console.WriteLine($"{presidente.EmailPresidente} IS NOW A PRESIDENT");
                }
                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

        }

        public void ListRevisers()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <ARTICLE ID> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');
            
            Artigo a = new Artigo();
            if (parameters.Length == 3)
            {
                a.Identificador = Int32.Parse(parameters[0]);
                a.NomeConferencia = parameters[1];
                a.AnoConferencia = Int32.Parse(parameters[2]);

                am.ExecListCompatibleRevisers(ctx, a);

                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        public void AssignReviser()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <REVISER EMAIL> <ARTICLE ID> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 4)
            {
                r.EmailRevisor = parameters[0];
                r.IDArtigo = Int32.Parse(parameters[1]);
                r.NomeConferencia = parameters[2];
                r.AnoConferencia = Int32.Parse(parameters[3]);

                rm.ExecAssignReviser(ctx, r);

                foreach (var revisor in ctx.Revisoes.Find(rev => ((rev.EmailRevisor == parameters[0]) && (rev.IDArtigo == Int32.Parse(parameters[1])) && (rev.NomeConferencia == parameters[2]) && (rev.AnoConferencia == Int32.Parse(parameters[3])))))
                {
                    Console.WriteLine($"NEW REVISER: {revisor.EmailRevisor}");
                }
                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }

            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }
        
        //A DATA TEM DE SER ALGO COMO 20181112 PORQUE A DATA LIMITE DE REVISAO DA CONFERENCIA B 2018 FOI ALTERADA PARA 20181120
        public void RegisterRevision()
        {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <REVISION DATE> <MINIMUM GRADE> <GRADE> <TEXT> <ARTICLE ID> <REVISER EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 8)
            {
                DateTime date;
                if (DateTime.TryParseExact(parameters[0], dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    r.NotaMinima = Int32.Parse(parameters[1]);
                    r.Nota = Int32.Parse(parameters[2]);
                    r.Texto = parameters[3];
                    r.IDArtigo = Int32.Parse(parameters[4]);
                    r.EmailRevisor = parameters[5];
                    r.NomeConferencia = parameters[6];
                    r.AnoConferencia = Int32.Parse(parameters[7]);

                    rm.ExecRegisterRevision(ctx, parameters[0], r);

                    foreach (var revisao in ctx.Revisoes.Find(rev => ((rev.IDArtigo == Int32.Parse(parameters[4])) && (rev.EmailRevisor == parameters[5]) && (rev.NomeConferencia == parameters[6]) && (rev.AnoConferencia == Int32.Parse(parameters[7])))))
                    {
                        if(revisao != null) {
                            foreach (var artigo in ctx.Artigos.Find(rev => ((rev.Identificador == Int32.Parse(parameters[4])) && (rev.NomeConferencia == parameters[6]) && (rev.AnoConferencia == Int32.Parse(parameters[7])))))
                            {
                                if(artigo != null)
                                {
                                    Console.WriteLine($"NEW REVISION: \nidArtigo: {revisao.IDArtigo}\nemailRevisor: {revisao.EmailRevisor}\nnomeConferencia: {revisao.NomeConferencia}\nanoConferencia: {revisao.AnoConferencia}");
                                    Console.WriteLine($"NEW ARTICLE STATE: {artigo.Estado}");
                                }
                            }
                        }
                    }
                    Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
                }
                else Console.WriteLine($"{invalidNewDateFormatMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        public void CalcAcceptedSubmissionsRatio() {
            ctx.Open();

            Console.WriteLine("\nFILL THE FIELDS: <CONFERENCE NAME> <CONFERENCE YEAR>");
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
                    Console.WriteLine($"ACCEPTED SUBMISSION RATIO: {ratio}%");
                    Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
                }
                else Console.WriteLine("IT WAS NOT POSSIBLE TO CALCULATE THE RATIO. PRESS ANY KEY TO TRY AGAIN.");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);

            ctx.Dispose();
        }

        //TO-DO
        public void ChangeSubmissionState() {throw new NotImplementedException();}
    }
}