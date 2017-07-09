using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace ConsoleOutput
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class OutputConsole : Window
    {
        public OutputConsole()
        {
            InitializeComponent();
        }

        public void WriteOutput(params string[] messages)
        {
            foreach (string message in messages)
                rtbConsoleOutput.AppendText(message);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            FileStream fileStream = File.Create("Console.log");
            rtbConsoleOutput.SelectAll();
            TextSelection textSelection = rtbConsoleOutput.Selection;
            textSelection.Save(fileStream, DataFormats.Text);
            fileStream.Flush();
            fileStream.Close();
        }
    }
}
