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
           // XDocument xmlDocument = XDocument.Load(@"C:\Users\alessandro.suprani\Desktop\libreria\libri-foschi-suprani\libreria\libri.XML");
            XDocument xmlDoc = XDocument.Parse(File.ReadAllText(@"../../libri.XML", System.Text.Encoding.UTF8), LoadOptions.None);

            IEnumerable<string> names = from libri in XDocument.Load(@"../../libri.XML")
                                        .Elements("Biblioteca").Elements("wiride")
                                        where (string)libri.Element("autore").Element("cognome") == cognome
                                        select libri.Element("codice_scheda").Value;

            foreach (string nomi in names)
              lst_lista.Items.Add(nomi);

             //xmlDoc.Save(@"C:\Users\alessandro.suprani\Desktop\libreria\libri-foschi-suprani\libreria\libri.XML");
        }
    }
}
