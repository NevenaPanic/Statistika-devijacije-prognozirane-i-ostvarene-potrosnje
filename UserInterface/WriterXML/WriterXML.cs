using DeljeniPodaci.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WriterXML
{
    public class WriterXML : IWriteXML
    {
        public WriterXML() { }
        public void Write(string pocetak, string kraj, string oblast, string kvadratna, string apsolutna, string putanja)
        {
            if (pocetak == null || kraj == null || oblast == null || kvadratna == null || apsolutna == null || putanja == null)
                throw new ArgumentNullException("Argumenti ne smeju biti null vrednosti!");

            if (pocetak.Trim() == "")
                throw new ArgumentException();
            
            if (kraj.Trim() == "")
                throw new ArgumentException();

            if (oblast.Trim() == "")
                throw new ArgumentException("Niste uneli oblast!");

            if (kvadratna.Trim() == "")
                throw new ArgumentException("Nije popunjen rezultat kvadratne devijacije!");

            if (apsolutna.Trim() == "")
                throw new ArgumentException("Nije popunjen rezultat apsoliutne devijacije!");

            if (putanja.Trim() == "")
                throw new ArgumentException("Nije uneta putanja u koju zelite da snimite fajl!");

            XmlWriter writer = XmlWriter.Create(putanja);

            writer.WriteStartDocument();
            writer.WriteWhitespace("\n");
            writer.WriteStartElement("DEVIJACIJA");
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("OBLAST", oblast);
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("DATUM_POČETKA", pocetak);
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("DATUM_KRAJA", kraj);
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("KVADRATNA_DEVIJACIJA", kvadratna.ToString());
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("APSOLUTNA_SREDNJA_DEVIJACIJA", apsolutna.ToString());
            writer.WriteWhitespace("\n");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    }
}
