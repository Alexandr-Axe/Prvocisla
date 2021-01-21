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
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                p = new Prvocislo(Convert.ToInt32(HorniMez.Text));
                p.zobrazit();
                PrvocislaVypsana.Text = p.xp;
                p.xp = "";
            }
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
        public Prvocislo(int MAX)
        {
            je_prv = new bool[MAX + 1]; // Vytvoříme pole s horní mezí
            for (int i = 2; i <= MAX; i++)
            {
                je_prv[i] = true; // Všechna čísla nastavíme na prvočísla
            }
            for (int i = 2; i <= MAX; i++)
            {
                if (je_prv[i])
                {
                    for (int j = 2 * i; j <= MAX; j += i)
                    {
                        je_prv[j] = false; // Vyhodnotíme, zda je číslo skutečně prvočíslo
                    }
                }
            }
        }

        
        public void zobrazit()
        {
            for (int i = 0; i < je_prv.Length; i++)
            {
                if (je_prv[i]) xp += $" {i}"; // Uložíme do stringu čísla, která jsou prvočísly (true)
            }
        }
    }
}
