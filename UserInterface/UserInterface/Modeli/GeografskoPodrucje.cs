using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class GeografskoPodrucje
    {
        string ime;
        string sifra;

        public GeografskoPodrucje() { }
        public GeografskoPodrucje(string ime, string sifra) { }

        public string Name { get => ime; set => ime = value; }
        public string Sifra { get => sifra; set => sifra = value; }
    }
}
