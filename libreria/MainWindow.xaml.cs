using System;
using System.Collections.Generic;
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
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;

namespace libreria
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XDocument doc = XDocument.Load(@"../../libri.XML");
        public MainWindow()
        {
            InitializeComponent();
        }

        // RICERCA LIBRO PER AUTORE | QUERY 1
        private void btn_libridi_Click(object sender, RoutedEventArgs e)
        {
            lst_lista.Items.Clear();
            string cognome = txt_autore.Text;
            XDocument xmlDoc = XDocument.Parse(File.ReadAllText(@"../../libri.XML", System.Text.Encoding.UTF8), LoadOptions.None);

            IEnumerable<string> names = from libri in XDocument.Load(@"../../libri.XML")
                                        .Elements("Biblioteca").Elements("wiride")
                                        where (string)libri.Element("autore").Element("cognome") == cognome
                                        select libri.Element("titolo").Value;

            foreach (string nomi in names)
              lst_lista.Items.Add(nomi);

        }

        private void txt_keyword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // COPIE PER TITOLO | QUERY 2
        private void btn_copie_Click(object sender, RoutedEventArgs e)
        {
            lst_lista.Items.Clear();
            int index = 0;
            IEnumerable<string> copie = from libri in XDocument.Load(@"../../libri.XML")
                                                    .Elements("Biblioteca").Elements("wiride")
                                        where (string)libri.Element("titolo") == txt_copie.Text
                                        select libri.Element("titolo").Value;
            foreach (string nomi in copie)              
                    index++;
            lst_lista.Items.Add(index);

        }


        // NUMERO DI COPIE PER GENERE | QUERY 3
        private void btn_genere_Click(object sender, RoutedEventArgs e)
        {
            lst_lista.Items.Clear();

            IEnumerable<string> titles = from libri in XDocument.Load(@"../../libri.XML")
                                        .Elements("Biblioteca").Elements("wiride")
                                         where libri.Element("genere").Value.Contains("romanzo")
                                         select libri.Element("titolo").Value;

            int cont = 0;

            foreach (string titolo in titles)
                cont++;

            lst_lista.Items.Add(cont.ToString());
        }


        // RIMOZIONE TAG ABSTRACT | QUERY 4
        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {

            
         
        }

        private void btn_modifica_Click(object sender, RoutedEventArgs e)
        {
            string titolo = txt_titolo2.Text;
            string testo = txt_inputtesto.Text;
            IEnumerable<XElement> Mod_Gen = from biblioteca in doc.Descendants("wiride")

                                            where biblioteca.Element("titolo").Value == titolo

                                            select biblioteca.Element("genere");
            if (Mod_Gen.OfType<XElement>().First().Value == null)
            {
                doc.Element("Biblioteca")
               .Elements("wiride")
               .Where(x => x.Attribute("titolo").Value == titolo).First()
               .AddBeforeSelf(
               new XElement("genere", testo));
            }
            else
            {
                Mod_Gen.OfType<XElement>().First().Value = testo;
            }

            doc.Save(@"../../libri.xml");

        }
    }
}
