using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Tehtava5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> kaikkipelaajat = new List<String>();
        private List<String> pelaajatnayttolista = new List<String>();
        private Pelaaja pelaaja = new Pelaaja();

        public MainWindow()
        {
            InitializeComponent();
            elementsInitialize();
        }

        private void elementsInitialize()
        {
            comboBox.Items.Add("Blues");
            comboBox.Items.Add("HIFK");
            comboBox.Items.Add("HPK");
            comboBox.Items.Add("Ilves");
            comboBox.Items.Add("JYP");
            comboBox.Items.Add("KalPa");
            comboBox.Items.Add("KooKoo");
            comboBox.Items.Add("Kärpät");
            comboBox.Items.Add("Lukko");
            comboBox.Items.Add("Pelicans");
            comboBox.Items.Add("SaiPa");
            comboBox.Items.Add("Sport");
            comboBox.Items.Add("Tappara");
            comboBox.Items.Add("TPS");
            comboBox.Items.Add("Ässät");
        }

        private void btnUusiPelaaja_Click(object sender, RoutedEventArgs e)
        {
            CleanText();
            txtbStatus.Text = "Uusi Pelaaja";
        }

        private void UpdateInfo()
        {
            pelaajatnayttolista.Clear();
            pelaaja.listboxList(kaikkipelaajat, kaikkipelaajat.Count, pelaajatnayttolista);

            listBoxPelaajat.ItemsSource = null;
            listBoxPelaajat.ItemsSource = pelaajatnayttolista;
        }

        private void CleanText()
        {
            txtEtunimi.Text = "";
            txtSukunimi.Text = "";
            txtSiirtohinta.Text = "";
            comboBox.SelectedIndex = 0;
        }

        private void btnTalletaPelaaja_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pelaaja = new Pelaaja(txtEtunimi.Text, txtSukunimi.Text, comboBox.Text, int.Parse(txtSiirtohinta.Text));
                if (!kaikkipelaajat.Any(str => str.Contains(pelaaja.Kokonimi)))
                {
                    kaikkipelaajat.Add(pelaaja.AllData());
                    UpdateInfo();

                }
                else
                {
                    kaikkipelaajat.RemoveAt(pelaaja.GetPosition(kaikkipelaajat, kaikkipelaajat.Count, pelaaja.Kokonimi));
                    kaikkipelaajat.Add(pelaaja.AllData());
                    UpdateInfo();
                }

                txtbStatus.Text = "Pelaaja Tallennettu";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxPelaajat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            pelaaja.ParseData(kaikkipelaajat, pelaaja.GetPosition(kaikkipelaajat, kaikkipelaajat.Count, listBoxPelaajat.SelectedItem.ToString()));

            CleanText();

            txtEtunimi.Text = pelaaja.Etunimi;
            txtSukunimi.Text = pelaaja.Sukunimi;
            txtSiirtohinta.Text = pelaaja.Siirtohinta.ToString();
            comboBox.SelectedIndex = comboBox.Items.IndexOf(pelaaja.Seura);
        }

        private void btnPoistaPelaaja_Click(object sender, RoutedEventArgs e)
        {
            kaikkipelaajat.RemoveAt(pelaaja.GetPosition(kaikkipelaajat, kaikkipelaajat.Count, listBoxPelaajat.SelectedItem.ToString()));
            UpdateInfo();
            txtbStatus.Text = "Pelaaja poistettu listasta";
        }

        private void btnKirjoitaPelaajat_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "Liiga-Pelaajat";
            save.DefaultExt = ".txt";
            save.Filter = "Text documents (.txt)|*.txt|XML (.xml)|*.xml";

            Nullable<bool> result = save.ShowDialog();

            if (result == true)
            {
                var extension = System.IO.Path.GetExtension(save.FileName);

                if (extension.ToLower() == ".txt") {
                    File.WriteAllLines(save.FileName, kaikkipelaajat.ToArray());
                } else if (extension.ToLower() == ".xml")
                {

                    if (File.Exists(save.FileName))
                    {
                        File.Delete(save.FileName);
                    }

                    XmlWriterSettings xmldata = new XmlWriterSettings();
                    xmldata.Indent = true;

                    xmldata.OmitXmlDeclaration = true;
                    StringBuilder sb = new StringBuilder();

                    XmlWriter writer = XmlWriter.Create(sb, xmldata);

                    writer.WriteStartDocument();
                    writer.WriteStartElement("pelaajat");

                    for (int i = 0; i < kaikkipelaajat.Count; i++)
                    {
                        pelaaja.ParseData(kaikkipelaajat, i);

                        writer.WriteStartElement("pelaaja");
                        writer.WriteElementString("etunimi", pelaaja.Etunimi);
                        writer.WriteElementString("sukunimi", pelaaja.Sukunimi);
                        writer.WriteElementString("seura", pelaaja.Seura);
                        writer.WriteElementString("hinta", pelaaja.Siirtohinta.ToString());
                        writer.WriteEndElement();
                        
                        }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                    writer.Close();

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(save.FileName, true))
                    {
                        file.Write(sb.ToString());
                    }
                }

                
            }

            txtbStatus.Text = "Tiedosto tallennettu";
        }

        private void btnluePelaaja_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open File";
            open.Filter = "Text documents (.txt)|*.txt|XML (.xml)|*.xml";

            Nullable<bool> result = open.ShowDialog();
            if (result == true)
            {
                var extension = System.IO.Path.GetExtension(open.FileName);

                if (extension.ToLower() == ".txt") { 
                try
                {
                    string line;

                    System.IO.StreamReader file = new System.IO.StreamReader(open.OpenFile());
                    while ((line = file.ReadLine()) != null)
                    {
                        pelaaja.ParseDataFile(line);

                        if (!kaikkipelaajat.Any(str => str.Contains(pelaaja.Kokonimi)))
                        {
                            kaikkipelaajat.Add(pelaaja.AllData());
                        }
                        else { }
                    }

                    file.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }else if (extension.ToLower() == ".xml") {

                    XmlDocument doc = new XmlDocument();
                    doc.Load(open.FileName);

                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    {
                        foreach (XmlNode locNode in node)
                        {
                            if (locNode.Name == "etunimi")
                            {
                                pelaaja.Etunimi = locNode.InnerText;
                            }
                            else if (locNode.Name == "sukunimi") {
                                pelaaja.Sukunimi = locNode.InnerText;
                            }
                            else if (locNode.Name == "seura")
                            {
                                pelaaja.Seura = locNode.InnerText;
                            }
                            else if (locNode.Name == "hinta")
                            {
                                pelaaja.Siirtohinta = int.Parse(locNode.InnerText);
                            }
                        }
                        if (!kaikkipelaajat.Any(str => str.Contains(pelaaja.Kokonimi)))
                        {
                            kaikkipelaajat.Add(pelaaja.AllData());
                        }
                    }
                }
                
            }
            pelaaja.listboxList(kaikkipelaajat, kaikkipelaajat.Count, pelaajatnayttolista);
            listBoxPelaajat.ItemsSource = pelaajatnayttolista;
            UpdateInfo();
        }
    }
}
