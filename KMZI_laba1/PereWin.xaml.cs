using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KMZI_laba1
{
    /// <summary>
    /// Логика взаимодействия для PereWin.xaml
    /// </summary>
    public partial class PereWin : Window
    {
        string AlphabetRUS = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string AlphabetENG = "abcdefghijklmnopqrstuvwxyz";
        int[] P;
        string s;
        string Word;
        OpenFileDialog FileOT = new OpenFileDialog();
        Random rand = new Random();
        public PereWin()
        {
            InitializeComponent();
            tblStatistic.Text = "а=0,0801\nб=0,0159\nв=0,0454\nг=0,0070\nд=0,0298\nе=0,0845\nж=0,0094\nз=0,0165\nи=0,0735\nй=0,0121\nк=0,0349\nл=0,0440\nм=0,0321\nн=0,0670\nо=0,1097\nп=0,0281\nр=0,0473\nс=0,0547\nт=0,0626\nу=0,0262\nф=0,0026\nх=0,0097\nц=0,0048\nч=0,0144\nш=0,0073\nщ=0,0036\nъ=0,0004\nы=0,0190\nь=0,0174\nэ=0,0032\nю=0,0064\nя=0,0201";
        }
        #region Зашифровать
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
                tbOpenText.Text = s;
            }
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string Cipher = "";
            tbTransposition.Text = "";
            Word = "";
            List<int> mas = new List<int>();
            if (rbEng.IsChecked == true)
            {
                int n = 26;
                P = new int[n];
                for (int i = 0; i < n; i++)
                {
                    mas.Add(i);
                }
                for (int i = 0; i < n; i++)
                {
                    int ind;
                    if (mas.Count > 1)
                    {
                        ind = (int)(rand.NextDouble() * mas.Count);
                        P[i] = mas[ind];
                        mas.RemoveAt(ind);
                    }
                }
                P[n - 1] = mas[0];
                for (int i = 0; i < P.Length; i++)
                {
                    Word += AlphabetENG[P[i]];
                }
                tbTransposition.Text = Word;
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolENG(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += Word[ind];
                }
            }
            else
            {
                int n = 32;
                P = new int[n];
                for (int i = 0; i < n; i++)
                {
                    mas.Add(i);
                }
                for (int i = 0; i < n; i++)
                {
                    int ind;
                    if (mas.Count > 1)
                    {
                        ind = (int)(rand.NextDouble() * mas.Count);
                        P[i] = mas[ind];
                        mas.RemoveAt(ind);
                    }
                }
                P[n - 1] = mas[0];
                for (int i = 0; i < P.Length; i++)
                {
                    Word += AlphabetRUS[P[i]];
                }
                tbTransposition.Text = Word;
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += Word[ind];
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
            if (AlphabetRUS.IndexOf(sym) == -1) return Ind;
            return AlphabetRUS.IndexOf(sym);
        }
        public int GetSymbolENG(char sym, int Ind)
        {
            if (AlphabetENG.IndexOf(sym) == -1) return Ind;
            return AlphabetENG.IndexOf(sym);
        }

        #endregion
        #region Расшифровать с ключом
        private void btnDEcrypt_Click(object sender, RoutedEventArgs e)
        {
            string Cipher = "";
            Word = tbTranspositionKEY.Text;
            if (rbEng.IsChecked == true)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolTranspos(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetENG[ind];
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolTranspos(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetRUS[ind];
                }
            }

            tblDECipherText.Text = Cipher;
            //SaveFileDialog sd = new SaveFileDialog();
            //sd.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            //if (sd.ShowDialog() == true)
            //{
            //    File.WriteAllText(sd.FileName, Cipher);
            //}

        }

        public int GetSymbolTranspos(char sym, int Ind)
        {
            if (Word.IndexOf(sym) == -1) return Ind;
            return Word.IndexOf(sym);
        }
        private void btnCIPHERText_Click(object sender, RoutedEventArgs e)
        {
            FileOT.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (FileOT.ShowDialog() == true)
            {
                Stream ms = new FileStream(FileOT.FileName, FileMode.Open);
                byte[] array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
                string buf = Encoding.UTF8.GetString(array);
                s = buf.ToLower();
                tbOpenCIPHERText.Text = s;
            }
        }
        #endregion
        #region Расшифровать без ключа
        private void btnOPenCipherText_Click(object sender, RoutedEventArgs e)
        {
            FileOT.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (FileOT.ShowDialog() == true)
            {
                Stream ms = new FileStream(FileOT.FileName, FileMode.Open);
                byte[] array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
                string buf = Encoding.Default.GetString(array);
                s = buf.ToLower();
                tbCipherwithoutKey.Text = s;
            }
        }

        private void btnStatisticsInText_Click(object sender, RoutedEventArgs e)
        {
            tbl1.Text = "";
            tbl2.Text = "";
            tbl3.Text = "";
            tblStatisticInText.Text = "";
            double all = 0;
            if (rbEngDiscovery.IsChecked == true)
            {
                double[] Statis = new double[26];
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolENG(s[i], i);
                    if (ind != i)
                    {
                        Statis[ind]++;
                        all++;
                    }
                }
                for (int i = 0; i < AlphabetENG.Length; i++)
                {
                    double stat = Statis[i] / all;
                    tblStatisticInText.Text += AlphabetENG[i] + "=" + Math.Round(stat, 4) + "\n";
                }
            }
            else
            {
                double[] Statis = new double[33];
                for (int i = 0; i < s.Length; i++)
                {
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind != i)
                    {
                        Statis[ind]++;
                        all++;
                    }
                }
                for (int i = 0; i < AlphabetRUS.Length; i++)
                {
                    double stat = Statis[i] / all;
                    tblStatisticInText.Text += AlphabetRUS[i] + "=" + Math.Round(stat, 4) + "\n";
                }
            }
          //  char[] separator = { '.', ',', '!', '?', '/', '{', '|', '}', '(', ')', ':', ';' };
           // string[] strSplit = s.Split();
            string pat1 = @"\W[а-я]\W";
            string pat2 = @"\W[а-я]{2}\W";
            string pat3 = @"\W[а-я]{3}\W";
            //for (int i = 0; i < s.Length; i++)
            //{
                foreach (Match match in Regex.Matches(s, pat1))
                    tbl1.Text += match.Value + "\n";
                foreach (Match match in Regex.Matches(s, pat2))
                    tbl2.Text += match.Value + "\n";
                foreach (Match match in Regex.Matches(s, pat3))
                    tbl3.Text += match.Value + "\n";
            //}
        }
        private void btnReplacement_Click(object sender, RoutedEventArgs e)
        {
            string symb = tbSymbol.Text.Trim();
            symb += tbReplacement.Text.Trim();
            string dd = "";
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == symb[0])
                    dd += symb[1];
                else if (s[i] == symb[1])
                    dd += symb[0];
                else dd += s[i];
            }
            s = dd;
            tbSymbol.Text = "";
            tbReplacement.Text = "";
            tbCipherwithoutKey.Text = s;
        }
    }
    #endregion
}
