using PristupBazi.Konekcija;
using PristupBazi.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupBazi.DAO
{
    public class PotrosnjaDAO
    {
        public void Find(DateTime datum, int sat, string oblast) 
        {
            using (IDbConnection connection = BazenKonekcija.GetConnection()) 
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand()) 
                {
                    command.CommandText =   "select *"+
                                            " from prognozirana_potrosnja"+
                                            " where datumpotrosnje like TO_DATE(:datum, 'yyyy/mm/dd') and sat = :sat and sifraoblasti like :oblast";
                    ParameterUtil.AddParameter(command, "datum", DbType.DateTime);
                    ParameterUtil.AddParameter(command, "sat", DbType.Int32);
                    ParameterUtil.AddParameter(command, "oblast", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "datum", datum);
                    ParameterUtil.SetParameterValue(command, "sat", sat);
                    ParameterUtil.SetParameterValue(command, "oblast", oblast);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
