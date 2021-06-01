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
            // select * from ostvarena_potrosnja where sat = 5 and sifraOblasti = 'VOJ' and datumPotrosnje = TO_DATE('2020/5/7' ,'YYYY-MM-DD');
            string query = "select * from " + imeTabele +
                            " where sat = :id_sat " +
                            " and datumPotrosnje = TO_DATE(:id_datum, 'YYYY/MM/DD HH24:MI:SS') " +
                            " and sifraOblasti LIKE :id_oblast";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id_sat", DbType.Int32);
                ParameterUtil.AddParameter(command, "id_datum", DbType.String);
                ParameterUtil.AddParameter(command, "id_oblast", DbType.String);
                command.Prepare();

                string pomDatum = p.DatumPotrosnje.ToString("yyyy/MM/dd HH:mm:ss");

                ParameterUtil.SetParameterValue(command, "id_sat", p.Sat);
                ParameterUtil.SetParameterValue(command, "id_datum", pomDatum);
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

       
        private void Upisi(Potrosnja p, IDbConnection connection, string imeTabele)
        {
            /*
               Upozorenje:
               delete from ostvarena_potrosnja;
               delete from prognozirana_potrosnja;
               commit
               U slucaju da rucno zelite da obrisete podatke iz tabela, uraditi to izvrsavanjem redom prethodno navedenih komandi!
           */
            String insertSql =  "insert into " + imeTabele + " (kolicina, imeFajla, vremeUcitavanjaFajla, datumPotrosnje, sat, sifraOblasti) " +
                                "values (:kolicina_p, :imeFajla_p, :vremeUcitavanjaFajla_p, :datumP_p, :sat_p, :sifraOblasti_p)";
            String updateSql =  "update " + imeTabele + " set " +
                                "kolicina = :kolicina_p, " +
                                "imeFajla = :imeFajla_p, " +
                                "vremeUcitavanjaFajla = :vremeUcitavanjaFajla_p " +
                                "where datumPotrosnje = :datumP_p " +
                                "and sat = :sat_p " +
                                "and sifraOblasti = :sifraOblasti_p";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = PostojiPoId(p, connection, imeTabele) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "kolicina_p", DbType.Double);
                ParameterUtil.AddParameter(command, "imeFajla_p", DbType.String);
                ParameterUtil.AddParameter(command, "vremeUcitavanjaFajla_p", DbType.String);
                ParameterUtil.AddParameter(command, "datumP_p", DbType.Date);
                ParameterUtil.AddParameter(command, "sat_p", DbType.Int32);
                ParameterUtil.AddParameter(command, "sifraOblasti_p", DbType.String);
                command.Prepare();
                string pomDatum = p.DatumPotrosnje.ToString("yyyy/MM/dd HH:mm:ss");
                ParameterUtil.SetParameterValue(command, "kolicina_p", p.Kolicina);
                ParameterUtil.SetParameterValue(command, "imeFajla_p", p.ImeFajla);
                ParameterUtil.SetParameterValue(command, "vremeUcitavanjaFajla_p", p.VremeUcitavanjaFajla.ToString());
                ParameterUtil.SetParameterValue(command, "datumP_p", p.DatumPotrosnje);
                ParameterUtil.SetParameterValue(command, "sat_p", p.Sat);
                ParameterUtil.SetParameterValue(command, "sifraOblasti_p", p.SifraOblasti);
                command.ExecuteNonQuery();
            }
        }

        // Get metod za sve zadovoljavajuce potrosnje
        public List<Potrosnja> SvePotrosnjeIntervala(DateTime datumPocetka, DateTime datumKraja, string oblast, string ImeTabele)
        {
            string pomP = datumPocetka.Day.ToString() + "-" + datumPocetka.Month.ToString() + "-" + datumPocetka.Year.ToString();
            string pomK = datumKraja.Day.ToString() + "-" + datumKraja.Month.ToString() + "-" + datumKraja.Year.ToString();
            List<Potrosnja> listaPotrosnja = new List<Potrosnja>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = " select kolicina, datumPotrosnje" +
                                          " from " + ImeTabele +
                                          " where sifraOblasti like :oblast" +
                                          " and datumPotrosnje between TO_DATE(:datumP , 'DD-MM-YYYY') and TO_DATE(:datumK , 'DD-MM-YYYY')" +
                                          " ORDER BY datumpotrosnje,sat";

                    ParameterUtil.AddParameter(command, "oblast", DbType.String);
                    ParameterUtil.AddParameter(command, "datumP", DbType.String);
                    ParameterUtil.AddParameter(command, "datumK", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "oblast", oblast);
                    ParameterUtil.SetParameterValue(command, "datumP", pomP);
                    ParameterUtil.SetParameterValue(command, "datumK", pomK);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        Potrosnja temp = new Potrosnja();
                        while (reader.Read())
                        {
                            temp.Kolicina = reader.GetFloat(0);
                            temp.DatumPotrosnje = reader.GetDateTime(1);
                            listaPotrosnja.Add(temp);
                        }
                    }
                }
            }
            return listaPotrosnja;
        }

        // Necemo da radimo ovako
        public double ApsolutnaDevijacija(DateTime datumPocetka, DateTime datumKraja, string oblast) 
        {
            string pomP = datumPocetka.Day.ToString() + "-" + datumPocetka.Month.ToString() + "-" + datumPocetka.Year.ToString();
            string pomK = datumKraja.Day.ToString() + "-" + datumKraja.Month.ToString() + "-" + datumKraja.Year.ToString();

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
                                          " and o.datumPotrosnje between TO_DATE(:datumP , 'DD-MM-YYYY') and TO_DATE(:datumK , 'DD-MM-YYYY')";

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
            string pomP = datumPocetka.Day.ToString() + "-" + datumPocetka.Month.ToString() + "-" + datumPocetka.Year.ToString();
            string pomK = datumKraja.Day.ToString() + "-" + datumKraja.Month.ToString() + "-" + datumKraja.Year.ToString();

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
                                          " and o.datumPotrosnje between TO_DATE(:datumP , 'DD-MM-YYYY') and TO_DATE(:datumK , 'DD-MM-YYYY')";

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
