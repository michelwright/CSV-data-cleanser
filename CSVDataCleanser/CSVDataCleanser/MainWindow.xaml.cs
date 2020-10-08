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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //    add_line_numbers();
            /*
            ToDo:
            1- Add File->import option to dropdown (done)
                -When import file is selected change window title to path (done)
                -Paste file content to richcontentbox (done)
            2- Finish up Find/Replace1 and Find/Replace2
                -Add apply button
             
             */
       //     ResultsTxtAreaOriginal.Document.PageWidth = 1000;
       //     pull_data("C:/Users/Mike/source/repos/WpfApp1/WpfApp1/bin/Debug/dataset.csv");
        }

        public String[] csv;

        private void import_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {

             //   ResultsTxtAreaOriginal.Document.Blocks.Clear();
                ResultsTxtAreaChanged.Document.Blocks.Clear();
                string filename = dlg.FileName;
                this.Title = "CSVDataCleanser - " + "[" + filename + "]";
                pull_data(filename);
            }
        }

        public void add_line_numbers() {
            //int count = csv.Length;
            int count = 50;
            for (int i = 1; i<=count; i++) {
                ListBoxItem item = new ListBoxItem();
                item.Focusable = false;
                item.Content = i;
                item.HorizontalAlignment = HorizontalAlignment.Right;
              //  lineNumbers_ListBox.Items.Add(item);
            }

        }

        public void pull_data(String filename)
        {
            string finalResult = "";
            List<String> csvText = new List<String>();
            try
            {
                using (var reader = new StreamReader(filename))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        finalResult += line + "\n";
                        csvText.Add(line + "\n");
                    }
                }
            //    ResultsTxtAreaOriginal.AppendText(finalResult);
                csv = csvText.ToArray();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
                throw;
            }
        }


        public void start_cleaner(string filename)
        {
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


        private void FilePathBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Console.WriteLine("Enter From the box!");
                //pull_data();
            }
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            Edit this later
            ^^^^^^^^^^^^^^^
            string fullText = return_Content_textArea(ResultsTxtAreaOriginal);
            string regexpText = regexp_TextBox.Text;
            string replace = replace1_TextBox.Text;
            string else_replace = else_TextBox.Text; 
            //string regexpText = "(,\")|(\",)";
            if (regexpText != "" && fullText != "" && replace != "") {
                run_regexp(regexpText, replace, else_replace);
            }
            */
        }


        public String regex1,replace1, regex2, replace2;

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (checkBox_Filter1.IsChecked == true)
            {
                this.regex1 = findBox1.Text;
                this.replace1 = findBox2.Text;
            }
            else {
                this.regex1 = "";
                this.replace1 = "";
            }
            if (checkBox_Filter2.IsChecked == true)
            {
                this.regex2 = step2_filePathBox.Text;
                this.replace2 = step2_filePathBox2.Text;
            }
            else {
                this.regex2 = "";
                this.replace2 = "";
            }

            if (regex1!="" && replace1!="") {
                if (regex2!="" && replace2!="") {
                    run_regexp(regex1,replace1,regex2,replace2); 
                }
                else {
                    run_regexp(regex1,replace1,"",""); 
                }
            }
*/
                /*
             
             
             */
        }

        public void run_regexp(string regexp, string replace, string else_regexp, string else_replace){
            string regX = regexp;
            Regex rgx = new Regex(regX);

            string regX2 = else_regexp;
            Regex rgx2 = new Regex(regX2);


            string results = "";
            string finalResult = "";

            foreach (var line in csv)
            {
                Match newMatch = rgx.Match(line);
                if (newMatch.Success)
                {
                    results = Regex.Replace(line, regexp, replace);
                    finalResult += results;
                }
                else
                {
                    if (else_regexp != "" && else_replace != "")
                    {
                        results = Regex.Replace(line, regX2, replace);
                    }
                    else {
                        results = line;
                    }
                    finalResult += results;
                }
            }

            Console.WriteLine("Completed:");
            Console.WriteLine(finalResult);
            //ResultsTxtAreaChanged.Document.Blocks.Add(new Paragraph(new Run(finalResult)));

         //   ResultsTxtAreaChanged.Document.Blocks.Clear();
        //    ResultsTxtAreaChanged.AppendText(finalResult);
        }

/*
        public void run_regexp(string originalText, string regexp, string replace)
        {
            //string regX = "(,\")|(\",)";
            string regX = regexp;
            Regex rgx = new Regex(regX);
            string results = "";
            string finalResult = "";


            //open CSV file
            try
            {
                using (var reader = new StreamReader(originalText))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        Match newMatch = rgx.Match(line);
                        if (newMatch.Success)
                        {
                            results = Regex.Replace(line, regexp, replace);
                            finalResult += results + Environment.NewLine;
                        }
                        else
                        {
                            results = Regex.Replace(line, "(,)", replace);
                            finalResult += results + Environment.NewLine;
                        }
                        //Console.WriteLine("FINAL CSV:");
                        //Console.WriteLine(finalResult);
                        Console.WriteLine("Completed:");
                        Console.WriteLine(finalResult);
                        ResultsTxtAreaChanged.Document.Blocks.Add(new Paragraph(new Run(finalResult)));
                    }
                }
                //File.WriteAllText(filename + "_cleaned.csv", finalResult, Encoding.UTF8);
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
                throw;
            }
        }
 */
        private string return_Content_textArea(RichTextBox rtb) {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        private void CheckBox_text_Click1(object sender, RoutedEventArgs e)
        {
        //    GroupBox1.IsEnabled = checkBox_Filter1.IsChecked == true ? true : false;
        }
        private void CheckBox_text_Click2(object sender, RoutedEventArgs e)
        {
        //    GroupBox2.IsEnabled = checkBox_Filter2.IsChecked == true ? true : false;
        }
    }
}
