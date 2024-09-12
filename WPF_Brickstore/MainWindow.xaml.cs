using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_Brickstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public ObservableCollection<LegoElem> legoElemek = new ObservableCollection<LegoElem>();
        static public List<string> kategoriaElemek = new List<string>();
        static public Dictionary<string, string> fajlLista = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != true)
            {
                return;
            }

        }
        private void Kereso()
        {
            ObservableCollection<LegoElem> masolat = new ObservableCollection<LegoElem>();
            foreach (LegoElem legoElem in legoElemek)
            {
                bool megfelel = true;
                if (tbxNevKereso.Text != null && tbxNevKereso.Text.Trim().Length > 0)
                {
                    if (legoElem.Nev.Substring(0, tbxNevKereso.Text.Trim().Length).ToLower() != tbxNevKereso.Text.Trim().ToLower())
                    {
                        megfelel = false;
                    }
                }
                if (tbxIdKereso.Text != null && tbxIdKereso.Text.Trim().Length > 0)
                {
                    if (legoElem.Id.Substring(0, tbxIdKereso.Text.Trim().Length).ToLower() != tbxIdKereso.Text.Trim().ToLower())
                    {
                        megfelel = false;
                    }
                }
                if (cboKategoriaKereso.SelectedItem.ToString() != "Nincs kiválasztva")
                {
                    if (legoElem.Kategoria != cboKategoriaKereso.SelectedItem.ToString())
                    {
                        megfelel = false;
                    }
                }
                if (megfelel)
                {
                    masolat.Add(legoElem);
                }
            }
            dgAdatok.ItemsSource = null;
            dgAdatok.Items.Clear();
            dgAdatok.ItemsSource = masolat;
        }
        private void tbxNevKereso_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kereso();
        }

        private void tbxIdKereso_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kereso();
        }

        private void cboKategoriaKereso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kereso();
        }

        private void lbxFajlok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgAdatok.ItemsSource = null;
            if (lbxFajlok.SelectedItem != null)
            {
                XDocument xaml = XDocument.Load(fajlLista[lbxFajlok.SelectedItem.ToString()]);
                legoElemek.Clear();
                foreach (XElement item in xaml.Descendants("Item"))
                {
                    LegoElem legoElem = new LegoElem(
                        item.Element("ItemID").Value,
                        item.Element("ItemName").Value,
                        item.Element("CategoryName").Value,
                        item.Element("ColorName").Value,
                        Convert.ToUInt32(item.Element("Qty").Value)
                        );
                    legoElemek.Add(legoElem);
                    if (!kategoriaElemek.Contains(legoElem.Kategoria))
                    {
                        kategoriaElemek.Add(legoElem.Kategoria);
                    }
                }
                kategoriaElemek.Sort();
                kategoriaElemek.Insert(0, "Nincs kiválasztva");
                cboKategoriaKereso.ItemsSource = kategoriaElemek;
                cboKategoriaKereso.SelectedIndex = 0;
                dgAdatok.ItemsSource = legoElemek;
                Kereso();
            }
        }
    }
}
