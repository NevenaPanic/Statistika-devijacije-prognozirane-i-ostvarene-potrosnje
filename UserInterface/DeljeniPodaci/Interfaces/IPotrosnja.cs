using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeljeniPodaci.Interfaces
{
    public interface IPotrosnja
    { 
         DateTime DatumPotrosnje { get; set; }
         int Sat { get; set; }
         float Kolicina { get; set; }
         string SifraOblasti { get; set; }
         string ImeFajla { get; set; }
         DateTime VremeUcitavanjaFajla { get; set; }
    }
}
