using DeljeniPodaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProračunDevijacija
{
    public class FunkcijeProračuna
    {
        public FunkcijeProračuna() { }
        public double FunkcijaApsoltnaDevijacijaPotrosnje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana) // | (ostvarena - prognozirana) / ostvarena * 100 |
        {
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
                rezultat += Math.Abs((((ostvarena[i].Kolicina - prognozirana[i].Kolicina) * 100) / ostvarena[i].Kolicina));
            }

            return rezultat / ostvarena.Count;
        }

        public double FunkcijaKvadratnaDevijacijaPotrosnje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana)
        {
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
            rezultat = Math.Sqrt(rezultat);

            return rezultat;
        }
    }
}
