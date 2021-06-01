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

            String insertSql = "insert into EVIDENCIJA_PODRUCJA (sifra, ime) values (:id_oblasti, :ime)";
            String updateSql = "update EVIDENCIJA_PODRUCJA set sifra = :id_oblasti, ime = :ime";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = PostojiPoId( sifraOblasti, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "id_oblasti", DbType.String);
                ParameterUtil.AddParameter(command, "ime", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id_oblasti", sifraOblasti);
                ParameterUtil.SetParameterValue(command, "ime", ime);
                command.ExecuteNonQuery();
            }
        }
    }
}
