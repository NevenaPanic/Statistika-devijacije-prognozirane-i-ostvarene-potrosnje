using DeljeniPodaci;
using Microsoft.Win32;
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
using ValidatorPodataka;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog ofOcekivane = new OpenFileDialog();
        OpenFileDialog ofOstvarene = new OpenFileDialog();
        ValidatorFajla vf = new ValidatorFajla();
        List<Potrosnja> procitanoOcekivano = new List<Potrosnja>();
        List<Potrosnja> procitanoOstvareno = new List<Potrosnja>();
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ocitaj_fajl_ocekivane_Click(object sender, RoutedEventArgs e)
        {

            lb_ocitan_fajl_ocekivane.Foreground = Brushes.Red;
            lb_ocitan_fajl_ocekivane.BorderBrush = Brushes.Red;
            lb_ocitan_fajl_ocekivane.BorderThickness = new Thickness(1);

            if (ofOcekivane.ShowDialog() == true)
            {
                string putanjaFajla = ofOcekivane.FileName;
                

                if (vf.ValidatorTipaFajla(putanjaFajla) == false || vf.ValidatorImenaFajla(ofOcekivane.SafeFileName) == false)
                {
                    // podesavanje izgleda interface-a
                    if (vf.ValidatorTipaFajla(putanjaFajla) == false)
                    {
                        lb_ocitan_fajl_ocekivane.Content = "Pogresan tip fajla!";
                        MessageBox.Show("Pogresan tip fajla! Molimo Vas unesite novi \".csv\" fajl!", "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        lb_ocitan_fajl_ocekivane.Content = "Pogresno ime fajla!";
                        MessageBox.Show("Pogresan format imena fajla! Molimo Vas unesite novi fajl u formatu ostv_YYYY_MM_DD ili prog_YYYY_MM_DD!", "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    procitanoOcekivano = vf.ProcitajFajl(putanjaFajla);
                    if (vf.ValidacijaPodatakaUFajlu(procitanoOcekivano) == false)
                    {
                        // pokriveni su slucaji pogesnog broja sati u danu i pogresnog broja podataka za jednu oblast
                        MessageBox.Show("Podaci koji se nalaze unutar fajla koji ste odabrali su nevalidni!", "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                        lb_ocitan_fajl_ocekivane.Content = "Nevalidan fajl!";
                    }
                    else
                    {
                        // podesavanje izgleda interface-a
                        lb_ocitan_fajl_ocekivane.Content = ofOcekivane.SafeFileName; // prvi "_" se ne ispisuje zbog labele
                        lb_ocitan_fajl_ocekivane.Foreground = Brushes.DarkGreen;
                        lb_ocitan_fajl_ocekivane.BorderBrush = Brushes.DarkGreen;
                    }
                }
            }
            else if (ofOcekivane.ShowDialog() == false)
            {
                // podesavanje izgleda interface-a
                lb_ocitan_fajl_ocekivane.Content = "Niste odabrali fajl!";
            }               
        }

        private void btn_ocitaj_fajl_ostvarene_Click(object sender, RoutedEventArgs e)
        {
            lb_ocitan_fajl_ostvarene.Foreground = Brushes.Red;
            lb_ocitan_fajl_ostvarene.BorderBrush = Brushes.Red;
            lb_ocitan_fajl_ostvarene.BorderThickness = new Thickness(1);

            if (ofOstvarene.ShowDialog() == true)
            {
                string putanjaFajlaOstvareno = ofOstvarene.FileName;


                if (vf.ValidatorTipaFajla(putanjaFajlaOstvareno) == false || vf.ValidatorImenaFajla(ofOstvarene.SafeFileName) == false)
                {
                    // podesavanje izgleda interface-a
                    if (vf.ValidatorTipaFajla(putanjaFajlaOstvareno) == false)
                    {
                        lb_ocitan_fajl_ostvarene.Content = "Pogresan tip fajla!";
                        MessageBox.Show("Pogresan tip fajla! Molimo Vas unesite novi \".csv\" fajl!", "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        lb_ocitan_fajl_ostvarene.Content = "Pogresno ime fajla!";
                        MessageBox.Show("Pogresan format imena fajla! Molimo Vas unesite novi fajl u formatu ostv_YYYY_MM_DD ili prog_YYYY_MM_DD!", "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    procitanoOstvareno = vf.ProcitajFajl(putanjaFajlaOstvareno);
                    if (vf.ValidacijaPodatakaUFajlu(procitanoOstvareno) == false)
                    {
                        // pokriveni su slucaji pogesnog broja sati u danu i pogresnog broja podataka za jednu oblast
                        MessageBox.Show("Podaci koji se nalaze unutar fajla koji ste odabrali su nevalidni!", "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                        lb_ocitan_fajl_ostvarene.Content = "Nevalidan fajl!";
                    }
                    else
                    {
                        // podesavanje izgleda interface-a
                        lb_ocitan_fajl_ostvarene.Content = ofOstvarene.SafeFileName; // prvi "_" se ne ispisuje zbog labele
                        lb_ocitan_fajl_ostvarene.Foreground = Brushes.DarkGreen;
                        lb_ocitan_fajl_ostvarene.BorderBrush = Brushes.DarkGreen;
                    }
                }
            }
            else if (ofOstvarene.ShowDialog() == false)
            {
                // podesavanje izgleda interface-a
                lb_ocitan_fajl_ostvarene.Content = "Niste odabrali fajl!";
            }
        }
    
    }
}
