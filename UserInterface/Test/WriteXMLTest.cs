using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriterXML;

namespace Test
{
    [TestFixture]
    public class WriteXMLTest
    {
        WriterXML.WriterXML fun = new WriterXML.WriterXML();

        [Test]
        [TestCase("4/20/2000", "4/27/2000", null, "21,21", "543,2", "nesto")]
        [TestCase("4/20/2000", "4/27/2000", "VOJ", null, "543,2", "nesto")]
        [TestCase("4/20/2000", "4/27/2000", "VOJ", "21,21", null, "nesto")]
        [TestCase("4/20/2000", "4/27/2000", "VOJ", "21,21", "543,2", null)]
        [TestCase(null, "4/27/2000", "VOJ", "21,21", "543,2", "nesto")]
        [TestCase("4/20/2000", null, "VOJ", "21,21", "543,2", "nesto")]
        public void WriteSaNullArgumentimaTest(string pocetak, string kraj, string oblast, string kvadratna, string apsolutna, string putanja)
        {
            Assert.Throws<ArgumentNullException>(() => fun.Write(pocetak, kraj, oblast, kvadratna, apsolutna, putanja));
        }

        [Test]
        [TestCase("4/20/2000", "4/27/2000", "", "21,21", "543,2", "nesto")]
        [TestCase("4/20/2000", "4/27/2000", "VOJ", "", "543,2", "nesto")]
        [TestCase("4/20/2000", "4/27/2000", "VOJ", "21,21", "", "nesto")]
        [TestCase("4/20/2000", "4/27/2000", "VOJ", "21,21", "543,2", "")]
        [TestCase("", "4/27/2000", "VOJ", "21,21", "543,2", "nesto")]
        [TestCase("4/20/2000", "", "VOJ", "21,21", "543,2", "nesto")]
        public void WriteSaPraznimStringovimaTest(string pocetak, string kraj, string oblast, string kvadratna, string apsolutna, string putanja)
        {
            Assert.Throws<ArgumentException>(() => fun.Write(pocetak, kraj, oblast, kvadratna, apsolutna, putanja));
        }

        [Test]
        [TestCase("4/20/2000", "4/27/2000", "VOJ", "12,25", "2,54", "noviFajl.xml")]
        public void WriteXMLDobarTest(string pocetak, string kraj, string oblast, string kvadratna, string apsolutna, string putanja)
        {
            Assert.DoesNotThrow(() => fun.Write(pocetak, kraj, oblast, kvadratna, apsolutna, putanja));
        }
    }
}
