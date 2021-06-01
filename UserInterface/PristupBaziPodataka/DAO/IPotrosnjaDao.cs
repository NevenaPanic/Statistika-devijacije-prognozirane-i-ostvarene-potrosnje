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
    }
}
