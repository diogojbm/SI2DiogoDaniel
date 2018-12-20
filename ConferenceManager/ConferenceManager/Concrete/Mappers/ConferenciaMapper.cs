using ConferenceManager.DAL;
using ConferenceManager.DAL.mapper;
using ConferenceManager.DAL.mapper.interfaces;
using ConferenceManager.Mapper;
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
    class ConferenciaMapper : AbstractMapper<Conferencia, Tuple<string, int>, List<Conferencia>>, IConferenciaMapper
    {
        public ConferenciaMapper(IContext ctx) : base(ctx)
        {
        }

        #region HELPER METHODS  
        internal List<Artigo> LoadArtigos(Conferencia c)
        {
            List<Artigo> lst = new List<Artigo>();

            ArtigoMapper am = new ArtigoMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@nomeConferencia", c.Nome));
            parameters.Add(new SqlParameter("@anoConferencia", c.AnoRealizacao));
            using (IDataReader rd = ExecuteReader("SELECT identificador, nomeConferencia, anoConferencia " +
                                                  "FROM Artigo WHERE nomeConferencia = @nomeConferencia " +
                                                  "AND anoConferencia = @anoConferencia", parameters))
            {
                while (rd.Read())
                {
                    int identificadorAInserir = rd.GetInt32(0);
                    string nomeConferenciaAInserir = rd.GetString(1);
                    int anoConferenciaAInserir = rd.GetInt32(2);
                    lst.Add(am.Read(new Tuple<int, string, int>(identificadorAInserir, nomeConferenciaAInserir, anoConferenciaAInserir)));
                }
            }
            return lst;
        }

        internal List<Utilizador> LoadUtilizadores(Conferencia c)
        {
            List<Utilizador> lst = new List<Utilizador>();

            UtilizadorMapper um = new UtilizadorMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@nomeConferencia", c.Nome));
            parameters.Add(new SqlParameter("@anoConferencia", c.AnoRealizacao));
            using (IDataReader rd = ExecuteReader("SELECT emailUtilizador " +
                                                  "FROM Inscricao WHERE nomeConferencia = @nomeConferencia " +
                                                  "AND anoConferencia = @anoConferencia", parameters))
            {
                while (rd.Read())
                {
                    string emailUtilizadorAInserir = rd.GetString(3);
                    lst.Add(um.Read(emailUtilizadorAInserir));
                }
            }
            return lst;
        }
        #endregion

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Conferencia";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE nome = @nome " +
                       "AND anoRealizacao = @anoRealizacao", SelectAllCommandText); ;
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Conferencia (nome, anoRealizacao, acronimo, dataLimiteRevisao, dataLimiteSubmissao, emailPresidente)" +
                    " VALUES(@nome, @anoRealizacao, @acronimo, @dataLimiteRevisao, @dataLimiteSubmissao, @emailPresidente)";
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Conferencia SET acronimo = @acronimo " +
                    "AND dataLimiteRevisao = @dataLimiteRevisao " +
                    "AND dataLimiteSubmissao = @dataLimiteSubmissao " +
                    "AND emailPresidente = @emailPresidente WHERE nome = @nome " +
                    "AND anoRealizacao = @anoRealizacao";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "DELETE FROM Conferencia WHERE nome = @nome " +
                       "AND anoRealizacao = @anoRealizacao";
            }
        }

        protected string UpdateRevisionLimitDateText
        {
            get
            {
                return "ConferênciaAcadémica.AtualizarConferenciaDataLimiteRevisao";
            }

        }

        protected override Conferencia Map(IDataRecord record)
        {
            Conferencia c = new Conferencia();
            c.Nome = record.GetString(0);
            c.AnoRealizacao = record.GetInt32(1);
            c.Acronimo = record.GetString(2);
            c.DataLimiteRevisao = record.GetDateTime(3).ToString("yyyyMMdd");
            c.DataLimiteSubmissao = record.GetDateTime(4).ToString("yyyyMMdd");
            c.EmailPresidente = record.GetString(5);
            return new ConferenciaProxy(c, context);
        }

        public override Conferencia Create(Conferencia entity)
        {
            return new ConferenciaProxy(base.Create(entity), context);
        }

        public override Conferencia Update(Conferencia entity)
        {
            return new ConferenciaProxy(base.Update(entity), context);
        }

        protected override void SelectParameters(IDbCommand command, Tuple<string, int> k)
        {
            command.Parameters.Add(new SqlParameter("@nome", k.Item1));
            command.Parameters.Add(new SqlParameter("@anoRealizacao", k.Item2));
        }

        protected override void InsertParameters(IDbCommand command, Conferencia c)
        {
            /*p1.Direction = ParameterDirection.InputOutput;
            if (c.Email != null)
                p1.Value = c.Email;
            else
                p1.Value = DBNull.Value;
            */

            command.Parameters.Add(new SqlParameter("@nome", c.Nome));
            command.Parameters.Add(new SqlParameter("@anoRealizacao", c.AnoRealizacao));
            command.Parameters.Add(new SqlParameter("@acronimo", c.AnoRealizacao));
            command.Parameters.Add(new SqlParameter("@dataLimiteRevisao", c.DataLimiteRevisao));
            command.Parameters.Add(new SqlParameter("@dataLimiteSubmissao", c.DataLimiteSubmissao));
            command.Parameters.Add(new SqlParameter("@emailPresidente", c.EmailPresidente));
        }

        protected void UpdateRevisionLimitDateParameters(SqlCommand cmd, Conferencia c)
        {
            cmd.Parameters.Add(new SqlParameter("@dataRevisao", c.DataLimiteRevisao));
            cmd.Parameters.Add(new SqlParameter("@nomeConferencia", c.Nome));
            cmd.Parameters.Add(new SqlParameter("@anoConferencia", c.AnoRealizacao));
        }

        protected override void DeleteParameters(IDbCommand command, Conferencia c) =>
            SelectParameters(command, new Tuple<string, int>(c.Nome, c.AnoRealizacao));

        protected override Conferencia UpdateEntityKey(IDbCommand command, Conferencia c)
        {
            var nomeUpdateKey = command.Parameters["@nome"] as SqlParameter;
            var anoRealizacaoUpdateKey = command.Parameters["@anoRealizacao"] as SqlParameter;

            c.Nome = nomeUpdateKey.Value.ToString();
            c.AnoRealizacao = int.Parse(anoRealizacaoUpdateKey.Value.ToString());
            return c;
        }

        protected override void UpdateParameters(IDbCommand command, Conferencia c)
            => InsertParameters(command, c);

        public void ExecUpdateRevisionLimitDate(Context ctx, Conferencia c)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using (SqlCommand cmd = ctx.createCommand())
                    {
                        //SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                        cmd.CommandText = UpdateRevisionLimitDateText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        UpdateRevisionLimitDateParameters(cmd, c);
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
