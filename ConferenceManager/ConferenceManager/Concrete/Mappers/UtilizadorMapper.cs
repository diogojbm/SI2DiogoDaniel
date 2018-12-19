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
    class UtilizadorMapper : AbstractMapper<Utilizador, string, List<Utilizador>>, IUtilizadorMapper
    {
        public UtilizadorMapper(IContext ctx) : base(ctx)
        {
        }

        #region HELPER METHODS  
        internal Instituicao LoadInstituicao(string nomeInstituicao)
        {
            Instituicao i = new Instituicao();
            InstituicaoMapper im = new InstituicaoMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@nomeInstituicao", nomeInstituicao));
            using (IDataReader rd = ExecuteReader("SELECT nome FROM Instituicao WHERE nomeInstituicao = @nomeInstituicao", parameters))
            {
                string nomeAInserir = rd.GetString(0);
                i = im.Read(nomeAInserir);
            }
            return i;
        }

        #endregion

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Utilizador";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE email = @email", SelectAllCommandText); ;
            }
        }

        internal Instituicao LoadInstituicao(UtilizadorProxy utilizadorProxy)
        {
            throw new NotImplementedException();
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Utilizador (email, nome, nomeInstituicao) VALUES(@email, @nome, @instituicao)";
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Utilizador SET nome = @nome AND nomeInstituicao = @nomeInstituicao WHERE email = @email";
            }
        }

        protected override string DeleteCommandText
        {
            get {
                return "DELETE FROM Utilizador WHERE email = @email";
            }
        }


        protected override Utilizador Map(IDataRecord record)
        {
            Utilizador u = new Utilizador();
            u.Email = record.GetString(0);
            u.Nome = record.GetString(1);
            u.NomeInstituicao = record.GetString(2);
            return new UtilizadorProxy(u, context);
        }

        public override Utilizador Create(Utilizador entity)
        {
            return new UtilizadorProxy(base.Create(entity), context);
        }

        public override Utilizador Update(Utilizador entity)
        {
            return new UtilizadorProxy(base.Update(entity), context);
        }

        protected override void SelectParameters(IDbCommand command, string k)
        {
            SqlParameter emailSelectDelete = new SqlParameter("@email", k);
            command.Parameters.Add(emailSelectDelete);
        }

        protected override void InsertParameters(IDbCommand command, Utilizador u)
        {
            SqlParameter emailInsertUpdate = new SqlParameter("@email", u.Email);
            SqlParameter nomeInsertUpdate = new SqlParameter("@nome", u.Nome);
            SqlParameter nomeInstituicaoInsertUpdate = new SqlParameter("@nomeInstituicao", u.NomeInstituicao);

            /*p1.Direction = ParameterDirection.InputOutput;
            if (u.Email != null)
                p1.Value = u.Email;
            else
                p1.Value = DBNull.Value;
            */
            command.Parameters.Add(emailInsertUpdate);
            command.Parameters.Add(nomeInsertUpdate);
            command.Parameters.Add(nomeInstituicaoInsertUpdate);
        }

        protected override void DeleteParameters(IDbCommand command, Utilizador u) => SelectParameters(command, u.Email);

        protected override Utilizador UpdateEntityKey(IDbCommand command, Utilizador u)
        {
            var emailUpdateKey = command.Parameters["@email"] as SqlParameter;
            u.Email = emailUpdateKey.Value.ToString();
            return u;
        }

        protected override void UpdateParameters(IDbCommand command, Utilizador u) => InsertParameters(command, u);
    }
}
