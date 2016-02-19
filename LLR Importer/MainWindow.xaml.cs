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
using LLRAlbertaConverter;
using System.IO;

namespace LLRImporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CSVContent;

        public MainWindow()
        {
            InitializeComponent();

            CSVContent = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();

            openDialog.DefaultExt = ".txt";
            openDialog.Filter = "Text Files|*.txt";

            if (openDialog.ShowDialog() != true)
                return;

            using (var file = openDialog.OpenFile())
            {
                var converter = new AERConverter();

                CSVContent = converter.ConvertDocumentToCSV(file);

                if (CSVContent != null)
                {
                    using (StringReader reader = new StringReader(CSVContent))
                    {
                        string line = string.Empty;

                        do
                        {
                            line = reader.ReadLine();

                            if (line != null)
                            {
                                FileContentViewer.Items.Add(line);
                            }

                        } while (line != null);
                    }

                    FileContentViewer.IsEnabled = true;
                    SaveCSVButton.IsEnabled = true;
                }
            }
        }

        private void SaveCSVButton_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog();

            saveDialog.DefaultExt = ".csv";
            saveDialog.Filter = "Comma Separated Values (*.csv)|*.csv";

            if (saveDialog.ShowDialog() == true)
                System.IO.File.WriteAllText(saveDialog.FileName, CSVContent);
        }
    }
}
