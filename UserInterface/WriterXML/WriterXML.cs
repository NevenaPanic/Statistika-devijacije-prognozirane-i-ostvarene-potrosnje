using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WriterXML
{
    public class WriterXML
    {
        public WriterXML() { }
        public void Write(DateTime pocetak, DateTime kraj, string oblast, string kvadratna, string apsolutna, string putanja)
        {
            XmlWriter writer = XmlWriter.Create(putanja);

            writer.WriteStartDocument();
            writer.WriteWhitespace("\n");
            writer.WriteStartElement("DEVIJACIJA");
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("OBLAST", oblast);
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("DATUM_POČETKA", pocetak.ToShortDateString());
            writer.WriteWhitespace("\n\t");
            writer.WriteElementString("DATUM_KRAJA", kraj.ToShortDateString());
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
