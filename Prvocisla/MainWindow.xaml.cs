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
using System.Text.RegularExpressions;

namespace Prvocisla
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Prvocislo p;
        string[] PoleCisel;
        string Trojky = string.Empty;
        string SjednocenaCisla = string.Empty;

        private void VypsatPrvocisla_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(DolniMez.Text) <= Convert.ToInt32(HorniMez.Text))
            {
                p = new Prvocislo(Convert.ToInt32(HorniMez.Text));
                p.ShowPrime(Convert.ToInt32(DolniMez.Text));
                PrvocislaVypsana.Text = p.xp;
                PoleCisel = p.xp.Split(' ');
                p.xp = "";
                if (CB3.IsChecked == true && CBMany.IsChecked == false)
                {
                    Trojky = VypisCislici3(PoleCisel);
                    PrvocislaVypsana.Text = Trojky;
                }
                else if (CB3.IsChecked == false && CBMany.IsChecked == true)
                {
                    SjednocenaCisla = VypisSjednoceneCislice(PoleCisel);
                    PrvocislaVypsana.Text = SjednocenaCisla;
                }
                else if (CB3.IsChecked == true && CBMany.IsChecked == true)
                {
                    SjednocenaCisla = VypisSjednoceneCislice(PoleCisel);
                    Trojky = VypisCislici3(SjednocenaCisla.Split(' '));
                    PrvocislaVypsana.Text = Trojky;
                }
                Trojky = "";
                SjednocenaCisla = "";
            }
            else MessageBox.Show("Horní mez musí být vyšší než dolní!", "CHYBA");
            DolniMez.Text = "";
            HorniMez.Text = "";
            CB3.IsChecked = false;
            CBMany.IsChecked = false;
        }
        string VypisCislici3(string[] CiselnePole) 
        {
            string Cisla = string.Empty;
            for (int i = 0; i < CiselnePole.Length; i++)
            {
                if (CiselnePole[i].Contains("3")) Cisla += $"{CiselnePole[i]} ";
            }
            return Cisla;
        }
        string VypisSjednoceneCislice(string[] CiselnePole) 
        {
            string Cisla = string.Empty;
            char[] Cislice; //char, ve kterém bude číslo rozděleno na samostatné číslice
            for (int i = 0; i < CiselnePole.Length - 1; i++)
            {
                    Cislice = CiselnePole[i].ToCharArray(); //Zde je provedeno rozložení čísla do charu
                    for (int j = 0; j < Cislice.Length; j++)
                    {
                        if (j != Cislice.Length - 1) //Zjištění, jestli se vejdu do pole
                        {
                            if (Cislice[j] == Cislice[j + 1])
                            {
                                if (j != Cislice.Length - 2) //Zjištění, jestli se vejdu do pole
                                {
                                    if (Cislice[j + 1] == Cislice[j + 2]) Cisla += $"{PoleCisel[i]} ";
                                }
                            }
                        }
                    }
            }
            return Cisla;
        }

        Regex JsouCisla = new Regex("^[0-9]*$");
        private void HorniMez_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !JsouCisla.IsMatch(HorniMez.Text); //Zjišťuje, jestli je vstup číslo (moc nefunkční)
        }
    }
    class Prvocislo
    {
        bool[] je_prv; // Bool pole (rozhodování, zda je číslo prvočíslo)
        public string xp = string.Empty;
        public Prvocislo(int horniMez)
        {
            je_prv = new bool[horniMez + 1]; // Vytvoříme pole s horní mezí
            for (int i = 2; i <= horniMez; i++)
            {
                je_prv[i] = true; // Všechna čísla nastavíme na prvočísla
            }
            for (int i = 2; i <= horniMez; i++)
            {
                if (je_prv[i])
                {
                    for (int j = 2 * i; j <= horniMez; j += i)
                    {
                        je_prv[j] = false; // Vyhodnotíme, zda je číslo skutečně prvočíslo
                    }
                }
            }
        }

        
        public void ShowPrime(int dolniMez)
        {
            if (dolniMez < 0 || dolniMez == 0 || dolniMez == 1) dolniMez = 2;
            for (int i = dolniMez; i < je_prv.Length; i++)
            {
                if (je_prv[i]) xp += $"{i} "; // Uložíme do stringu čísla, která jsou prvočísly (true)
            }
        }
    }
}
