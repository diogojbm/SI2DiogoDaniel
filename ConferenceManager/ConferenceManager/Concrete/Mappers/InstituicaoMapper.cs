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
    class InstituicaoMapper : AbstractMapper<Instituicao, string, List<Instituicao>>, IInstituicaoMapper
    {
        public InstituicaoMapper(IContext ctx) : base(ctx)
        {
        }

        #region HELPER METHODS  
        internal List<Utilizador> LoadUtilizadores(Instituicao i)
        {
            List<Utilizador> lst = new List<Utilizador>();

            UtilizadorMapper um = new UtilizadorMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@nomeInstituicao", i.Nome));
            using (IDataReader rd = ExecuteReader("SELECT nomeInstituicao FROM Utilizador WHERE nomeInstituicao = @nomeInstituicao", parameters))
            {
                while (rd.Read())
                {
                    string nomeInstituicaoAInserir = rd.GetString(2);
                    lst.Add(um.Read(nomeInstituicaoAInserir));
                }
            }
            return lst;
        }
        #endregion

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Instituicao";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE nome = @nome", SelectAllCommandText); ;
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Instituicao (nome, morada, pais) VALUES(@nome, @morada, @pais)";
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Instituicao SET morada = @morada AND pais = @pais WHERE nome = @nome";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "DELETE FROM Instituicao WHERE nome = @nome";
            }
        }


        protected override Instituicao Map(IDataRecord record)
        {
            Instituicao i = new Instituicao();
            i.Nome = record.GetString(0);
            i.Morada = record.GetString(1);
            i.Pais = record.GetString(2);
            return new InstituicaoProxy(i, context);
        }

        public override Instituicao Create(Instituicao entity)
        {
            return new InstituicaoProxy(base.Create(entity), context);
        }

        public override Instituicao Update(Instituicao entity)
        {
            return new InstituicaoProxy(base.Update(entity), context);
        }

        protected override void SelectParameters(IDbCommand command, string k)
        {
            SqlParameter nomeSelectDelete = new SqlParameter("@nome", k);
            command.Parameters.Add(nomeSelectDelete);
        }

        protected override void InsertParameters(IDbCommand command, Instituicao i)
        {
            SqlParameter nomeInsertUpdate = new SqlParameter("@nome", i.Nome);
            SqlParameter moradaInsertUpdate = new SqlParameter("@morada", i.Morada);
            SqlParameter paisInsertUpdate = new SqlParameter("@pais", i.Pais);

            /*p1.Direction = ParameterDirection.InputOutput;
            if (i. != null)
                p1.Value = i.Email;
            else
                p1.Value = DBNull.Value;
            */
            command.Parameters.Add(nomeInsertUpdate);
            command.Parameters.Add(moradaInsertUpdate);
            command.Parameters.Add(paisInsertUpdate);
        }

        protected override void DeleteParameters(IDbCommand command, Instituicao i) => SelectParameters(command, i.Nome);

        protected override Instituicao UpdateEntityKey(IDbCommand command, Instituicao i)
        {
            var nomeUpdateKey = command.Parameters["@nome"] as SqlParameter;
            i.Nome = nomeUpdateKey.Value.ToString();
            return i;
        }

        protected override void UpdateParameters(IDbCommand command, Instituicao i) => InsertParameters(command, i);
    }
}
