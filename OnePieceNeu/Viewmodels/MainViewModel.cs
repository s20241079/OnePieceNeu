using OnePieceNeu.Common;
using OnePieceNeu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePieceNeu.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged // Erbt von deiner common-Klasse
    {
        private object _currentView;

        // Das ist das Gegenstück zum Binding im XAML
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                // Ruft die Methode aus deiner NotifyPropertyChanged-Klasse auf
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            // Wenn die App startet, laden wir direkt die StartView in das Fenster
            CurrentView = new StartView();
        }
    }
}