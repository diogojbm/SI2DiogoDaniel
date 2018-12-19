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
    class SubmissaoMapper : AbstractMapper<Submissao, Tuple<int, string, string, int>, List<Submissao>>, ISubmissaoMapper
    {
        public SubmissaoMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Submissao";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE idArtigo = @idArtigo AND emailAutor = @emailAutor AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia", SelectAllCommandText); ;
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Submissao (idArtigo, emailAutor, responsavel, nomeConferencia, anoConferencia) VALUES(@idArtigo, @emailAutor, @responsavel, @nomeConferencia, @anoConferencia)";
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Submissao SET responsavel = @responsavel WHERE idArtigo = @idArtigo AND emailAutor = @emailAutor AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "DELETE FROM Submissao WHERE idArtigo = @idArtigo AND emailAutor = @emailAutor AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia";
            }
        }


        protected override Submissao Map(IDataRecord record)
        {
            Submissao s = new Submissao();
            s.IDArtigo = record.GetInt32(0);
            s.EmailAutor = record.GetString(1);
            s.Responsavel = record.GetBoolean(2);
            s.NomeConferencia = record.GetString(3);
            s.AnoConferencia = record.GetInt32(4);
            return s;
        }

        protected override void SelectParameters(IDbCommand command, Tuple<int, string, string, int> k)
        {
            SqlParameter idArtigoSelectDelete = new SqlParameter("@idArtigo", k.Item1);
            SqlParameter emailAutorSelectDelete = new SqlParameter("@emailAutor", k.Item2);
            SqlParameter nomeConferenciaSelectDelete = new SqlParameter("@nomeConferencia", k.Item3);
            SqlParameter anoConferenciaSelectDelete = new SqlParameter("@anoConferencia", k.Item4);
            command.Parameters.Add(idArtigoSelectDelete);
            command.Parameters.Add(emailAutorSelectDelete);
            command.Parameters.Add(nomeConferenciaSelectDelete);
            command.Parameters.Add(anoConferenciaSelectDelete);
        }

        protected override void InsertParameters(IDbCommand command, Submissao s)
        {
            SqlParameter idArtigoInsertUpdate = new SqlParameter("@idArtigo", s.IDArtigo);
            SqlParameter emailAutorInsertUpdate = new SqlParameter("@emailAutor", s.EmailAutor);
            SqlParameter responsavelInsertUpdate = new SqlParameter("@responsavel", s.Responsavel);
            SqlParameter nomeConferenciaInsertUpdate = new SqlParameter("@nomeConferencia", s.NomeConferencia);
            SqlParameter anoConferenciaInsertUpdate = new SqlParameter("@anoConferencia", s.AnoConferencia);

            /*p1.Direction = ParameterDirection.InputOutput;
            if (s.IDArtigo != null)
                p1.Value = s.IDArtigo;
            else
                p1.Value = DBNull.Value;
            */

            command.Parameters.Add(idArtigoInsertUpdate);
            command.Parameters.Add(emailAutorInsertUpdate);
            command.Parameters.Add(responsavelInsertUpdate);
            command.Parameters.Add(nomeConferenciaInsertUpdate);
            command.Parameters.Add(anoConferenciaInsertUpdate);
        }

        protected override void DeleteParameters(IDbCommand command, Submissao s)
            => SelectParameters(command, new Tuple<int, string, string, int>(s.IDArtigo, s.EmailAutor, s.NomeConferencia, s.AnoConferencia));

        protected override Submissao UpdateEntityKey(IDbCommand command, Submissao s)
        {
            var idArtigoUpdateKey = command.Parameters["@idArtigo"] as SqlParameter;
            var emailAutorUpdateKey = command.Parameters["@emailAutor"] as SqlParameter;
            var nomeConferenciaUpdateKey = command.Parameters["@nomeConferencia"] as SqlParameter;
            var anoConferenciaUpdateKey = command.Parameters["@anoConferencia"] as SqlParameter;

            s.IDArtigo = int.Parse(idArtigoUpdateKey.Value.ToString());
            s.EmailAutor = emailAutorUpdateKey.Value.ToString();
            s.NomeConferencia = nomeConferenciaUpdateKey.Value.ToString();
            s.AnoConferencia = int.Parse(anoConferenciaUpdateKey.Value.ToString());
            return s;
        }

        protected override void UpdateParameters(IDbCommand command, Submissao s) => InsertParameters(command, s);
    }
}
