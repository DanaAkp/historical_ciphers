using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace KMZI_laba1
{
    public partial class MainWindow : Window
    {
        string AlphabetRUS = "абв где жзи йкл мно прс туф хцч шщъ ьыэ юя";
        string AlphabetENG = "abcdefghijklmnopqrstuvwxyz";
        string s;
        int TestKey = 0;
        OpenFileDialog FileOT = new OpenFileDialog();
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Шифр Цезаря
        private void btnOpenText_Click(object sender, RoutedEventArgs e)
        {
            FileOT.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (FileOT.ShowDialog() == true)
            {
                Stream ms = new FileStream(FileOT.FileName, FileMode.Open);
                byte[] array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
                string buf = Encoding.Unicode.GetString(array);
                s = buf.ToLower();
            }
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            int Key = int.Parse(tbKeyEncrypt.Text);
            string Cipher = "";
            if (rbENG.IsChecked == true)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolENG(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetENG[(ind + Key) % 31];
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetRUS[(ind + Key) % 31];
                }
            }
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (sd.ShowDialog() == true)
            {
                File.WriteAllText(sd.FileName, Cipher);
            }
        }
        public int GetSymbolRUS(char sym, int Ind)
        {
            if(AlphabetRUS.IndexOf(sym)==-1) return Ind;
            return AlphabetRUS.IndexOf(sym);
        }
        public int GetSymbolENG(char sym, int Ind)
        {
            if (AlphabetENG.IndexOf(sym) == -1) return Ind;
            return AlphabetENG.IndexOf(sym);
        }
        #endregion

        #region Шифр Виженера

        private void btnOpenNewWIn_Click(object sender, RoutedEventArgs e)
        {
            CephirVizhenera win = new CephirVizhenera();
            win.Show();
        }

        private void btnOpenNewWinPere_Click(object sender, RoutedEventArgs e)
        {
            PereWin win = new PereWin();
            win.Show();
        }
        #endregion

        #region Раскрытие шифра Цезаря

        private void btnCipherText_Click(object sender, RoutedEventArgs e)
        {
            FileOT.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (FileOT.ShowDialog() == true)
            {
                Stream ms = new FileStream(FileOT.FileName, FileMode.Open);
                byte[] array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
                string buf = Encoding.UTF8.GetString(array);
                s = buf.ToLower();
            }
          //  tblDecipher.Text = s;
        }
        private void btnDecipher_Click(object sender, RoutedEventArgs e)
        {
            int Key = int.Parse(tbKey.Text);
            string OT = "";
            if (rbENG.IsChecked == true)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolENG(s[i], i);
                    if (ind == i)
                        OT += s[i];
                    else OT += AlphabetENG[(Math.Abs(ind - Key)) % 26];
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind == i)
                        OT += s[i];
                    else OT += AlphabetRUS[(Math.Abs(ind - Key)) % 31];
                }
            }
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (sd.ShowDialog() == true)
            {
                File.WriteAllText(sd.FileName, OT);
            }
        }
        //without Key
        private void btnOpenCipherTxt_Click(object sender, RoutedEventArgs e)
        {
            FileOT.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (FileOT.ShowDialog() == true)
            {
                Stream ms = new FileStream(FileOT.FileName, FileMode.Open);
                byte[] array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
                string buf = Encoding.UTF8.GetString(array);
                s = buf.ToLower();
                tbCipherText.Text = s;
            }
        }
        private void btnDecipherWithoutKey_Click(object sender, RoutedEventArgs e)
        {
            TestKey++;
            TestKey %= 31;
            string OT = "";
            if (rbENGwithoutKey.IsChecked == true)
            {
                for (int i = 0; i < 30; i++)
                {
                    int ind = GetSymbolENG(s[i], i);
                    if (ind == i)
                        OT += s[i];
                    else OT += AlphabetENG[(Math.Abs(ind - TestKey)) % 26];
                }
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind == i)
                        OT += s[i];
                    else OT += AlphabetRUS[(Math.Abs(ind - TestKey)) % 31];
                }
            }
            tblDecipher.Text = OT;
        }

        private void btnDecipherAlltext_Click(object sender, RoutedEventArgs e)
        {
            string OT = "";
            if (rbENGwithoutKey.IsChecked == true)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolENG(s[i], i);
                    if (ind == i)
                        OT += s[i];
                    else OT += AlphabetENG[(Math.Abs(ind - TestKey)) % 26];
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind == i)
                        OT += s[i];
                    else OT += AlphabetRUS[(Math.Abs(ind - TestKey)) % 31];
                }
            }
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (sd.ShowDialog() == true)
            {
                File.WriteAllText(sd.FileName, OT);
            }
        }
        #endregion
    }
}
