using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeljeniPodaci.Interfaces
{
    public interface IFunkcijeProracunaDevijacije
    {
        double FunkcijaApsoltnaDevijacijaPotrosnje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana);
        double FunkcijaKvadratnaDevijacijaPotrosnje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana);
    }
}
