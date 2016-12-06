using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava5
{
    public class Pelaaja
    {
        private string etunimi, sukunimi, seura;
        private int siirtohinta;

        public string Etunimi
        {
            get { return etunimi; }
            set { etunimi = value; }
        }

        public string Sukunimi
        {
            get { return sukunimi; }
            set { sukunimi = value; }
        }

        public string Seura
        {
            get { return seura; }
            set { seura = value; }
        }

        public int Siirtohinta
        {
            get { return siirtohinta; }
            set { siirtohinta = value; }
        }

        public Pelaaja(string etunimi, string sukunimi, string seura, int siirtohinta)
        {
            this.etunimi = etunimi;
            this.sukunimi = sukunimi;
            this.seura = seura;
            this.siirtohinta = siirtohinta;
        }

        public Pelaaja()
        {}

        public string Kokonimi
        {
            get { return etunimi + " " + sukunimi; }
        }

        public string Esitysnimi
        {
            get { return Kokonimi + "," + seura; }
        }

        public string AllData()
        {
            return Esitysnimi + ";" + siirtohinta.ToString();
        }

        public int GetPosition(List<String> lista, int listSize, string pelaaja)
        {
            int position = -1;
            for (int i = 0; i < listSize; i++)
            {
                if (lista[i].Contains(pelaaja))
                {
                    position = i;
                }
            }

            return position;
        }

        public void ParseData(List<String> lista, int playerPosition)
        {
            string stringfromlist = lista[playerPosition].ToString();
            string[] data = stringfromlist.Split(' ', ',', ';');

            Etunimi = data[0];
            Sukunimi = data[1];
            seura = data[2];
            siirtohinta = int.Parse(data[3]);
        }

        public void ParseDataFile(string pelaaja)
        {
            string[] data = pelaaja.Split(' ', ',', ';');

            Etunimi = data[0];
            Sukunimi = data[1];
            seura = data[2];
            siirtohinta = int.Parse(data[3]);
        }

        public List<String> listboxList(List<String> pelaajat, int listSize, List<String> pelaajatnayttolista)
        {
            string[] data;
            for (int i = 0; i < listSize; i++)
            {
                string stringfromlist = pelaajat[i].ToString();
                data = stringfromlist.Split(';');
                pelaajatnayttolista.Add(data[0]);
            }

            return pelaajatnayttolista;
        }
    }
}

