using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNev_Click(object sender, RoutedEventArgs e)
        {
            string tbxSzoveg = tbxKereso.Text.Trim();
            if (tbxSzoveg.Length < 1)
            {
                dgAdatok.ItemsSource = legoElemek;
                return;
            }
            ObservableCollection<LegoElem> masolat = new ObservableCollection<LegoElem>();
            foreach (LegoElem legoElem in legoElemek)
            {
                if (legoElem.Nev.Substring(0, tbxSzoveg.Length).ToLower() == tbxSzoveg.ToLower())
                {
                    masolat.Add(legoElem);
                }
            }
            dgAdatok.ItemsSource = masolat;
        }

        private void btnId_Click(object sender, RoutedEventArgs e)
        {
            string tbxSzoveg = tbxKereso.Text.Trim();
            if (tbxSzoveg.Length < 1)
            {
                dgAdatok.ItemsSource = legoElemek;
                return;
            }
            ObservableCollection<LegoElem> masolat = new ObservableCollection<LegoElem>();
            foreach (LegoElem legoElem in legoElemek)
            {
                if (legoElem.Id.Substring(0, tbxSzoveg.Length).ToLower() == tbxSzoveg.ToLower())
                {
                    masolat.Add(legoElem);
                }
            }
            dgAdatok.ItemsSource = masolat;
        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            dgAdatok.ItemsSource = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "BSX fájlok (*.bsx)|*.bsx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != true)
            {
                return;
            }
            XDocument xaml = XDocument.Load(ofd.FileName);
            legoElemek.Clear();
            foreach (XElement item in xaml.Descendants("Item"))
            {
                legoElemek.Add(new LegoElem(
                    item.Element("ItemID").Value, 
                    item.Element("ItemName").Value, 
                    item.Element("CategoryName").Value,
                    item.Element("ColorName").Value,
                    Convert.ToUInt32(item.Element("Qty").Value)
                    ));
            }
            dgAdatok.ItemsSource = legoElemek;
        }
    }
}