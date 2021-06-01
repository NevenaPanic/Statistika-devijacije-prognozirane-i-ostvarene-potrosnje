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

            try
            {
                Int32.Parse(imeParsirano[1]);
                Int32.Parse(imeParsirano[2]);
                Int32.Parse(imeParsirano[3].Substring(0, 2));
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

        public bool ValidacijaPodatakaUFajlu(List<Potrosnja> procitano)
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

            if (greska == true || brojacSati % 300 != 0)
                return false;

            return true;
        }

        public string ValidirajPodatke(List<Potrosnja> procitano)
        {
            string errorPoruka = "";
            ValidatorFajla v = new ValidatorFajla();

            if (v.ValidacijaPodatakaUFajlu(procitano) == false)
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
