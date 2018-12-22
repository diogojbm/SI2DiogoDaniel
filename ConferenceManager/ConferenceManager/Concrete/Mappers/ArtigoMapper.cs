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
    class ArtigoMapper : AbstractMapper<Artigo, Tuple<int, string, int>, List<Artigo>>, IArtigoMapper
    {
        public ArtigoMapper(IContext ctx) : base(ctx)
        {
        }

        #region HELPER METHODS  
        internal List<Utilizador> LoadRevisores(Artigo a)
        {
            List<Utilizador> lst = new List<Utilizador>();

            UtilizadorMapper um = new UtilizadorMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@identificador", a.Identificador));
            using (IDataReader rd = ExecuteReader("SELECT emailRevisor " +
                                                  "FROM Revisao WHERE identificador = @identificador", parameters))
            {
                while (rd.Read())
                {
                    string emailUtilizadorAInserir = rd.GetString(4);
                    lst.Add(um.Read(emailUtilizadorAInserir));
                }
            }
            return lst;
        }

        internal List<Utilizador> LoadAutores(Artigo a)
        {
            List<Utilizador> lst = new List<Utilizador>();

            UtilizadorMapper um = new UtilizadorMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@idArtigo", a.Identificador));
            using (IDataReader rd = ExecuteReader("SELECT emailAutor " +
                                                  "FROM Submissao WHERE idArtigo = @idArtigo", parameters))
            {
                while (rd.Read())
                {
                    string emailUtilizadorAInserir = rd.GetString(1);
                    lst.Add(um.Read(emailUtilizadorAInserir));
                }
            }
            return lst;
        }

        internal List<Ficheiro> LoadFicheiros(Artigo a)
        {
            List<Ficheiro> lst = new List<Ficheiro>();

            FicheiroMapper um = new FicheiroMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@idArtigo", a.Identificador));
            using (IDataReader rd = ExecuteReader("SELECT nome, idArtigo, nomeConferencia, anoConferencia " +
                                                  "FROM Ficheiro WHERE idArtigo = @idArtigo", parameters))
            {
                while (rd.Read())
                {
                    string nomeAInserir = rd.GetString(0);
                    int idArtigoAInserir = rd.GetInt32(1);
                    string nomeConferenciaAInserir = rd.GetString(2);
                    int anoConferenciaAInserir = rd.GetInt32(3);
                    lst.Add(um.Read(new Tuple<string, int, string, int>(nomeAInserir, idArtigoAInserir, nomeConferenciaAInserir, anoConferenciaAInserir)));
                }
            }
            return lst;
        }
        #endregion

        protected override string SelectAllCommandText
        {
            get
            {
                return "SELECT * FROM Artigo";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} WHERE identificador = @identificador " +
                       "AND nomeConferencia = @nomeConferencia " +
                       "AND anoConferencia = @anoConferencia", SelectAllCommandText); ;
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Artigo (identificador, resumo, dataSubmissao, nomeConferencia, anoConferencia, estado) " +
                       "VALUES (@identificador, @resumo, @dataSubmissao, @nomeConferencia, @anoConferencia, @estado)";
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "UPDATE Artigo " +
                       "SET resumo = @resumo " +
                       "AND dataSubmissao = dataSubmissao " +
                       "AND estado = @estado " +
                       "WHERE identificador = @identificador AND nomeConferencia = @nomeConferencia " +
                       "AND anoConferencia = @anoConferencia";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "DELETE FROM Artigo " +
                       "WHERE identificador = @identificador " +
                       "AND nomeConferencia = @nomeConferencia " +
                       "AND anoConferencia = @anoConferencia";
            }
        }


        protected override Artigo Map(IDataRecord record)
        {
            Artigo a = new Artigo();
            a.Identificador = record.GetInt32(0);
            a.Resumo = record.GetString(1);
            a.DataSubmissao = record.GetDateTime(2);
            a.NomeConferencia = record.GetString(3);
            a.AnoConferencia = record.GetInt32(4);
            a.Estado = record.GetString(5);
            return new ArtigoProxy(a, context);
        }

        public override Artigo Create(Artigo entity)
        {
            return new ArtigoProxy(base.Create(entity), context);
        }

        public override Artigo Update(Artigo entity)
        {
            return new ArtigoProxy(base.Update(entity), context);
        }

        protected override void SelectParameters(IDbCommand command, Tuple<int, string, int> k)
        {
            SqlParameter identificadorSelectDelete = new SqlParameter("@identificador", k.Item1);
            SqlParameter nomeConferenciaSelectDelete = new SqlParameter("@nomeConferencia", k.Item2);
            SqlParameter anoConferenciaSelectDelete = new SqlParameter("@anoConferencia", k.Item3);
            command.Parameters.Add(identificadorSelectDelete);
            command.Parameters.Add(nomeConferenciaSelectDelete);
            command.Parameters.Add(anoConferenciaSelectDelete);
        }

        protected override void InsertParameters(IDbCommand command, Artigo a)
        {
            SqlParameter identificadorInsertUpdate = new SqlParameter("@identificador", a.Identificador);
            SqlParameter resumoInsertUpdate = new SqlParameter("@resumo", a.Resumo);
            SqlParameter dataSubmissaoInsertUpdate = new SqlParameter("@dataSubmissao", a.DataSubmissao);
            SqlParameter nomeConferenciaInsertUpdate = new SqlParameter("@nomeConferencia", a.NomeConferencia);
            SqlParameter anoConferenciaInsertUpdate = new SqlParameter("@anoConferencia", a.AnoConferencia);
            SqlParameter estadoInsertUpdate = new SqlParameter("@estado", a.Estado);

            /*p1.Direction = ParameterDirection.InputOutput;
            if (a.Email != null)
                p1.Value = a.Email;
            else
                p1.Value = DBNull.Value;
            */

            command.Parameters.Add(identificadorInsertUpdate);
            command.Parameters.Add(resumoInsertUpdate);
            command.Parameters.Add(dataSubmissaoInsertUpdate);
            command.Parameters.Add(nomeConferenciaInsertUpdate);
            command.Parameters.Add(anoConferenciaInsertUpdate);
            command.Parameters.Add(estadoInsertUpdate);
        }

        protected override void DeleteParameters(IDbCommand command, Artigo a) =>
            SelectParameters(command, new Tuple<int, string, int>(a.Identificador, a.NomeConferencia, a.AnoConferencia));

        protected override Artigo UpdateEntityKey(IDbCommand command, Artigo a)
        {
            var identificadorUpdateKey = command.Parameters["@identificador"] as SqlParameter;
            var nomeConferenciaUpdateKey = command.Parameters["@nomeConferencia"] as SqlParameter;
            var anoConferenciaUpdateKey = command.Parameters["@anoConferencia"] as SqlParameter;

            a.Identificador = int.Parse(identificadorUpdateKey.Value.ToString());
            a.NomeConferencia = nomeConferenciaUpdateKey.Value.ToString();
            a.AnoConferencia = int.Parse(anoConferenciaUpdateKey.Value.ToString());
            return a;
        }

        protected override void UpdateParameters(IDbCommand command, Artigo a) => InsertParameters(command, a);

        protected string ListCompatibleRevisersText
        {
            get
            {
                return "ConferênciaAcadémica.ListarRevisores";
            }
        }

        protected void ListCompatibleRevisersParameters(SqlCommand cmd, Artigo a)
        {
            cmd.Parameters.Add(new SqlParameter("@idArtigo", a.Identificador));
            cmd.Parameters.Add(new SqlParameter("@nomeConferencia", a.NomeConferencia));
            cmd.Parameters.Add(new SqlParameter("@anoConferencia", a.AnoConferencia));
        }

        public void ExecListCompatibleRevisers(Context ctx, Artigo a)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using (SqlCommand cmd = ctx.createCommand())
                    {
                        cmd.CommandText = ListCompatibleRevisersText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        ListCompatibleRevisersParameters(cmd, a);
                        SqlDataReader dr = cmd.ExecuteReader();
                        Console.WriteLine("LISTA DE REVISORES COMPATÍVEIS:");
                        while (dr.Read())
                            Console.WriteLine(dr[0].ToString());

                        cmd.Parameters.Clear();
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
