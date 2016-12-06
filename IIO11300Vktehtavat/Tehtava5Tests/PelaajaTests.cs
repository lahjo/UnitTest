using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tehtava5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava5.Tests
{
    [TestClass()]
    public class PelaajaTests
    {
        [TestMethod()]
        public void PelaajaTest()
        {
            Pelaaja pelaaja = new Pelaaja("Teemu", "Selänne", "anaheim ducks", 3000000);
            Assert.IsInstanceOfType(pelaaja.Etunimi, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Sukunimi, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Seura, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Siirtohinta, typeof(int));
        }

        [TestMethod()]
        public void PelaajaTest1()
        {
            Pelaaja pelaaja = new Pelaaja("Teemu", "Selänne", null, 3000000);

            Assert.IsInstanceOfType(pelaaja.Etunimi, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Sukunimi, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Seura, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Siirtohinta, typeof(int));
        }

        [TestMethod()]
        public void AllDataTest()
        {
            Pelaaja pelaaja = new Pelaaja("Teemu", "Selänne", "anaheim ducks", 3000000);
            Pelaaja pelaaja2 = new Pelaaja("Patrik", "Laine", "Winnipeg Jets", 1000000);
            Assert.AreNotSame(pelaaja, pelaaja2);
        }

        [TestMethod()]
        public void GetPositionTest()
        {
            List<string> lista = new List<string>();

            Pelaaja pelaaja = new Pelaaja("Teemu", "Selänne", "anaheim ducks", 3000000);
            lista.Add(pelaaja.AllData());

            pelaaja = new Pelaaja("Patrik", "Laine", "Winnipeg Jets", 1000000);
            lista.Add(pelaaja.AllData());

            Assert.AreEqual(1, pelaaja.GetPosition(lista, lista.Count, "Patrik Laine"));
        }

        [TestMethod()]
        public void ParseDataTest()
        {
            List<string> lista = new List<string>();

            Pelaaja pelaaja = new Pelaaja("Teemu", "Selänne", "anaheimDucks", 3000000);
            lista.Add(pelaaja.AllData());

            pelaaja = new Pelaaja("Patrik", "Laine", "WinnipegJets", 1000000);
            lista.Add(pelaaja.AllData());

            pelaaja.ParseData(lista, 0);

            Assert.IsInstanceOfType(pelaaja.Etunimi, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Sukunimi, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Seura, typeof(string));
            Assert.IsInstanceOfType(pelaaja.Siirtohinta, typeof(int));


            Assert.AreEqual("Teemu", pelaaja.Etunimi);
            Assert.AreEqual("Selänne", pelaaja.Sukunimi);
            Assert.AreEqual("anaheimDucks", pelaaja.Seura);
            Assert.AreEqual(3000000, pelaaja.Siirtohinta);
        }
    }
}