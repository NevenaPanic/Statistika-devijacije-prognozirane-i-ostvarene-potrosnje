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
    public class PotrosnjaDAO : IPotrosnjaDao
    {
        public void UpisiPotrosnju(Potrosnja p, string imeTabele)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Upisi(p, connection, imeTabele);
            }
        }

        public bool PostojiPoId(Potrosnja p, IDbConnection connection, string imeTabele)
        {
            string query = "select * from " + imeTabele +" where sat = :id_sat and datumPotrosnje = TO_DATE(:id_datum, 'YYYY-MM-DD') and sifraOblasti LIKE :id_oblast";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id_sat", DbType.Int32);
                ParameterUtil.AddParameter(command, "id_datum", DbType.Date);
                ParameterUtil.AddParameter(command, "id_oblast", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id_sat", p.Sat);
                ParameterUtil.SetParameterValue(command, "id_datum", p.DatumPotrosnje);
                ParameterUtil.SetParameterValue(command, "id_oblast", p.SifraOblasti);
                return command.ExecuteScalar() != null;
            }
        }

        public void UpisiSvePotrosnje(List<Potrosnja> potrosnje, string imeTabele)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                foreach (Potrosnja p in potrosnje)
                {
                    Upisi(p, connection, imeTabele);
                }

                transaction.Commit();
            }
        }

        public void Upisi(Potrosnja p, IDbConnection connection, string imeTabele)
        {

            String insertSql = "insert into " + imeTabele + "(datumPotrosnje, sat, kolicina, sifraOblasti, imeFajla, vremeUcitavanjaFajla) " +
               "values (:datumP_p, :sat_p, :kolicina_p, :sifraOblasti_p, :imeFajla_p, :vremeUcitavanjaFajla_p)";
            String updateSql = "update" + imeTabele + " set datumPotrosnje = :datumP_p, sat = :sat_p, kolicina = :kolicina_p, sifraOblasti = :sifraOblasti_p, imeFajla = :imeFajla_p, vremeUcitavanjaFajla = :vremeUcitavanjaFajla_p";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = PostojiPoId(p, connection, imeTabele) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "datumP_p", DbType.Date);
                ParameterUtil.AddParameter(command, "sat_p", DbType.Int32);
                ParameterUtil.AddParameter(command, "kolicina_p", DbType.Double);
                ParameterUtil.AddParameter(command, "sifraOblasti_p", DbType.String);
                ParameterUtil.AddParameter(command, "imeFajla_p", DbType.String);
                ParameterUtil.AddParameter(command, "vremeUcitavanjaFajla_p", DbType.String);
                ParameterUtil.SetParameterValue(command, "datumP_p", p.DatumPotrosnje);
                ParameterUtil.SetParameterValue(command, "sat_p", p.Sat);
                ParameterUtil.SetParameterValue(command, "kolicina_p", p.Koliicina);
                ParameterUtil.SetParameterValue(command, "sifraOblasti_p", p.SifraOblasti);
                ParameterUtil.SetParameterValue(command, "imeFajla_p", p.ImeFajla);
                Console.WriteLine(p.VremeUcitavanjaFajla.ToString().Length);
                ParameterUtil.SetParameterValue(command, "vremeUcitavanjaFajla_p", p.VremeUcitavanjaFajla.ToString());
                command.ExecuteNonQuery();
            }
        }
    }
}
