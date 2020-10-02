using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CSVDataCleanser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // start_cleaner("Native-Owned Online Businesses");
        }

        public void pull_data() {
            if (filePathBox.Text != "") {
                Console.WriteLine("Box is not empty!");
                string filename = filePathBox.Text;
                string finalResult = "";
                try
                {
                    using (var reader = new StreamReader(filename))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            finalResult += line + "\n";

                            Console.WriteLine("FINAL CSV:");
                            Console.WriteLine(finalResult);
                        }
                    }
                    ResultsTxtAreaOriginal.AppendText(finalResult);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error!");
                    throw;
                }
            }
        }


        public void start_cleaner(string filename) {
            string regX = "(,\")|(\",)";
            Regex rgx = new Regex(regX);
            string results = "";
            string finalResult = "";


            //open CSV file
            try
            {
                using (var reader = new StreamReader(filename + ".csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        Match newMatch = rgx.Match(line);
                        if (newMatch.Success)
                        {
                            results = Regex.Replace(line, "(,\")|(\",)", ";");
                            finalResult += results + Environment.NewLine;
                        }
                        else
                        {
                            results = Regex.Replace(line, "(,)", ";");
                            finalResult += results + Environment.NewLine;
                        }
                        Console.WriteLine("FINAL CSV:");
                        Console.WriteLine(finalResult);
                    }
                }
                File.WriteAllText(filename + "_cleaned.csv", finalResult, Encoding.UTF8);
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
                throw;
            }

            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true) {
                string filename = dlg.FileName;
                filePathBox.Text = filename;
                pull_data();
            }

        }

        private void FilePathBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Console.WriteLine("Enter From the box!");
                pull_data();
            }
        }
    }
}
