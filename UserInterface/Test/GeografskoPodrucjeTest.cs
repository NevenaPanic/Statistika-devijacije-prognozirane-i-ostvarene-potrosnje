using DeljeniPodaci;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class GeografskoPodrucjeTest
    {
        [Test]
        [TestCase("Kikinda", null)]
        [TestCase(null, "Kik")]
        public void KonstruktorSaNullArgumentimaTest(string ime, string sifra)
        {
            Assert.Throws<ArgumentNullException>(() => new GeografskoPodrucje(ime, sifra));
        }

        [Test]
        [TestCase("Kikinda", "")]
        [TestCase("", "Kik")]
        public void KonstruktorSaPraznimStringomTest(string ime, string sifra)
        {
            Assert.Throws<ArgumentException>(() => new GeografskoPodrucje(ime, sifra));
        }

        [Test]
        [TestCase("Novi Sad", "NS")]
        public void KonstruktorSaDobrimArgumentimaTest(string ime, string sifra)
        {
            GeografskoPodrucje novoGP = new GeografskoPodrucje(ime, sifra);
            Assert.AreEqual(ime, novoGP.Ime);
            Assert.AreEqual(sifra, novoGP.Sifra);
        }

        [Test]
        [TestCase("Ime")]
        public void PropertyImeTest(string ime)
        {
            GeografskoPodrucje gp = new GeografskoPodrucje();
            gp.Ime = ime;
            Assert.AreEqual(gp.Ime, ime);
        }

        [Test]
        [TestCase("Sifra")]
        public void PropertySifraTest(string sifra)
        {
            GeografskoPodrucje gp = new GeografskoPodrucje();
            gp.Sifra = sifra;
            Assert.AreEqual(gp.Sifra, sifra);
        }

    }
}
