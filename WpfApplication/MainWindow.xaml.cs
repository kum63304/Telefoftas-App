using System;
using System.Collections.Generic;
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
using TelesoftasApp.Views;
using Unity.Attributes;

namespace TelesoftasApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        private static readonly Regex _regex = new Regex("[^0-9]+");
        public MainWindow()
        {
            InitializeComponent();
        }
      
        

        [Dependency]
        public MainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }

        private void StackPanel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        
    }
}
