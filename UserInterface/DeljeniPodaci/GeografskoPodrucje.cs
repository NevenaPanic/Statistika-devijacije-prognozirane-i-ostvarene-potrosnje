using DeljeniPodaci.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeljeniPodaci
{
    public class GeografskoPodrucje : IGepgrafskoPodrucje
    {
        string ime;
        string sifra;

        public GeografskoPodrucje() { }
        public GeografskoPodrucje(string ime, string sifra)
        {
            if (ime == null || sifra == null)
                throw new ArgumentNullException();
            if (ime.Trim().Equals(String.Empty) || sifra.Trim().Equals(String.Empty))
                throw new ArgumentException();

            this.ime = ime;
            this.sifra = sifra;
        }

        public string Ime { get => ime; set => ime = value; }
        public string Sifra { get => sifra; set => sifra = value; }
    }
}
