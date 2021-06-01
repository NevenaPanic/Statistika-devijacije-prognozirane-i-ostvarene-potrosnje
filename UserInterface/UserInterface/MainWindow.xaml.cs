using DeljeniPodaci;
using Microsoft.Win32;
using ObradaPodataka;
using PristupBaziPodataka.DAO.DAOImpl;
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
        PotrosnjaDAO p = new PotrosnjaDAO();
        KontrolerPodacima kontoler = new KontrolerPodacima() { };

        List<Potrosnja> procitanoOcekivano = new List<Potrosnja>();
        List<Potrosnja> procitanoOstvareno = new List<Potrosnja>();
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

                string validan = vf.ValidirajPutanju(putanjaFajla);
                if (!validan.Equals(String.Empty))
                {
                        lb_ocitan_fajl_ocekivane.Content = "Nevalidan fajl!";
                        MessageBox.Show(validan, "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    procitanoOcekivano = kontoler.ProcitajFajl(putanjaFajla);
                    validan = vf.ValidirajPodatke(procitanoOcekivano);
                    if (!validan.Equals(String.Empty))
                    {
                        MessageBox.Show(validan, "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                        lb_ocitan_fajl_ocekivane.Content = "Nevalidan fajl!";
                    }
                    else
                    {
                        lb_ocitan_fajl_ocekivane.Content = ofOcekivane.SafeFileName; // prvi "_" se ne ispisuje zbog labele
                        lb_ocitan_fajl_ocekivane.Foreground = Brushes.DarkGreen;
                        lb_ocitan_fajl_ocekivane.BorderBrush = Brushes.DarkGreen;
                    }
                }
            }
            else if (ofOcekivane.ShowDialog() == false)
            {
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

                string validno = vf.ValidirajPutanju(putanjaFajlaOstvareno);

                if (!validno.Equals(String.Empty))
                {
                    lb_ocitan_fajl_ostvarene.Content = "Nevalidan fajl!";
                    MessageBox.Show(validno, "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    procitanoOstvareno = kontoler.ProcitajFajl(putanjaFajlaOstvareno);
                    validno = vf.ValidirajPodatke(procitanoOstvareno);
                    if (!validno.Equals(String.Empty))
                    {
                        MessageBox.Show(validno, "Nevalidan fajl!", MessageBoxButton.OK, MessageBoxImage.Error);
                        lb_ocitan_fajl_ostvarene.Content = "Nevalidan fajl!";
                    }
                    else
                    {
                        lb_ocitan_fajl_ostvarene.Content = ofOstvarene.SafeFileName; // prvi "_" se ne ispisuje zbog labele
                        lb_ocitan_fajl_ostvarene.Foreground = Brushes.DarkGreen;
                        lb_ocitan_fajl_ostvarene.BorderBrush = Brushes.DarkGreen;
                    }
                }
            }
            else if (ofOstvarene.ShowDialog() == false)
            {
                lb_ocitan_fajl_ostvarene.Content = "Niste odabrali fajl!";
            }
        }

        private void btn_ucitaj_Click(object sender, RoutedEventArgs e)
        {
            if (!lb_ocitan_fajl_ocekivane.Content.Equals("Nevalidan fajl!") && !lb_ocitan_fajl_ostvarene.Content.Equals("Nevalidan fajl!")
                && !lb_ocitan_fajl_ocekivane.Content.Equals("Niste odabrali fajl!") && !lb_ocitan_fajl_ostvarene.Content.Equals("Niste odabrali fajl!"))
            {
                kontoler.UpisFajlovaUBazu(procitanoOcekivano, procitanoOstvareno);
                MessageBox.Show("Zavrsen upis u bazu", "Baza upis!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nemoguce je izvrsiti upis u bazu sa nevalidnim fajlovima!", "Neuspeli upis u bazu!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btn_racunaj_Click(object sender, RoutedEventArgs e)
        {
            DateTime pocetak = dp_pocetak.SelectedDate.GetValueOrDefault();
            DateTime kraj = dp_kraj.SelectedDate.GetValueOrDefault();
            string oblast = tb_uneto_podrucje.Text;

            if (pocetak != DateTime.MinValue && kraj != DateTime.MinValue && pocetak <= kraj && !oblast.Equals(String.Empty))
            {
                lb_apsolutna.Content = kontoler.ApsoltnaDevijacijaPotrosnje(pocetak, kraj, oblast);
                lb_kvadratna.Content = kontoler.KvadratnaDevijacijaPotrosnje(pocetak, kraj, oblast);
            }

        }
    }
}
