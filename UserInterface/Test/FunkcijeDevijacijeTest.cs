using DeljeniPodaci;
using DeljeniPodaci.Interfaces;
using NUnit.Framework;
using ProračunDevijacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class FunkcijeDevijacijeTest
    {
        FunkcijeProračuna funkcija = new FunkcijeProračuna();
        [Test]
        public void FunkcijaApsoltnaIKvadratnaDevijacijaPrazneListeTest()
        {
            List<Potrosnja> praznaLista = new List<Potrosnja>();
            List<Potrosnja> okejLista = new List<Potrosnja>();

            for (int i = 1; i < 25; i++)
            {
                okejLista.Add(new Potrosnja(DateTime.Now, i, i * 100, "VOJ", "neki fajl", DateTime.Now));
            }

            // obe funkcije testirane praznim listama
            Assert.AreEqual(-2, funkcija.FunkcijaKvadratnaDevijacijaPotrosnje(okejLista, praznaLista));
            Assert.AreEqual(-3, funkcija.FunkcijaKvadratnaDevijacijaPotrosnje(praznaLista, okejLista));
            Assert.AreEqual(-2, funkcija.FunkcijaApsoltnaDevijacijaPotrosnje(okejLista, praznaLista));
            Assert.AreEqual(-3, funkcija.FunkcijaApsoltnaDevijacijaPotrosnje(praznaLista, okejLista));
        }

        [Test]
        public void FunkcijaApsolutneIKvadratneDevijacijeSaNullArgumentimaTest()
        {
            List<Potrosnja> okejLista = new List<Potrosnja>();

            for (int i = 1; i < 25; i++)
            {
                okejLista.Add(new Potrosnja(DateTime.Now, i, i * 100, "VOJ", "neki fajl", DateTime.Now));
            }

            // obe funkcije testirane null listama 
            Assert.Throws<ArgumentNullException>(() => funkcija.FunkcijaKvadratnaDevijacijaPotrosnje(null, okejLista));
            Assert.Throws<ArgumentNullException>(() => funkcija.FunkcijaKvadratnaDevijacijaPotrosnje(okejLista, null));
            Assert.Throws<ArgumentNullException>(() => funkcija.FunkcijaApsoltnaDevijacijaPotrosnje(null, okejLista));
            Assert.Throws<ArgumentNullException>(() => funkcija.FunkcijaApsoltnaDevijacijaPotrosnje(okejLista, null));
        }


        [Test]
        public void FunkcijaApsoltnaIKvadratnaDevijacijaDobriParametriTest()
        {
            List<Potrosnja> dobraLista1 = new List<Potrosnja>();
            List<Potrosnja> dobraLista2 = new List<Potrosnja>();


            for (int i = 1; i < 25; i++)
            {
                dobraLista1.Add(new Potrosnja(new DateTime(2021, 5, 3), i, i * 100, "BGD", "neki_fajl", DateTime.Now));
                dobraLista2.Add(new Potrosnja(new DateTime(2021, 5, 3), i, i * 105, "BGD", "neki_fajl", DateTime.Now));
            }

            for (int i = 1; i < 25; i++)
            {
                dobraLista1.Add(new Potrosnja(new DateTime(2021, 5, 4), i, i * 101, "BGD", "neki_fajl", DateTime.Now.AddDays(1)));
                dobraLista2.Add(new Potrosnja(new DateTime(2021, 5, 4), i, i * 105, "BGD", "neki_fajl", DateTime.Now.AddDays(1)));
            }

            // dobri testovi
            Assert.AreEqual(4.48020, Math.Round(funkcija.FunkcijaApsoltnaDevijacijaPotrosnje(dobraLista1, dobraLista2)), 5);
            Assert.AreEqual(31.24794, Math.Round(funkcija.FunkcijaKvadratnaDevijacijaPotrosnje(dobraLista1, dobraLista2)), 5);
        }

        [Test]
        public void FunkcijaApsolutneIKvadratneDevijacijeNepodudarajuceListeTest()
        {
            List<Potrosnja> losaLista1 = new List<Potrosnja>();
            List<Potrosnja> losaLista2 = new List<Potrosnja>();

            for (int i = 1; i < 25; i++)
                losaLista1.Add(new Potrosnja(DateTime.Now, i, i * 100, "BGD", "neki_fajl", DateTime.Now));

            for (int i = 1; i < 25; i++)
                losaLista2.Add(new Potrosnja(DateTime.Now.AddDays(1), i, i * 100, "BGD", "neki_fajl", DateTime.Now));

            Assert.AreEqual(-4, funkcija.FunkcijaApsoltnaDevijacijaPotrosnje(losaLista1, losaLista2));
            Assert.AreEqual(-4, funkcija.FunkcijaKvadratnaDevijacijaPotrosnje(losaLista1, losaLista2));
        }
    }
}
