using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupBazi.Konekcija
{
    public class ParametriKonekcije
    {
        //TODO: change connection string
        public static readonly string DATA_SOURCE = "//localhost:1521/xe";

        //TODO: change username and password
        public static readonly string USER_ID = "BazaPodataka";
        public static readonly string PASSWORD = "ftn";
    }
}
