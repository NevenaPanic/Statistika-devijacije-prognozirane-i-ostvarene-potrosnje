using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.ModeliPodataka
{
    public class GeografskoPodrucje
    {
        string ime;
        string sifra;

        public GeografskoPodrucje() { }
        public GeografskoPodrucje(string ime, string sifra)
        {
            this.ime = ime;
            this.sifra = sifra;
        }

        public string Ime { get => ime; set => ime = value; }
        public string Sifra { get => sifra; set => sifra = value; }
    }
}
