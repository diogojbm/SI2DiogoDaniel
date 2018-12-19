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

namespace ConferenceManager.Concrete
{
    class InscricaoMapper : AbstractMapper<Inscricao, Tuple<string, int, string>, List<Inscricao>>, IInscricaoMapper
    {
        public InscricaoMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Inscricao";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE nomeConferencia = @nomeConferencia" +
                       "AND anoConferencia = @anoConferencia AND emailUtilizador = @emailUtilizador", SelectAllCommandText); ;
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Inscricao (dataRegisto, nomeConferencia, anoConferencia, emailUtilizador) " +
                       "VALUES (@dataRegisto, @nomeConferencia, @anoConferencia, @emailUtilizador)";
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Inscricao " +
                       "SET dataRegisto = @dataRegisto" +
                       "WHERE nomeConferencia = @nomeConferencia " +
                       "AND anoConferencia = @anoConferencia AND emailUtilizador = @emailUtilizador";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "DELETE FROM Inscricao " +
                       "WHERE nomeConferencia = @nomeConferencia" +
                       "AND anoConferencia = @anoConferencia AND emailUtilizador = @emailUtilizador";
            }
        }


        protected override Inscricao Map(IDataRecord record)
        {
            Inscricao i = new Inscricao();
            i.DataRegisto = record.GetDateTime(0);
            i.NomeConferencia = record.GetString(1);
            i.AnoConferencia = record.GetInt32(2);
            i.EmailUtilizador = record.GetString(3);
            return i;
        }

        protected override void SelectParameters(IDbCommand command, Tuple<string, int, string> k)
        {
            SqlParameter nomeConferenciaSelectDelete = new SqlParameter("@nomeConferencia", k.Item1);
            SqlParameter anoConferenciaSelectDelete = new SqlParameter("@anoConferencia", k.Item2);
            SqlParameter emailUtilizadorSelectDelete = new SqlParameter("@emailUtilizador", k.Item3);
            command.Parameters.Add(nomeConferenciaSelectDelete);
            command.Parameters.Add(anoConferenciaSelectDelete);
        }

        protected override void InsertParameters(IDbCommand command, Inscricao i)
        {
            SqlParameter dataRegistoInsertUpdate = new SqlParameter("@dataRegisto", i.DataRegisto);
            SqlParameter nomeConferenciaInsertUpdate = new SqlParameter("@nomeConferencia", i.NomeConferencia);
            SqlParameter anoConferenciaInsertUpdate = new SqlParameter("@anoConferencia", i.AnoConferencia);
            SqlParameter emailUtilizadorInsertUpdate = new SqlParameter("@emailUtilizador", i.EmailUtilizador);

            /*p1.Direction = ParameterDirection.InputOutput;
            if (i.Email != null)
                p1.Value = i.Email;
            else
                p1.Value = DBNull.Value;
            */
            command.Parameters.Add(dataRegistoInsertUpdate);
            command.Parameters.Add(nomeConferenciaInsertUpdate);
            command.Parameters.Add(anoConferenciaInsertUpdate);
            command.Parameters.Add(emailUtilizadorInsertUpdate);
        }

        protected override void DeleteParameters(IDbCommand command, Inscricao i) =>
            SelectParameters(command, new Tuple<string, int, string>(i.NomeConferencia, i.AnoConferencia, i.EmailUtilizador));

        protected override Inscricao UpdateEntityKey(IDbCommand command, Inscricao i)
        {
            var nomeConferenciaUpdateKey = command.Parameters["@nomeConferencia"] as SqlParameter;
            var anoConferenciaUpdateKey = command.Parameters["@anoConferencia"] as SqlParameter;
            var emailUtilizadorUpdateKey = command.Parameters["@emailUtilizador"] as SqlParameter;

            i.NomeConferencia = nomeConferenciaUpdateKey.Value.ToString();
            i.AnoConferencia = int.Parse(anoConferenciaUpdateKey.Value.ToString());
            i.EmailUtilizador = emailUtilizadorUpdateKey.Value.ToString();
            return i;
        }

        protected override void UpdateParameters(IDbCommand command, Inscricao i)
            => InsertParameters(command, i);
    }
}
