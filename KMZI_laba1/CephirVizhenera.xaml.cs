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
using System.Windows.Shapes;

namespace KMZI_laba1
{
    public partial class CephirVizhenera : Window
    {
        string AlphabetRUS = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
        string AlphabetENG = "abcdefghijklmnopqrstuvwxyz";
        string s;
        string Word;
       // int TestKey = 0;
        OpenFileDialog FileOT = new OpenFileDialog();
        public CephirVizhenera()
        {
            InitializeComponent();
        }

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
            Word = tbWord.Text.Trim();
            string Cipher = "";
            if (rbEng.IsChecked == true)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int Key = GetSymbolENG(Word[i % Word.Length], i % Word.Length);
                    int ind = GetSymbolENG(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetENG[(ind + Key) % 26];
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int Key = GetSymbolRUS(Word[i % Word.Length], i % Word.Length);
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetRUS[(ind + Key) % 32];
                }
            }
            tblCipherText.Text = Cipher;
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
                tbCiphertext.Text = s;
            }
        }
        private void btnDecipher_Click(object sender, RoutedEventArgs e)
        {
            Word = tbWordDecipher.Text;
            string Cipher = "";
            if (rbEng.IsChecked == true)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int Key = GetSymbolENG(Word[i % Word.Length], i % Word.Length);
                    int ind = GetSymbolENG(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetENG[(Math.Abs(ind - Key)) % 26];
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    int Key = GetSymbolRUS(Word[i % Word.Length], i % Word.Length);
                    int ind = GetSymbolRUS(s[i], i);
                    if (ind == i)
                        Cipher += s[i];
                    else Cipher += AlphabetRUS[(Math.Abs(ind - Key)) % 32];
                }
            }
            tbOT.Text = Cipher;
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "All files (*.*)|*.*|TXT text (*.txt)|*.txt";
            if (sd.ShowDialog() == true)
            {
                File.WriteAllText(sd.FileName, Cipher);
            }
        }

        private void btnTranspositionAlphabet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
