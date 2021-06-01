using NUnit.Framework;
using System;
using DeljeniPodaci;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class PotrosnjaTest
    {
        [Test]
        [TestCase("2021/5/31", 0, 1509, "VOJ", "ostv_2021_02_05", "2021/5/31")]
        [TestCase("2021/5/31", 39, 1509, "VOJ", "ostv_2021_02_05", "2021/5/31")]
        [TestCase("2021/5/31", 1, -300, "VOJ", "ostv_2021_02_05", "2021/5/31")]
        [TestCase("2021/5/31", 1, 1509, "", "ostv_2021_02_05", "2021/5/31")]
        [TestCase("2021/5/31", 1, 1509, "VOJ", "", "2021/5/31")]
        public void KonstruktorLosiArgumentiTest(DateTime datumPotrosnje, int sat, float kolicina, string sifraOblasti, string imeFajla, DateTime vremeUcitavanjaFajla)
        {
            Assert.Throws<ArgumentException>(() => new Potrosnja( datumPotrosnje, sat, kolicina, sifraOblasti, imeFajla, vremeUcitavanjaFajla));
        }

        [Test]
        [TestCase("2021/5/31", 1, 1509, null, "ostv_2021_02_05", "2021/5/31")]
        [TestCase("2021/5/31", 1, 1509, "VOJ", null, "2021/5/31")]
        public void KonstruktorSaNullArgumentimaTest(DateTime datumPotrosnje, int sat, float kolicina, string sifraOblasti, string imeFajla, DateTime vremeUcitavanjaFajla)
        {
            Assert.Throws<ArgumentNullException>(() => new Potrosnja(datumPotrosnje, sat, kolicina, sifraOblasti, imeFajla, vremeUcitavanjaFajla));
        }

        [Test]
        [TestCase("2021/5/31", 1, 1509, "VOJ", "ostv_2021_02_05", "2021/5/31")]
        public void KonstruktorSaDobrimArgumentimaTest(DateTime datumPotrosnje, int sat, float kolicina, string sifraOblasti, string imeFajla, DateTime vremeUcitavanjaFajla)
        {
            Assert.DoesNotThrow(() => new Potrosnja( datumPotrosnje, sat, kolicina, sifraOblasti, imeFajla, vremeUcitavanjaFajla));
        }
        
        [Test]
        [TestCase("2021/5/31", 1, 1255, "LOZ", "ostv_2021_02_05", "2021/5/31")]
        public void KonstruktorSaDobrimArgumentima2Test(DateTime datumPotrosnje, int sat, float kolicina, string sifraOblasti, string imeFajla, DateTime vremeUcitavanjaFajla)
        {
            Potrosnja novaPotrosnja = new Potrosnja( datumPotrosnje, sat, kolicina, sifraOblasti, imeFajla, vremeUcitavanjaFajla);
            Assert.AreEqual(novaPotrosnja.DatumPotrosnje, datumPotrosnje);
            Assert.AreEqual(novaPotrosnja.Sat, sat);
            Assert.AreEqual(novaPotrosnja.Kolicina, kolicina);
            Assert.AreEqual(novaPotrosnja.SifraOblasti, sifraOblasti);
            Assert.AreEqual(novaPotrosnja.ImeFajla, imeFajla);
            Assert.AreEqual(novaPotrosnja.VremeUcitavanjaFajla, vremeUcitavanjaFajla);
        }

        [Test]
        [TestCase("2020/02/5", 5, 525, "NS", "ostv_2020_02_5", "2021/5/31")]
        public void ProveraPropertyTest(DateTime datumPotrosnje, int sat, float kolicina, string sifraOblasti, string imeFajla, DateTime vremeUcitavanjaFajla)
        {
            Potrosnja potosnjaTest = new Potrosnja();
            potosnjaTest.DatumPotrosnje = datumPotrosnje;
            potosnjaTest.Sat = sat;
            potosnjaTest.SifraOblasti = sifraOblasti;
            potosnjaTest.ImeFajla = imeFajla;
            potosnjaTest.VremeUcitavanjaFajla = vremeUcitavanjaFajla;

            Assert.AreEqual(potosnjaTest.DatumPotrosnje, datumPotrosnje);
            Assert.AreEqual(potosnjaTest.Sat, sat);
            Assert.AreEqual(potosnjaTest.SifraOblasti, sifraOblasti);
            Assert.AreEqual(potosnjaTest.ImeFajla, imeFajla);
            Assert.AreEqual(potosnjaTest.VremeUcitavanjaFajla, vremeUcitavanjaFajla);
        }
    }
}
