using DeljeniPodaci;
using PristupBaziPodataka.DAO.DAOImpl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorPodataka;

namespace ObradaPodataka
{
    public class KontrolerPodacima
    {
        PotrosnjaDAO p = new PotrosnjaDAO();
        public KontrolerPodacima() { }

        public List<Potrosnja> ProcitajFajl(string putanja)
        {
            ValidatorFajla vf = new ValidatorFajla();
            List<Potrosnja> potrosnja = new List<Potrosnja>();
            StreamReader reader;

            DateTime datumCitanja = DateTime.Now;
            string imeFajla = putanja.Substring(putanja.Length - 19, 19);
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

        public void UpisFajlovaUBazu(List<Potrosnja> ocekivana, List<Potrosnja> ostvarena)
        {
            p.UpisiSvePotrosnje(ocekivana, "PROGNOZIRANA_POTROSNJA");
            p.UpisiSvePotrosnje(ostvarena, "OSTVARENA_POTROSNJA");
        }

        public double ApsoltnaDevijacijaPotrosnje(DateTime pocetakIntervala, DateTime krajIntervala, string oblast)
        {
            return p.ApsolutnaDevijacija(pocetakIntervala, krajIntervala, oblast);
        }

        public double KvadratnaDevijacijaPotrosnje(DateTime pocetakIntervala, DateTime krajIntervala, string oblast)
        {
            return p.KvadratnaDevijacija(pocetakIntervala, krajIntervala, oblast);
        }

    }
}
