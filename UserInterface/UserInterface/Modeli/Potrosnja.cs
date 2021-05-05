using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class Potrosnja
    {
        DateTime datum;
        uint sat;
        float vrednost;
        string oblast;
        // pracenje podataka iz kog fajla i kada su procitani
        string imeFajla;
        DateTime vremeUvozaFajla;

        public Potrosnja() { }

        public Potrosnja(DateTime datum, uint sat, float vrednost, string oblast, string imeFajla, DateTime vremeUvozaFajla)
        {
            this.Datum = datum;
            this.Sat = sat;
            this.Vrednost = vrednost;
            this.Oblast = oblast;
            this.ImeFajla = imeFajla;
            this.VremeUvozaFajla = vremeUvozaFajla;
        }

        public DateTime Datum { get => datum; set => datum = value; }
        public uint Sat { get => sat; set => sat = value; }
        public float Vrednost { get => vrednost; set => vrednost = value; }
        public string Oblast { get => oblast; set => oblast = value; }
        public string ImeFajla { get => imeFajla; set => imeFajla = value; }
        public DateTime VremeUvozaFajla { get => vremeUvozaFajla; set => vremeUvozaFajla = value; }
    }
}
