using ConferenceManagerEF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Transactions;


namespace ConferenceManager
{
    class AppService
    {
        private const string dateFormat = "yyyyMMdd";
        private const string connectionStringName = "name = ConferenceManagerEntities";
        private const string invalidNumberOfArgumentsMessage = "INVALID NUMBER OF ARGUMENTS. PRESS ANY KEY TO TRY AGAIN.";
        private const string pressAnyKeyForANewCommandMessage = "PRESS ANY KEY FOR A NEW COMMAND.";
        private const string invalidNewDateFormatMessage = "ERROR ON NEW DATE'S FORMAT. THE NEW DATE MUST RESPECT YYYYMMDD FORMAT. PRESS ANY KEY TO START AGAIN.";

        public AppService(){

        }

        public void UpdateRevisionLimitDate()
        {
            Console.WriteLine("\nFILL THE FIELDS: <REVISION DATE> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            DateTime date;
            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    using (var ts = new TransactionScope())
                    {
                        using (EntityConnection cn = new EntityConnection(connectionStringName))
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
                                        Console.WriteLine($"NEW REVISION LIMIT DATE: {ic.dataLimiteRevisao.ToString()}");
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                                else Console.WriteLine(e.Message);
                            }
                        }
                    }
                    Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
                }
                else Console.WriteLine($"\n{invalidNewDateFormatMessage}\n");
            }

            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        public void UpdateSubmissionLimitDate()
        {
            Console.WriteLine("\nFILL THE FIELDS: <SUBMISSION DATE> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            DateTime date;
            if (parameters.Length == 3)
            {
                if (DateTime.TryParseExact(parameters[0], dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    using (var ts = new TransactionScope())
                    {
                        using (EntityConnection cn = new EntityConnection(connectionStringName))
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
                                        Console.WriteLine($"NEW SUBMISSION LIMIT DATE: {ic.dataLimiteSubmissao.ToString()}");
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                                else Console.WriteLine(e.Message);
                            }
                        }
                    }
                    Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
                }
                else Console.WriteLine($"{invalidNewDateFormatMessage}\n");
            }

            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        public void UpdateConferencePresident()
        {
            Console.WriteLine("\nFILL THE FIELDS: <PRESIDENT EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 3)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
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
                                    Console.WriteLine($"NEW PRESIDENT EMAIL: {ic.emailPresidente.ToString()}");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }

            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        public void AssignAuthorRole()
        {
            Console.WriteLine("\nFILL THE FIELDS: <ARTICLE ID> <USER EMAIL> <RESPONSIBLE (0/1)> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 5)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
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
                                    Console.WriteLine($"NEW AUTHOR: {subm.emailAutor.ToString()}");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        public void AssignReviserRole()
        {
            Console.WriteLine("\nFILL THE FIELDS: <ARTICLE ID> <USER EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 4)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
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
                                    Console.WriteLine($"NEW REVISER: {rev.emailRevisor.ToString()}");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        public void AssignPresidentRole()
        {
            Console.WriteLine("\nFILL THE FIELDS: <USER EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 3)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
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
                                    Console.WriteLine($"NEW PRESIDENT: {conf.emailPresidente.ToString()}");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"{pressAnyKeyForANewCommandMessage}");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        //TO-DO:
        public void ListRevisers()
        {
            Console.WriteLine("\nFILL THE FIELDS: <ARTICLE ID> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 3)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                int idArtigo = Int32.Parse(parameters[0]);
                                string nomeConferencia = parameters[1];
                                int anoConferencia = Int32.Parse(parameters[2]);
                                //var result = ctx.ListarRevisores(idArtigo, nomeConferencia, anoConferencia);
                                IEnumerable<Utilizador> returnValue = ctx.Database.SqlQuery<Utilizador>("ConferênciaAcadémica.ListarRevisores @idArtigo, @nomeConferencia, @anoConferencia",
                                                    new SqlParameter("idArtigo", idArtigo),
                                                    new SqlParameter("nomeConferencia", nomeConferencia),
                                                    new SqlParameter("anoConferencia", anoConferencia)).AsEnumerable();
                                foreach (var user in returnValue)
                                {
                                    Console.WriteLine("" + user.email);
                                }                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        public void AssignReviser()
        {
            Console.WriteLine("\nFILL THE FIELDS: <REVISER EMAIL> <ARTICLE ID> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 4)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
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
                                    Console.WriteLine($"NEW REVISER: {rev.emailRevisor.ToString()}");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"\n{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        //A DATA TEM DE SER ALGO COMO 20181112 PORQUE A DATA LIMITE DE REVISAO DA CONFERENCIA B 2018 FOI ALTERADA PARA 20181120
        public void RegisterRevision()
        {
            Console.WriteLine("\nFILL THE FIELDS: <REVISION DATE> <MINIMUM GRADE> <GRADE> <TEXT> <ARTICLE ID> <REVISER EMAIL> <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 8)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                DateTime date;
                                if (DateTime.TryParseExact(parameters[0], dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                                {
                                    int notaMinima = Int32.Parse(parameters[1]);
                                    int nota = Int32.Parse(parameters[2]);
                                    string texto = parameters[3];
                                    int idArtigo = Int32.Parse(parameters[4]);
                                    string emailRevisor = parameters[5];
                                    string nomeConferencia = parameters[6];
                                    int anoConferencia = Int32.Parse(parameters[7]);
                                    ctx.RegistarRevisao(date, notaMinima, nota, texto, idArtigo, emailRevisor, nomeConferencia, anoConferencia);
                                    Revisao revisao = ctx.Revisaos.Find(idArtigo, emailRevisor, nomeConferencia, anoConferencia);
                                    Artigo artigo = ctx.Artigoes.Find(idArtigo, nomeConferencia, anoConferencia);
                                    if (revisao != null)
                                    {
                                        ctx.Entry(revisao).State = EntityState.Modified;
                                        ctx.SaveChanges();
                                        ts.Complete();
                                        Console.WriteLine($"NEW REVISION: \nidArtigo: {revisao.idArtigo}\nemailRevisor: {revisao.emailRevisor}\nnomeConferencia: {revisao.nomeConferencia}\nanoConferencia: {anoConferencia}");
                                        Console.WriteLine($"NEW ARTICLE STATE: {artigo.estado}");
                                    }
                                }
                                else Console.WriteLine(invalidNewDateFormatMessage);
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        public void CalcAcceptedSubmissionsRatio()
        {
            Console.WriteLine("\nFILL THE FIELDS: <CONFERENCE NAME> <CONFERENCE YEAR>");
            string[] parameters = Console.ReadLine().Split(' ');

            if (parameters.Length == 2)
            {
                using (var ts = new TransactionScope())
                {
                    using (EntityConnection cn = new EntityConnection(connectionStringName))
                    {
                        try
                        {
                            using (var ctx = new ConferenceManagerEntities())
                            {
                                string nomeConferencia = parameters[0];
                                int anoConferencia = Int32.Parse(parameters[1]);

                                IEnumerable<float> returnValue = ctx.Database.SqlQuery<float>("ConferênciaAcadémica.PercentagemSub @nomeConferencia, @anoConferencia",
                                                    new SqlParameter("nomeConferencia", nomeConferencia),
                                                    new SqlParameter("anoConferencia", anoConferencia));
                                foreach (var percent in returnValue)
                                {
                                    Console.WriteLine($"ACCEPTED SUBMISSIONS RATIO: {percent}");
                                }

                                //Conferencia conf = ctx.Conferencias.Find(nomeConferencia, anoConferencia);
                                /*if (conf != null)
                                {
                                    ctx.Entry(conf).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ts.Complete();
                                }*/
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                            else Console.WriteLine(e.Message);
                        }
                    }
                }
                Console.WriteLine($"{pressAnyKeyForANewCommandMessage}\n");
            }
            else Console.WriteLine(invalidNumberOfArgumentsMessage);
        }

        //TO-DO
        public void ChangeSubmissionState(){ throw new NotImplementedException();}
    }
}