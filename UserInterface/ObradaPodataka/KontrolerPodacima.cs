using DeljeniPodaci;
using PristupBaziPodataka.DAO.DAOImpl;
using ProračunDevijacija;
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
        FunkcijeProračuna funkcije = new FunkcijeProračuna();
        public KontrolerPodacima() { }

        public List<Potrosnja> ProcitajFajl(string putanja)
        {
            ValidatorFajla vf = new ValidatorFajla();
            List<Potrosnja> potrosnja = new List<Potrosnja>();
            StreamReader reader;

            int brojacPodrucja = 0;
            DateTime datumCitanja = DateTime.Now;
            string imeFajla = putanja.Substring(putanja.Length - 19, 19);
            // datum
            string[] datumParsiran = imeFajla.Split('_');
            DateTime datum = new DateTime(Int32.Parse(datumParsiran[1]), Int32.Parse(datumParsiran[2]), Int32.Parse(datumParsiran[3].Substring(0, 2)));

            int indikatorNovogPodrucja;
            if (datum.DayOfWeek.ToString().Equals("Sunday") && datum.Month == 3 && datum.Day > 24 && datum.Day < 32)
                indikatorNovogPodrucja = 23;
            else if (datum.DayOfWeek.ToString().Equals("Sunday") && datum.Month == 10 && datum.Day > 24 && datum.Day < 32)
                indikatorNovogPodrucja = 25;
            else
                indikatorNovogPodrucja = 24;

            if (vf.ValidatorImenaFajla(imeFajla) == true && vf.ValidatorTipaFajla(putanja))
            {
                reader = new StreamReader(File.OpenRead(putanja));
                Potrosnja p;

                while (!reader.EndOfStream)
                {
                    brojacPodrucja++;
                    var line = reader.ReadLine();
                    var values = line.Split('\t');

                    if (!values[0].Equals("Sat"))
                    {
                        // sat, vrednost, oblast
                        int sat;
                        Int32.TryParse(values[0], out sat);

                        float kolicina;
                        float.TryParse(values[1], out kolicina);

                        p = new Potrosnja(datum, sat, kolicina, values[2], imeFajla, datumCitanja);
                        potrosnja.Add(p);

                        if (brojacPodrucja % indikatorNovogPodrucja == 0)
                        {   // provera ako ne postoji GP u bazi upisi ga sa vrednostima sifre na oba mesta
                            if (!g.PostojiPoId(p.SifraOblasti.ToUpper()))
                            {
                                g.UpisiGP(p.SifraOblasti.ToUpper().Trim(), p.SifraOblasti.ToUpper().Trim());
                            }
                        }
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

            return funkcije.FunkcijaApsoltnaDevijacijaPotrosnje(ostvarena,prognozirana);
        }

        public double KvadratnaDevijacijaPotrosnje(DateTime pocetakIntervala, DateTime krajIntervala, string oblast)
        {
            List<Potrosnja> prognozirana = p.SvePotrosnjeIntervala(pocetakIntervala, krajIntervala, oblast, "PROGNOZIRANA_POTROSNJA");
            List<Potrosnja> ostvarena = p.SvePotrosnjeIntervala(pocetakIntervala, krajIntervala, oblast, "OSTVARENA_POTROSNJA");

            return funkcije.FunkcijaKvadratnaDevijacijaPotrosnje(ostvarena, prognozirana);
        }

        public void UpisiGPUBazu(string sifraOblasti, string imeOblasti)
        {
            g.UpisiGP(sifraOblasti.ToUpper().Trim(), imeOblasti);
        }

        public bool PostojiGP(string sifra)
        {
            return g.PostojiPoId(sifra.Trim().ToUpper());
        }
    }
}
