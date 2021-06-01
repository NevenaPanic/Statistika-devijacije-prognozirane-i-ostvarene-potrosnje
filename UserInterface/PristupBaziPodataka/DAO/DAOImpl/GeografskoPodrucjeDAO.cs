using DeljeniPodaci;
using PristupBaziPodataka.Connection;
using PristupBaziPodataka.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupBaziPodataka.DAO.DAOImpl
{
    public class GeografskoPodrucjeDAO : IGeograskoPodrucjeDAO
    {
        public void UpisiGP(string sifraOblasti, string ime)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Upisi(sifraOblasti, ime, connection);
            }
        }


        public bool PostojiPoId(string sifraOblasti)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open(); 
                return PostojiPoId(sifraOblasti, connection);
            }
        }

        public bool PostojiPoId(string sifraOblasti, IDbConnection connection)
        {
            string query = "select * from EVIDENCIJA_PODRUCJA where sifra = :id_oblast";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id_oblast", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id_oblast", sifraOblasti);
                return command.ExecuteScalar() != null;
            }
        }

        private void Upisi(string sifraOblasti, string ime, IDbConnection connection)
        {

            String insertSql = "insert into EVIDENCIJA_PODRUCJA (ime, sifra) values (:ime , :id_oblasti)";
            String updateSql = "update EVIDENCIJA_PODRUCJA set ime = :ime where sifra = :id_oblasti";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = PostojiPoId( sifraOblasti, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "ime", DbType.String);
                ParameterUtil.AddParameter(command, "id_oblasti", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "ime", ime);
                ParameterUtil.SetParameterValue(command, "id_oblasti", sifraOblasti);
                command.ExecuteNonQuery();
            }
        }
    }
}
