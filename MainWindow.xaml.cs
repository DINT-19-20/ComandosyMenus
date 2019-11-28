using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace Comandos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> lista;
        public string CurrentItem { get; set; } = null;
        public string CopiedItem { get; set; } = null;

        public MainWindow()
        {
            lista = new ObservableCollection<string>();
            InitializeComponent();
            DispatcherTimer temporizador = new DispatcherTimer();
            temporizador.Interval = TimeSpan.FromSeconds(1);
            temporizador.Tick += Temporizador_Tick;
            temporizador.Start();
            ListadoListBox.DataContext = lista;
        }

        private void Temporizador_Tick(object sender, EventArgs e)
        {
            HoraTextBlock.Text = DateTime.Now.ToLongTimeString();
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lista.Add($"Item añadido a las {DateTime.Now.ToLongTimeString()}");
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lista.Count < 10;
        }

        private void ClearCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lista.Clear();
        }

        private void ClearCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lista.Count > 0;
        }

        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CopiedItem = CurrentItem;
        }

        private void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CurrentItem != null;
        }

        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lista.Add(CopiedItem);
            CopiedItem = null;
        }

        private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CopiedItem != null;
        }
    }
}
