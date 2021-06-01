using DeljeniPodaci;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorPodataka;

namespace Test
{
    [TestFixture]
    public class ValidatorFajlaTest
    {
        ValidatorFajla fun = new ValidatorFajla();

        [Test]
        public void ValidatorImenaFajlaTest()
        {
            string loseIme = "ostv_lose2_lose_20";
            string loseIme2 = "ostv_2021_52_04";
            string loseIme3 = "lose_2021_52_04";
            string loseIme4 = "prog_2021_07_35";
            string loseIme5 = "prog_2021_06_31";
            string loseIme6 = "prog_2021_02_29";
            string loseIme7 = "prog_-2021_02_29";
            string dobroIme = "ostv_2021_07_07";

            Assert.IsFalse(fun.ValidatorImenaFajla(loseIme));
            Assert.IsFalse(fun.ValidatorImenaFajla(loseIme2));
            Assert.IsFalse(fun.ValidatorImenaFajla(loseIme3));
            Assert.IsFalse(fun.ValidatorImenaFajla(loseIme4));
            Assert.IsFalse(fun.ValidatorImenaFajla(loseIme5));
            Assert.IsFalse(fun.ValidatorImenaFajla(loseIme6));
            Assert.IsFalse(fun.ValidatorImenaFajla(loseIme7));
            Assert.IsTrue(fun.ValidatorImenaFajla(dobroIme));
        }

        [Test]
        public void ValidatorTipaFajlaTest()
        {
            string losFajl = "C:\\Users\\Stefan\\Desktop\\ostv_2020_05_07.txt";
            string dobarFajl = "C:\\Users\\Stefan\\Desktop\\ostv_2020_05_07.csv";


            Assert.IsFalse(fun.ValidatorTipaFajla(losFajl));
            Assert.IsTrue(fun.ValidatorTipaFajla(dobarFajl));
        }

        [Test]
        public void ValidacijaPodatakaUFajluTest()
        {
            List<Potrosnja> prazna = new List<Potrosnja>();
            List<Potrosnja> manjeOd24 = new List<Potrosnja>();
            List<Potrosnja> losa23h = new List<Potrosnja>();
            List<Potrosnja> dobra23h = new List<Potrosnja>();
            List<Potrosnja> losa25h = new List<Potrosnja>();
            List<Potrosnja> dobra25h = new List<Potrosnja>();
            List<Potrosnja> losiSati = new List<Potrosnja>();
            List<Potrosnja> losaGp = new List<Potrosnja>();
            List<Potrosnja> dobra = new List<Potrosnja>();

            for (int i = 1; i < 10; i++)
            {
                manjeOd24.Add(new Potrosnja(DateTime.Now, i, 10 * i, "BGD", "nevl_2020_02_02.csv", DateTime.Now));
            }

            for (int i = 1; i < 25; i++)
            {
                losiSati.Add(new Potrosnja(DateTime.Now, i + 1, 10 * i, "BGD", "ostv_2020_02_02.csv", DateTime.Now));
            }

            for (int i = 1; i < 24; i++)
            { 
                dobra23h.Add(new Potrosnja(new DateTime(2021, 3, 28), i, 10 * i, "BGD", "ostv_2021_03_28.csv", DateTime.Now));
                losa23h.Add(new Potrosnja(new DateTime(2021, 3, 28), i, 10 * i, "BGD", "ostv_2021_03_28.csv", DateTime.Now));
            }
            losa23h[22].Sat++;

            for (int i = 1; i < 26; i++)
            { 
                dobra25h.Add(new Potrosnja(new DateTime(2020, 10, 25), i, 10 * i, "BGD", "ostv_2020_10_25.csv", DateTime.Now));
                losa25h.Add(new Potrosnja(new DateTime(2020, 10, 25), i, 10 * i, "BGD", "ostv_2020_10_25.csv", DateTime.Now));
            }
            losa25h[24].SifraOblasti = "VOJ";

            for (int i = 1; i < 25; i++)
            {
                dobra.Add(new Potrosnja(DateTime.Now, i, 10 * i, "BGD", "ostv_2020_02_02.csv", DateTime.Now));
            }

            for (int i = 1; i < 24; i++)
            {
                losaGp.Add(new Potrosnja(DateTime.Now, i, 10 * i, "BGD", "ostv_2020_02_02.csv", DateTime.Now));
            }
            losaGp.Add(new Potrosnja(DateTime.Now, 24, 10 * 2, "VOJ", "ostv_2020_02_02.csv", DateTime.Now));

            Assert.IsFalse( fun.ValidacijaPodatakaUFajlu(prazna, new DateTime(2020, 2,2)));
            Assert.IsFalse( fun.ValidacijaPodatakaUFajlu(losiSati, new DateTime(2020, 2, 2)));
            Assert.IsFalse( fun.ValidacijaPodatakaUFajlu(manjeOd24, new DateTime(2020, 2, 2)));
            Assert.IsFalse( fun.ValidacijaPodatakaUFajlu(losa25h, new DateTime(2020, 10, 25)));
            Assert.IsFalse( fun.ValidacijaPodatakaUFajlu(losa23h, new DateTime(2021, 3, 28)));
            Assert.IsFalse( fun.ValidacijaPodatakaUFajlu(losaGp, new DateTime(2020, 2, 2)));

            Assert.IsTrue( fun.ValidacijaPodatakaUFajlu(dobra, new DateTime(2020, 2, 2)));
            Assert.IsTrue( fun.ValidacijaPodatakaUFajlu(dobra23h, new DateTime(2021, 3, 28)));
            Assert.IsTrue( fun.ValidacijaPodatakaUFajlu(dobra25h, new DateTime(2020, 10, 25)));
        }

        [Test]
        [TestCase("\\dobra_putanja\\lose_ime.csv")]
        public void ValidirajFajlPogresnoImeTest(string putanja)
        {
            Assert.AreEqual("Neispravan format imena fajla! Molimo Vas unesite novi \".csv\" fajl! \n", fun.ValidirajPutanju(putanja));
        }
        
        [Test]
        [TestCase("\\dobra_putanja\\ostv_2021_09_09.xml")]
        public void ValidirajFajlPogresanTipTest(string putanja)
        {
            Assert.AreEqual("Pogresan tip fajla! Molimo Vas unesite novi \".csv\" fajl! \n", fun.ValidirajPutanju(putanja));
        }
        
        [Test]
        [TestCase("\\dobra_putanja\\ostv_2021_09_09.csv")]
        public void ValidirajFajlDobarTest(string putanja)
        {
            Assert.AreEqual("", fun.ValidirajPutanju(putanja));
        }
    }
}
