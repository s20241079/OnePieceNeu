using OnePieceNeu.ViewModels;
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


    /// <summary>
    /// Interaktionslogik für SchwierigkeitView.xaml
    /// </summary>
    namespace OnePieceNeu.Views // <- MUSS exakt so heißen wie im XAML vor dem letzten Punkt
    {
        public partial class SchwierigkeitView : UserControl // <- MUSS exakt so heißen wie im XAML nach dem letzten Punkt
        {
            private MainViewModel _mainViewModel;

            public SchwierigkeitView(MainViewModel mainViewModel)
            {
                InitializeComponent(); // Die rote Welle verschwindet, sobald Name & Namespace stimmen!

                _mainViewModel = mainViewModel;
                this.DataContext = new SchwierigkeitViewModel(mainViewModel);
            }
        }
    }

