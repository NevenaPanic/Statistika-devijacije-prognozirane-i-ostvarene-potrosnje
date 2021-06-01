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
                ParameterUtil.SetParameterValue(command, "vremeUcitavanjaFajla_p", p.VremeUcitavanjaFajla.ToString());
                command.ExecuteNonQuery();
            }
        }

        public double ApsolutnaDevijacija(DateTime datumPocetka, DateTime datumKraja, string oblast) 
        {
            string pomP = datumPocetka.Year.ToString() + "-" + datumPocetka.Month.ToString() + "-" + datumPocetka.Day.ToString();
            string pomK = datumKraja.Year.ToString() + "-" + datumKraja.Month.ToString() + "-" + datumKraja.Day.ToString();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection()) 
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    /*  SQL upit koji radi !
                        select sum(ABS((o.kolicina - p.kolicina)/o.kolicina*100))/count(o.kolicina)
                        from OSTVARENA_POTROSNJA  o, PROGNOZIRANA_POTROSNJA p
                        where o.sifraOblasti like 'BGD' and p.sifraOblasti = o.sifraoblasti
                        and o.datumPotrosnje = p.datumPotrosnje
                        and o.datumPotrosnje between TO_DATE('2020-1-15' , 'YYYY-MM-DD') and TO_DATE('2020-12-12' , 'YYYY-MM-DD');
                     */
                    command.CommandText = " select cast(sum(ABS((o.kolicina - p.kolicina)/o.kolicina*100))/count(o.kolicina) as NUMERIC(11,7)) AS APSOLUTNA_DEVIJACIJA" +
                                          " from OSTVARENA_POTROSNJA o, PROGNOZIRANA_POTROSNJA p" +
                                          " where o.sifraOblasti like :oblast and p.sifraOblasti = o.sifraOblasti" +    
                                          " and o.datumPotrosnje = p.datumPotrosnje" +
                                          " and o.datumPotrosnje between TO_DATE(:datumP , 'YYYY-MM-DD') and TO_DATE(:datumK , 'YYYY-MM-DD')";

                    ParameterUtil.AddParameter(command, "oblast", DbType.String);
                    ParameterUtil.AddParameter(command, "datumP", DbType.String);
                    ParameterUtil.AddParameter(command, "datumK", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "oblast", oblast);
                    ParameterUtil.SetParameterValue(command, "datumP", pomP);
                    ParameterUtil.SetParameterValue(command, "datumK", pomK);

                    if (command.ExecuteScalar() != DBNull.Value)
                        return Convert.ToDouble(command.ExecuteScalar());
                    else
                        return -1;
                }
            }
        }

        public double KvadratnaDevijacija(DateTime datumPocetka, DateTime datumKraja, string oblast)
        {
            string pomP = datumPocetka.Year.ToString() + "-" + datumPocetka.Month.ToString() + "-" + datumPocetka.Day.ToString();
            string pomK = datumKraja.Year.ToString() + "-" + datumKraja.Month.ToString() + "-" + datumKraja.Day.ToString();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    /*  SQL upit koji radi !
                        select CAST(SQRT(SUM(((o.kolicina - p.kolicina)/o.kolicina*100)*((o.kolicina - p.kolicina)/o.kolicina*100))) as NUMERIC(11,6)) as KVADRATNA_DEVIJACIJA
                        from OSTVARENA_POTROSNJA  o, PROGNOZIRANA_POTROSNJA p
                        where o.sifraOblasti like 'VOJ' and p.sifraOblasti = o.sifraoblasti
                        and o.datumPotrosnje = p.datumPotrosnje
                        and o.datumPotrosnje between TO_DATE('2020-1-15' , 'YYYY-MM-DD') and TO_DATE('2020-12-12' , 'YYYY-MM-DD');
                     */
                    command.CommandText = " select CAST(SQRT(SUM(((o.kolicina - p.kolicina)/o.kolicina*100)*((o.kolicina - p.kolicina)/o.kolicina*100))) as NUMERIC(11,6)) as KVADRATNA_DEVIJACIJA" +
                                          " from OSTVARENA_POTROSNJA o, PROGNOZIRANA_POTROSNJA p" +
                                          " where o.sifraOblasti like :oblast and p.sifraOblasti = o.sifraOblasti" +
                                          " and o.datumPotrosnje = p.datumPotrosnje" +
                                          " and o.datumPotrosnje between TO_DATE(:datumP , 'YYYY-MM-DD') and TO_DATE(:datumK , 'YYYY-MM-DD')";

                    ParameterUtil.AddParameter(command, "oblast", DbType.String);
                    ParameterUtil.AddParameter(command, "datumP", DbType.String);
                    ParameterUtil.AddParameter(command, "datumK", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "oblast", oblast);
                    ParameterUtil.SetParameterValue(command, "datumP", pomP);
                    ParameterUtil.SetParameterValue(command, "datumK", pomK);

                    if (command.ExecuteScalar() != DBNull.Value)
                        return Convert.ToDouble(command.ExecuteScalar());
                    else
                        return -1;
                }
            }
        }
    }
}
