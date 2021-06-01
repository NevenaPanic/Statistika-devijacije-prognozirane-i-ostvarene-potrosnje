using DeljeniPodaci;
using DeljeniPodaci.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorPodataka
{
    public class ValidatorFajla : IValidatorPodataka
    {
        public ValidatorFajla() { }

        public bool ValidatorImenaFajla(string imeFajla)
        {
            string[] imeParsirano = imeFajla.Split('_');
            int godina, mesec, dan;

            if (!imeParsirano[0].Equals("ostv") && !imeParsirano[0].Equals("prog"))
                return false;

            try
            {
                godina = Int32.Parse(imeParsirano[1]);
                mesec = Int32.Parse(imeParsirano[2]);
                dan = Int32.Parse(imeParsirano[3].Substring(0, 2));

                if (godina < 0 || mesec < 1 || dan < 1 || mesec > 12)
                    return false;

                List<int> meseciSa31Danom = new List<int>() { 1, 3 ,5, 7, 8, 10, 12};

                if (meseciSa31Danom.Contains(mesec))
                {
                    if (dan > 31)
                        return false;
                }
                else if (mesec != 2 && dan > 30)
                    return false;


                if (mesec == 2 && dan >= 29 && godina % 4 != 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidatorTipaFajla(string putanja)
        {
            if (Path.GetExtension(putanja).Equals(".csv"))
                return true;
            else
                return false;
        }

        public bool ValidacijaPodatakaUFajlu(List<Potrosnja> procitano, DateTime datum)
        {
            int brojacSati = 0;
            bool greska = false;
            int brojac = 0;

            if (procitano.Count == 0 || procitano.Count < 23)
                return false;

            if (datum.DayOfWeek.ToString().Equals("Sunday") && datum.Month == 3 && datum.Day > 24 && datum.Day < 32)
                brojac = 23;
            else if (datum.DayOfWeek.ToString().Equals("Sunday") && datum.Month == 10 && datum.Day > 24 && datum.Day < 32)
                brojac = 25;
            else
                brojac = 24;

            for (int i = 0; i < procitano.Count - 1; i+= brojac)
            {
                for (int j = 0; j < brojac; j++)
                {
                    brojacSati += procitano[i + j].Sat;
                    if (!procitano[i].SifraOblasti.Equals(procitano[i + j].SifraOblasti))
                        greska = true;
                }
            }

            if (brojac == 23)
            {
                if ( greska == false && brojacSati % 276 == 0)
                    return true;
                else
                    return false;
            }

            if (brojac == 25)
            {
                if (greska == false && brojacSati % 325 == 0)
                    return true;
                else
                    return false;
            }

            if (greska == true || brojacSati % 300 != 0)
                return false;

            return true;
        }

        public string ValidirajPodatke(List<Potrosnja> procitano, DateTime datum)
        {
            string errorPoruka = "";
            ValidatorFajla v = new ValidatorFajla();

            if (v.ValidacijaPodatakaUFajlu(procitano, datum) == false)
                errorPoruka = "Neispravni podaci unutar fajla!";

            return errorPoruka;
        }

        public string ValidirajPutanju(string putanjaFajla)
        {
            string errorPoruka = "";
            ValidatorFajla v = new ValidatorFajla();

            string[] deloviPutanje = putanjaFajla.Split('\\');
            string imeFajla = deloviPutanje[deloviPutanje.Length - 1];

            if (v.ValidatorTipaFajla(putanjaFajla) == false)
                errorPoruka += "Pogresan tip fajla! Molimo Vas unesite novi \".csv\" fajl! \n";
            else if (v.ValidatorImenaFajla(imeFajla) == false)
                errorPoruka += "Neispravan format imena fajla! Molimo Vas unesite novi \".csv\" fajl! \n";

            return errorPoruka;
        }
    }
}
