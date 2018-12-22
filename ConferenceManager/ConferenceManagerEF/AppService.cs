using ConferenceManagerEF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.EntityClient;
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

        public void UpdateRevisionLimitDate()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;
            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    using (var ts = new TransactionScope())
                    {
                        using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                        {
                            try
                            {
                                using (var ctx = new ConferenceManagerEntities())
                                {
                                    string nomeConferencia = parameters[1];
                                    int anoConferencia = Int32.Parse(parameters[2]);
                                    ctx.AtualizarConferenciaDataLimiteRevisao(date, nomeConferencia, anoConferencia);
                                    Conferencia ic = ctx.Conferencias.Find(nomeConferencia, anoConferencia);
                                    if (ic != null)
                                    {
                                        ctx.Entry(ic).State = EntityState.Modified;
                                        ctx.SaveChanges();
                                        ts.Complete();
                                        Console.WriteLine("NOVA DATA LIMITE DE REVISAO: " + ic.dataLimiteRevisao.ToString());
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
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
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA SUBMISSÃO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            DateTime date;
            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    using (var ts = new TransactionScope())
                    {
                        using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                        {
                            try
                            {
                                using (var ctx = new ConferenceManagerEntities())
                                {
                                    string nomeConferencia = parameters[1];
                                    int anoConferencia = Int32.Parse(parameters[2]);
                                    ctx.AtualizarConferenciaDataLimiteSubmissao(date, nomeConferencia, anoConferencia);
                                    Conferencia ic = ctx.Conferencias.Find(nomeConferencia, anoConferencia);
                                    if (ic != null)
                                    {
                                        ctx.Entry(ic).State = EntityState.Modified;
                                        ctx.SaveChanges();
                                        ts.Complete();
                                        Console.WriteLine("NOVA DATA LIMITE DE SUBMISSAO: " + ic.dataLimiteSubmissao.ToString());
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
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
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL PRESIDENTE> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();
            if (parameters.Length == 3)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                string emailPresidente = parameters[0];
                                string nomeConferencia = parameters[1];
                                int anoConferencia = Int32.Parse(parameters[2]);
                                ctx.AtualizarConferenciaPresidente(emailPresidente, nomeConferencia, anoConferencia);
                                Conferencia ic = ctx.Conferencias.Find(nomeConferencia, anoConferencia);
                                if (ic != null)
                                {
                                    ctx.Entry(ic).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                    Console.WriteLine("NOVO EMAIL DO PRESIDENTE: " + ic.emailPresidente.ToString());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void AssignAuthorRole()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <RESPONSÁVEL> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Submissao s = new Submissao();
            if (parameters.Length == 5)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                int idArtigo = Int32.Parse(parameters[0]);
                                string emailUtilizador = parameters[1];
                                bool responsavel = false;
                                if (parameters[2].Equals("1")) responsavel = true;
                                string nomeConferencia = parameters[3];
                                int anoConferencia = Int32.Parse(parameters[4]);
                                ctx.AtribuirPapelAutor(idArtigo, emailUtilizador, responsavel, nomeConferencia, anoConferencia);
                                Submissao subm = ctx.Submissaos.Find(idArtigo, emailUtilizador, nomeConferencia, anoConferencia);
                                if (subm != null)
                                {
                                    ctx.Entry(subm).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                    Console.WriteLine("NOVO AUTOR: " + subm.emailAutor.ToString());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void AssignReviserRole()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <EMAIL UTILIZADOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Revisao r = new Revisao();

            if (parameters.Length == 4)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                int idArtigo = Int32.Parse(parameters[0]);
                                string emailUtilizador = parameters[1];
                                string nomeConferencia = parameters[2];
                                int anoConferencia = Int32.Parse(parameters[3]);
                                ctx.AtribuirPapelRevisor(idArtigo, emailUtilizador, nomeConferencia, anoConferencia);
                                Revisao rev = ctx.Revisaos.Find(idArtigo, emailUtilizador, nomeConferencia, anoConferencia);
                                if (rev != null)
                                {
                                    ctx.Entry(rev).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                    Console.WriteLine("NOVO REVISOR: " + rev.emailRevisor.ToString());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void AssignPresidentRole()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL UTILIZADOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            Conferencia c = new Conferencia();

            if (parameters.Length == 3)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                string emailUtilizador = parameters[0];
                                string nomeConferencia = parameters[1];
                                int anoConferencia = Int32.Parse(parameters[2]);
                                ctx.AtribuirPapelPresidente(emailUtilizador, nomeConferencia, anoConferencia);
                                Conferencia conf = ctx.Conferencias.Find(nomeConferencia, anoConferencia);
                                if (conf != null)
                                {
                                    ctx.Entry(conf).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                    Console.WriteLine("NOVO PRESIDENTE: " + conf.emailPresidente.ToString());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        //TO-DO:
        public void ListRevisers()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <ID ARTIGO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 3)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                int idArtigo = Int32.Parse(parameters[0]);
                                string nomeConferencia = parameters[1];
                                int anoConferencia = Int32.Parse(parameters[2]);
                                ctx.ListarRevisores(idArtigo, nomeConferencia, anoConferencia);
                                Conferencia conf = ctx.Conferencias.Find(nomeConferencia, anoConferencia);
                                if (conf != null)
                                {
                                    ctx.Entry(conf).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                    Console.WriteLine("NOVO PRESIDENTE: " + conf.emailPresidente.ToString());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void AssignReviser()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <EMAIL REVISOR> <ID ARTIGO> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            if(parameters.Length == 4)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                string emailUtilizador = parameters[0];
                                int idArtigo = Int32.Parse(parameters[1]);
                                string nomeConferencia = parameters[2];
                                int anoConferencia = Int32.Parse(parameters[3]);
                                ctx.AtribuirRevisor(emailUtilizador, idArtigo, nomeConferencia, anoConferencia);
                                Revisao rev = ctx.Revisaos.Find(idArtigo, emailUtilizador, nomeConferencia, anoConferencia);
                                if (rev != null)
                                {
                                    ctx.Entry(rev).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                    Console.WriteLine("REVISOR: " + rev.emailRevisor.ToString());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        //A DATA TEM DE SER ALGO COMO 20181112 PORQUE A DATA LIMITE DE REVISAO DA CONFERENCIA B 2018 FOI ALTERADA PARA 20181120
        public void RegisterRevision()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <DATA REVISÃO> <NOTA MÍNIMA> <NOTA> <TEXTO> <ID ARTIGO> <EMAIL REVISOR> <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 8)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                DateTime date;
                                if (DateTime.TryParseExact(parameters[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                                {
                                    int notaMinima = Int32.Parse(parameters[1]);
                                    int nota = Int32.Parse(parameters[2]);
                                    string texto = parameters[3];
                                    int idArtigo = Int32.Parse(parameters[4]);
                                    string emailRevisor = parameters[5];
                                    string nomeConferencia = parameters[6];
                                    int anoConferencia = Int32.Parse(parameters[7]);
                                    ctx.RegistarRevisao(date, notaMinima, nota, texto, idArtigo, emailRevisor, nomeConferencia, anoConferencia);
                                    Revisao rev = ctx.Revisaos.Find(idArtigo, emailRevisor, nomeConferencia, anoConferencia);
                                    if (rev != null)
                                    {
                                        ctx.Entry(rev).State = EntityState.Modified;
                                        ctx.SaveChanges();
                                        ts.Complete();
                                        Console.WriteLine("NOVA REVISÃO: \n" + "idArtigo: " + rev.idArtigo + "\nemailRevisor: " + rev.emailRevisor + "\nnomeConferencia: " + rev.nomeConferencia + "\nanoConferencia: " + anoConferencia);
                                    }
                                }
                                else Console.WriteLine();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        public void CalcAcceptedSubmissionsRatio()
        {
            Console.WriteLine();
            Console.WriteLine("PREENCHA OS CAMPOS: <NOME CONFERÊNCIA> <ANO CONFERÊNCIA>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 8)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection("name = ConferenceManagerEntities"))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                string nome = parameters[0];
                                int anoRealizacao = Int32.Parse(parameters[1]);
                                ctx.(date, notaMinima, nota, texto, idArtigo, emailRevisor, nomeConferencia, anoConferencia);
                                Conferencia conf = ctx.Conferencias.Find(nomeConferencia, anoConferencia);
                                if (conf != null)
                                {
                                    ctx.Entry(conf).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("PRESSIONE QUALQUER TECLA PARA UM NOVO COMANDO.");
            }

            else Console.WriteLine("NÚMERO ERRADO DE ARGUMENTOS, PRESSIONE QUALQUER TECLA PARA TENTAR NOVAMENTE.");
        }

        //TO-DO
        public void ChangeSubmissionState() { }
    }
}