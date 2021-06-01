using DeljeniPodaci;
using DeljeniPodaci.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProračunDevijacija
{
    public class FunkcijeProračuna : IFunkcijeProracunaDevijacije
    {
        public FunkcijeProračuna() { }
        public double FunkcijaApsoltnaDevijacijaPotrosnje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana) // | (ostvarena - prognozirana) / ostvarena * 100 |
        {
            if (ostvarena == null || prognozirana == null)
                throw new ArgumentNullException();

            if (prognozirana.Count == 0)
                return -2;                      // nema podataka za prognoziranu potrosnju
            else if (ostvarena.Count == 0)
                return -3;                      // nema podataka za ostvarenu potrosnju

            double rezultat = 0;

            for (int i = 0; i < ostvarena.Count; i++)
            {
                if (ostvarena[i].DatumPotrosnje != prognozirana[i].DatumPotrosnje)
                    return -4;                  // ne slazu se podaci unutar baze
                rezultat += Math.Abs((((ostvarena[i].Kolicina - prognozirana[i].Kolicina) * 100) / ostvarena[i].Kolicina));
            }

            return rezultat / ostvarena.Count;
        }

        public double FunkcijaKvadratnaDevijacijaPotrosnje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana)
        {
            if (ostvarena == null || prognozirana == null)
                throw new ArgumentNullException();

            if (prognozirana.Count == 0)
                return -2;                      // nema podataka za prognoziranu potrosnju
            else if (ostvarena.Count == 0)
                return -3;                      // nema podataka za ostvarenu potrosnju

            double rezultat = 0;

            for (int i = 0; i < ostvarena.Count; i++)
            {
                if (ostvarena[i].DatumPotrosnje != prognozirana[i].DatumPotrosnje)
                    return -4;                  // ne slazu se podaci unutar baze

                rezultat += Math.Pow(((ostvarena[i].Kolicina - prognozirana[i].Kolicina) / ostvarena[i].Kolicina * 100), 2);
            }
            rezultat = Math.Sqrt(rezultat);

            return rezultat;
        }
    }
}
