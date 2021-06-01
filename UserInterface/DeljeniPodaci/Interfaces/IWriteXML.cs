using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeljeniPodaci.Interfaces
{
    public interface IWriteXML
    {
        void Write(string pocetak, string kraj, string oblast, string kvadratna, string apsolutna, string putanja);
    }
}
