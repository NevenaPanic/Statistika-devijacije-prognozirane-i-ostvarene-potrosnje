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
        GeografskoPodrucjeDAO g = new GeografskoPodrucjeDAO();
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

        public double ApsoltnaDevijacijaPotrosnje(DateTime pocetakIntervala, DateTime krajIntervala, string oblast) // | (ostvarena - prognozirana) / ostvarena * 100 |
        {
            List<Potrosnja> prognozirana = p.SvePotrosnjeIntervala(pocetakIntervala, krajIntervala, oblast, "PROGNOZIRANA_POTROSNJA");
            List<Potrosnja> ostvarena = p.SvePotrosnjeIntervala(pocetakIntervala, krajIntervala, oblast, "OSTVARENA_POTROSNJA");

            if (prognozirana.Count == 0)
                return -2;                      // nema podataka za prognoziranu potrosnju
            else if (ostvarena.Count == 0)
                return -3;                      // nema podataka za ostvarenu potrosnju

            for (int i = 0; i < prognozirana.Count; i += 24)
            {
                if (ostvarena[i].DatumPotrosnje != prognozirana[i].DatumPotrosnje)
                    return -4;                  // ne slazu se podaci unutar baze
            }

            double rezultat = 0;

            for (int i = 0; i < ostvarena.Count; i++)
            {
                rezultat += Math.Abs((((ostvarena[i].Kolicina - prognozirana[i].Kolicina) * 100 ) / ostvarena[i].Kolicina));
            }

            return rezultat / ostvarena.Count;
        }

        public double KvadratnaDevijacijaPotrosnje(DateTime pocetakIntervala, DateTime krajIntervala, string oblast)
        {
            List<Potrosnja> prognozirana = p.SvePotrosnjeIntervala(pocetakIntervala, krajIntervala, oblast, "PROGNOZIRANA_POTROSNJA");
            List<Potrosnja> ostvarena = p.SvePotrosnjeIntervala(pocetakIntervala, krajIntervala, oblast, "OSTVARENA_POTROSNJA");

            if (prognozirana.Count == 0)
                return -2;                      // nema podataka za prognoziranu potrosnju
            else if (ostvarena.Count == 0)
                return -3;                      // nema podataka za ostvarenu potrosnju

            for (int i = 0; i < prognozirana.Count; i += 24)
            {
                if (ostvarena[i].DatumPotrosnje != prognozirana[i].DatumPotrosnje)
                    return -4;                  // ne slazu se podaci unutar baze
            }

            double rezultat = 0;

            for (int i = 0; i < ostvarena.Count; i++)
            {
                rezultat += Math.Pow(((ostvarena[i].Kolicina - prognozirana[i].Kolicina) / ostvarena[i].Kolicina * 100), 2);
            }
            rezultat =  Math.Sqrt(rezultat);

            return rezultat;
        }

        public void UpisiGPUBazu(string sifraOblasti, string imeOblasti)
        {
            g.UpisiGP(sifraOblasti, imeOblasti);
        }

    }
}
