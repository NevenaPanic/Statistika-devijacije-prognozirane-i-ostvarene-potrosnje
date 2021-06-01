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
        public List<Potrosnja> ProcitajFajl(string putanja) 
        {
            ValidatorFajla vf = new ValidatorFajla();
            List<Potrosnja> potrosnja = new List<Potrosnja>();
            StreamReader reader;

            string[] deloviPutanje = putanja.Split('\\');
            string imeFajla = deloviPutanje[deloviPutanje.Length - 1];

            string tipFajla = imeFajla.Substring(imeFajla.Length-3, 3);
                DateTime datumCitanja = DateTime.Now;
                string[] datumParsiran = imeFajla.Split('_');

            if (vf.ValidatorImenaFajla(imeFajla) == true && vf.ValidatorTipaFajla(putanja))
            { 
                reader = new StreamReader(File.OpenRead(putanja));
                Potrosnja p;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\t');

                    if (!values[0].Equals("Sat"))
                    {
                        // sat, vrednost, oblast
                        int sat;
                        Int32.TryParse(values[0], out sat);

                        float kolicina;
                        float.TryParse(values[1], out kolicina);

                        p = new Potrosnja(new DateTime(Int32.Parse(datumParsiran[1]), Int32.Parse(datumParsiran[2]), Int32.Parse(datumParsiran[3].Substring(0, 2))), sat, kolicina, values[2], imeFajla, datumCitanja);
                        potrosnja.Add(p);

                    }
                }
            }
            return potrosnja;
        }

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
    }
}
