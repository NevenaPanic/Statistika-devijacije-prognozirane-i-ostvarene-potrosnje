using DeljeniPodaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupBaziPodataka.DAO
{
    public interface IPotrosnjaDao
    {
        void UpisiPotrosnju(Potrosnja p, string imeTabele);
        void UpisiSvePotrosnje(List<Potrosnja> potrosnje, string imeTabele);
        List<Potrosnja> SvePotrosnjeIntervala(DateTime datumPocetka, DateTime datumKraja, string oblast, string ImeTabele);
        double ApsolutnaDevijacija(DateTime datumPocetka, DateTime datumKraja, string oblast);
        double KvadratnaDevijacija(DateTime datumPocetka, DateTime datumKraja, string oblast);
    }
}
