using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.ModeliPodataka
{
    public class Potrosnja
    {
        DateTime datumPotrosnje;
        uint sat;
        float kolicina;
        string oblast;

        string imeFajla;
        DateTime vremeUcitavanjaFajla;


        public Potrosnja() { }
        public Potrosnja(DateTime datumPotrosnje, uint sat, float kolicina, string oblast, string imeFajla, DateTime vremeUcitavanjaFajla)
        {
            this.datumPotrosnje = datumPotrosnje;
            this.sat = sat;
            this.kolicina = kolicina;
            this.oblast = oblast;
            this.imeFajla = imeFajla;
            this.vremeUcitavanjaFajla = vremeUcitavanjaFajla;
        }

        public DateTime DatumPotrosnje { get => datumPotrosnje; set => datumPotrosnje = value; }
        public uint Sat { get => sat; set => sat = value; }
        public float Koliicina { get => kolicina; set => kolicina = value; }
        public string Oblast { get => oblast; set => oblast = value; }
        public string ImeFajla { get => imeFajla; set => imeFajla = value; }
        public DateTime VremeUcitavanjaFajla { get => vremeUcitavanjaFajla; set => vremeUcitavanjaFajla = value; }
    }
}
