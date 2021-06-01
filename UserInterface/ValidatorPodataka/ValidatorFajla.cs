using DeljeniPodaci;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorPodataka
{
    public class ValidatorFajla
    {
        public ValidatorFajla() { }
      
        public bool ValidatorImenaFajla(string imeFajla) 
        {
            string[] imeParsirano = imeFajla.Split('_');
            int godina, mesec, dan;
            try
            {
                godina = Int32.Parse(imeParsirano[1]);
                mesec = Int32.Parse(imeParsirano[2]);
                dan = Int32.Parse(imeParsirano[3].Substring(0, 2));

                if (godina < 0 || mesec < 1 || dan < 1)
                    return false;
                List<int> meseciSa31Danom = new List<int>();
                meseciSa31Danom.Add(1); meseciSa31Danom.Add(3); meseciSa31Danom.Add(5); meseciSa31Danom.Add(7);
                meseciSa31Danom.Add(8); meseciSa31Danom.Add(10); meseciSa31Danom.Add(12);

                if (meseciSa31Danom.Contains(mesec))
                {
                    if (dan > 31)
                        return false;
                }
                else if (mesec != 2)
                {
                    if (dan > 30)
                        return false;
                }


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

            for (int i = 0; i < procitano.Count - 1; i+= 24)
            {
                for (int j = 0; j < 24; j++)
                {
                    brojacSati += procitano[i + j].Sat;
                    if (!procitano[i].SifraOblasti.Equals(procitano[i + j].SifraOblasti))
                        greska = true;
                }
            }

            if (datum.DayOfWeek.Equals("Sunday") && datum.Month == 3 && datum.Day > 24 && datum.Day < 32)
            {
                if (brojacSati == 23 && greska == false)
                    return true;
                else
                    return false;
            }

            if (datum.DayOfWeek.Equals("Sunday") && datum.Month == 10 && datum.Day > 24 && datum.Day < 32)
            {
                if (brojacSati == 25 && greska == false)
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
