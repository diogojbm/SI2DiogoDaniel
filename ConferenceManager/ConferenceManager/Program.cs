using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ConferenceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = @"Data Source=VLABSIAD;Initial Catalog=ConferênciaAcadémica;Integrated Security=True";
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "EXEC ConferênciaAcadémica.AtualizarConferenciaDataLimiteSubmissao '20181121', 'B', 2018";
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                            Console.WriteLine(dr[0].ToString() + "-" + dr[1].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
        }
    }
}
