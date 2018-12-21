using ConferenceManager.DAL;
using ConferenceManager.DAL.mapper;
using ConferenceManager.DAL.mapper.interfaces;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ConferenceManager.Concrete
{
    class RevisaoMapper : AbstractMapper<Revisao, Tuple<int, string, string, int>, List<Revisao>>, IRevisaoMapper
    {
        public RevisaoMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Revisao";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE idArtigo = @idArtigo AND emailRevisor = @emailRevisor AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia", SelectAllCommandText); ;
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Revisao (idArtigo, emailRevisor, responsavel, nomeConferencia, anoConferencia) VALUES(@idArtigo, @emailRevisor, @responsavel, @nomeConferencia, @anoConferencia)";
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Revisao SET responsavel = @responsavel WHERE idArtigo = @idArtigo AND emailRevisor = @emailRevisor AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "DELETE FROM Revisao WHERE idArtigo = @idArtigo AND emailRevisor = @emailRevisor AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia";
            }
        }


        protected override Revisao Map(IDataRecord record)
        {
            Revisao r = new Revisao();
            r.NotaMinima = record.GetInt32(0);
            r.Nota = record.GetInt32(1);
            r.Texto = record.GetString(2);
            r.IDArtigo = record.GetInt32(3);
            r.EmailRevisor = record.GetString(4);
            r.NomeConferencia = record.GetString(5);
            r.AnoConferencia = record.GetInt32(6);
            return r;
        }


        protected override void SelectParameters(IDbCommand command, Tuple<int, string, string, int> k)
        {
            SqlParameter idArtigoSelectDelete = new SqlParameter("@idArtigo", k.Item1);
            SqlParameter emailRevisorSelectDelete = new SqlParameter("@emailRevisor", k.Item2);
            SqlParameter nomeConferenciaSelectDelete = new SqlParameter("@nomeConferencia", k.Item3);
            SqlParameter anoConferenciaSelectDelete = new SqlParameter("@anoConferencia", k.Item4);
            command.Parameters.Add(idArtigoSelectDelete);
            command.Parameters.Add(emailRevisorSelectDelete);
            command.Parameters.Add(nomeConferenciaSelectDelete);
            command.Parameters.Add(anoConferenciaSelectDelete);
        }

        protected override void InsertParameters(IDbCommand command, Revisao r)
        {
            SqlParameter notaInsertUpdate = new SqlParameter("@nota", r.Nota);
            SqlParameter notaMinimaInsertUpdate = new SqlParameter("@notaMinima", r.NotaMinima);
            SqlParameter textoInsertUpdate = new SqlParameter("@texto", r.Texto);
            SqlParameter idArtigoInsertUpdate = new SqlParameter("@idArtigo", r.IDArtigo);
            SqlParameter emailRevisorInsertUpdate = new SqlParameter("@emailRevisor", r.EmailRevisor);
            SqlParameter nomeConferenciaInsertUpdate = new SqlParameter("@nomeConferencia", r.NomeConferencia);
            SqlParameter anoConferenciaInsertUpdate = new SqlParameter("@anoConferencia", r.AnoConferencia);

            /*p1.Direction = ParameterDirection.InputOutput;
            if (s.IDArtigo != null)
                p1.Value = s.IDArtigo;
            else
                p1.Value = DBNull.Value;
            */

            command.Parameters.Add(notaInsertUpdate);
            command.Parameters.Add(notaMinimaInsertUpdate);
            command.Parameters.Add(textoInsertUpdate);
            command.Parameters.Add(idArtigoInsertUpdate);
            command.Parameters.Add(emailRevisorInsertUpdate);
            command.Parameters.Add(nomeConferenciaInsertUpdate);
            command.Parameters.Add(anoConferenciaInsertUpdate);
        }

        protected override void DeleteParameters(IDbCommand command, Revisao r)
            => SelectParameters(command, new Tuple<int, string, string, int>(r.IDArtigo, r.EmailRevisor, r.NomeConferencia, r.AnoConferencia));

        protected override Revisao UpdateEntityKey(IDbCommand command, Revisao r)
        {
            var idArtigoUpdateKey = command.Parameters["@idArtigo"] as SqlParameter;
            var emailRevisorUpdateKey = command.Parameters["@emailRevisor"] as SqlParameter;
            var nomeConferenciaUpdateKey = command.Parameters["@nomeConferencia"] as SqlParameter;
            var anoConferenciaUpdateKey = command.Parameters["@anoConferencia"] as SqlParameter;

            r.IDArtigo = int.Parse(idArtigoUpdateKey.Value.ToString());
            r.EmailRevisor = emailRevisorUpdateKey.Value.ToString();
            r.NomeConferencia = nomeConferenciaUpdateKey.Value.ToString();
            r.AnoConferencia = int.Parse(anoConferenciaUpdateKey.Value.ToString());
            return r;
        }

        protected override void UpdateParameters(IDbCommand command, Revisao r) => InsertParameters(command, r);

        // ----------------------------- FUNCIONALIDADES ------------------------------

        protected string NewReviserText
        {
            get
            {
                return "ConferênciaAcadémica.AtribuirPapelRevisor";
            }

        }

        protected void NewReviserParameters(SqlCommand cmd, Revisao r)
        {
            cmd.Parameters.Add(new SqlParameter("@idArtigo", r.IDArtigo));
            cmd.Parameters.Add(new SqlParameter("@email", r.EmailRevisor));
            cmd.Parameters.Add(new SqlParameter("@nomeConferencia", r.NomeConferencia));
            cmd.Parameters.Add(new SqlParameter("@anoConferencia", r.AnoConferencia));
        }

        public void ExecNewReviser(Context ctx, Revisao r)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using (SqlCommand cmd = ctx.createCommand())
                    {
                        //SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd.CommandText = NewReviserText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        NewReviserParameters(cmd, r);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTA CONFERÊNCIA FOI ATUALIZADA.");
                        tran.Complete();
                    }
                }
                catch (SqlException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

        }

        protected string AssignReviserText
        {
            get
            {
                return "ConferênciaAcadémica.AtribuirRevisor";
            }

        }

        protected void AssignReviserParameters(SqlCommand cmd, Revisao r)
        {
            cmd.Parameters.Add(new SqlParameter("@emailRevisor", r.EmailRevisor));
            cmd.Parameters.Add(new SqlParameter("@idArtigo", r.IDArtigo));
            cmd.Parameters.Add(new SqlParameter("@nomeConferencia", r.NomeConferencia));
            cmd.Parameters.Add(new SqlParameter("@anoConferencia", r.AnoConferencia));
        }

        public void ExecAssignReviser(Context ctx, Revisao r)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using (SqlCommand cmd = ctx.createCommand())
                    {
                        //SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                        cmd.CommandText = AssignReviserText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        AssignReviserParameters(cmd, r);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTA CONFERÊNCIA FOI ATUALIZADA.");
                        tran.Complete();
                    }
                }
                catch (SqlException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        protected string ExecRegisterRevisionText
        {
            get
            {
                return "ConferênciaAcadémica.RegistarRevisao";
            }

        }

        protected void ExecRegisterRevisionParameters(SqlCommand cmd, string dataRevisao, Revisao r)
        {
            cmd.Parameters.Add(new SqlParameter("@dataRevisao", dataRevisao));
            cmd.Parameters.Add(new SqlParameter("@notaMinima", r.NotaMinima));
            cmd.Parameters.Add(new SqlParameter("@nota", r.Nota));
            cmd.Parameters.Add(new SqlParameter("@texto", r.Texto));
            cmd.Parameters.Add(new SqlParameter("@idArtigo", r.IDArtigo));
            cmd.Parameters.Add(new SqlParameter("@emailRevisor", r.EmailRevisor));
            cmd.Parameters.Add(new SqlParameter("@nomeConferencia", r.NomeConferencia));
            cmd.Parameters.Add(new SqlParameter("@anoConferencia", r.AnoConferencia));
        }

        public void ExecRegisterRevision(Context ctx, string dataRevisao, Revisao r)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using (SqlCommand cmd = ctx.createCommand())
                    {
                        //SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                        cmd.CommandText = ExecRegisterRevisionText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        ExecRegisterRevisionParameters(cmd, dataRevisao, r);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        Console.WriteLine("OPERAÇÃO CONCLUÍDA COM SUCESSO! ESTA CONFERÊNCIA FOI ATUALIZADA.");
                        tran.Complete();
                    }
                }
                catch (SqlException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
