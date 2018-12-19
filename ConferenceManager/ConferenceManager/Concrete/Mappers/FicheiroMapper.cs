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

namespace ConferenceManager.Concrete
{
    class FicheiroMapper : AbstractMapper<Ficheiro, Tuple<string, int, string, int>, List<Ficheiro>>, IFicheiroMapper
    {
        public FicheiroMapper(IContext ctx) : base(ctx)
        {
        }

        #region HELPER METHODS  
        internal Artigo LoadArtigo(int idArtigo, string nomeConferencia, int anoConferencia)
        {
            Artigo a = new Artigo();
            ArtigoMapper am = new ArtigoMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@idArtigo", idArtigo));
            parameters.Add(new SqlParameter("@nomeConferencia", nomeConferencia));
            parameters.Add(new SqlParameter("@anoConferencia", anoConferencia));
            using (IDataReader rd = ExecuteReader("SELECT identificador, nomeConferencia, anoConferencia " +
                                                  "FROM Artigo WHERE idArtigo = @idArtigo " +
                                                  "AND nomeConferencia = @nomeConferencia " +
                                                  "AND anoConferencia = @anoConferencia", parameters))
            {
                int identificadorAInserir = rd.GetInt32(0);
                string nomeConferenciaAInserir = rd.GetString(1);
                int anoConferenciaAInserir = rd.GetInt32(2);
                a = am.Read(new Tuple<int, string, int>(identificadorAInserir, nomeConferenciaAInserir, anoConferenciaAInserir));
            }
            return a;
        }

        #endregion

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Ficheiro";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE nome = @nome AND idArtigo = @idArtigo AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia", SelectAllCommandText); ;
            }
        }


        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Ficheiro (nome, idArtigo, responsavel, nomeConferencia, anoConferencia) VALUES(@nome, @idArtigo, @responsavel, @nomeConferencia, @anoConferencia)";
            }
        }

        /*
        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Ficheiro SET responsavel = @responsavel WHERE nome = @nome AND idArtigo = @idArtigo AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia";
            }
        }*/

        protected override string DeleteCommandText
        {
            get
            {
                return "DELETE FROM Ficheiro WHERE nome = @nome AND idArtigo = @idArtigo AND nomeConferencia = @nomeConferencia AND anoConferencia = @anoConferencia";
            }
        }


        protected override Ficheiro Map(IDataRecord record)
        {
            Ficheiro f = new Ficheiro();
            f.Nome = record.GetString(0);
            f.IDArtigo = record.GetInt32(1);
            f.NomeConferencia = record.GetString(2);
            f.AnoConferencia = record.GetInt32(3);
            return new FicheiroProxy(f, context);
        }

        public override Ficheiro Create(Ficheiro entity)
        {
            return new FicheiroProxy(base.Create(entity), context);
        }

        public override Ficheiro Update(Ficheiro entity)
        {
            return new FicheiroProxy(base.Update(entity), context);
        }

        protected override void SelectParameters(IDbCommand command, Tuple<string, int, string, int> k)
        {
            SqlParameter nomeSelectDelete = new SqlParameter("@nome", k.Item1);
            SqlParameter idArtigoSelectDelete = new SqlParameter("@idArtigo", k.Item2);
            SqlParameter nomeConferenciaSelectDelete = new SqlParameter("@nomeConferencia", k.Item3);
            SqlParameter anoConferenciaSelectDelete = new SqlParameter("@anoConferencia", k.Item4);
            command.Parameters.Add(nomeSelectDelete);
            command.Parameters.Add(idArtigoSelectDelete);
            command.Parameters.Add(nomeConferenciaSelectDelete);
            command.Parameters.Add(anoConferenciaSelectDelete);
        }

        protected override void InsertParameters(IDbCommand command, Ficheiro f)
        {
            SqlParameter nomeInsertUpdate = new SqlParameter("@nome", f.Nome);
            SqlParameter idArtigoInsertUpdate = new SqlParameter("@idArtigo", f.IDArtigo);
            SqlParameter nomeConferenciaInsertUpdate = new SqlParameter("@nomeConferencia", f.NomeConferencia);
            SqlParameter anoConferenciaInsertUpdate = new SqlParameter("@anoConferencia", f.AnoConferencia);

            /*p1.Direction = ParameterDirection.InputOutput;
            if (f.IDArtigo != null)
                p1.Value = f.IDArtigo;
            else
                p1.Value = DBNull.Value;
            */

            command.Parameters.Add(nomeInsertUpdate);
            command.Parameters.Add(idArtigoInsertUpdate);
            command.Parameters.Add(nomeConferenciaInsertUpdate);
            command.Parameters.Add(anoConferenciaInsertUpdate);
        }

        protected override void DeleteParameters(IDbCommand command, Ficheiro f)
            => SelectParameters(command, new Tuple<string, int, string, int>(f.Nome, f.IDArtigo, f.NomeConferencia, f.AnoConferencia));

        protected override Ficheiro UpdateEntityKey(IDbCommand command, Ficheiro f)
        {
            var nomeUpdateKey = command.Parameters["@nome"] as SqlParameter;
            var idArtigoUpdateKey = command.Parameters["@idArtigo"] as SqlParameter;
            var nomeConferenciaUpdateKey = command.Parameters["@nomeConferencia"] as SqlParameter;
            var anoConferenciaUpdateKey = command.Parameters["@anoConferencia"] as SqlParameter;

            f.Nome = nomeUpdateKey.Value.ToString();
            f.IDArtigo = int.Parse(idArtigoUpdateKey.Value.ToString());
            f.NomeConferencia = nomeConferenciaUpdateKey.Value.ToString();
            f.AnoConferencia = int.Parse(anoConferenciaUpdateKey.Value.ToString());
            return f;
        }

        protected override void UpdateParameters(IDbCommand command, Ficheiro f) => InsertParameters(command, f);
    }
}
