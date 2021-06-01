using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeljeniPodaci.Interfaces
{
    public interface IValidatorPodataka
    {
        bool ValidatorImenaFajla(string imeFajla);
        bool ValidatorTipaFajla(string putanja);
        bool ValidacijaPodatakaUFajlu(List<Potrosnja> procitano, DateTime datum);
    }
}
