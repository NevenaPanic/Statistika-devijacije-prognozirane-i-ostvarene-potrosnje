using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeljeniPodaci
{
    public class Potrosnja
    {
        DateTime datumPotrosnje;
        int sat;
        float kolicina;
        string sifraOblasti;
        string imeFajla;
        DateTime vremeUcitavanjaFajla;

        public Potrosnja() { }
        public Potrosnja(DateTime datumPotrosnje, int sat, float kolicina, string sifraOblasti, string imeFajla, DateTime vremeUcitavanjaFajla)
        {
            this.datumPotrosnje = datumPotrosnje;
            this.sat = sat;
            this.kolicina = kolicina;
            this.sifraOblasti = sifraOblasti;
            this.imeFajla = imeFajla;
            this.vremeUcitavanjaFajla = vremeUcitavanjaFajla;
        }

        public DateTime DatumPotrosnje { get => datumPotrosnje; set => datumPotrosnje = value; }
        public int Sat { get => sat; set => sat = value; }
        public float Kolicina { get => kolicina; set => kolicina = value; }
        public string SifraOblasti { get => sifraOblasti; set => sifraOblasti = value; }
        public string ImeFajla { get => imeFajla; set => imeFajla = value; }
        public DateTime VremeUcitavanjaFajla { get => vremeUcitavanjaFajla; set => vremeUcitavanjaFajla = value; }

        public override string ToString()
        {
            string r = "\n[DATUM CITANJA]: " + vremeUcitavanjaFajla;
            r += "\n\t[DATUM POTROSNJE]: " + datumPotrosnje.ToShortDateString();
            r += "\n\t[SAT]: " + sat;
            r += "\n\t[KOLICINA]: " + kolicina;
            r += "\n\t[SIFRA OBLASTI]: " + sifraOblasti;
            r += "\n\t[IME FAJLA]: " + imeFajla;

            return r;
        }
    }
}
