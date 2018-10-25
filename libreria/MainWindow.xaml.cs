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

        private void btn_genere_Click(object sender, RoutedEventArgs e)
        {
            lst_lista.Items.Clear();

            IEnumerable<string> titles = from libri in XDocument.Load(@"../../libri.XML")
                                        .Elements("Biblioteca").Elements("wiride")
                                         where (string)libri.Element("genere") == "romanzo"
                                         select libri.Element("titolo").Value;

            int cont = 0;

            foreach (string titolo in titles)
                cont++;

            lst_lista.Items.Add(cont.ToString());
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            XElement elment = (from xml1 in doc.Descendants("abstract")
                               select xml1).FirstOrDefault();
            elment.Remove();
            doc.Save(@"../../libri.XML");
        }
    }
}
