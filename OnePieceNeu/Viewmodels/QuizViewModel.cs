using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OnePieceNeu.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using OnePieceNeu.Viewmodels;
using OnePieceNeu.Views;

namespace OnePieceNeu.ViewModels
{
    public class QuizViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        private string _schwierigkeitsgrad;

        // Diese Eigenschaften sind im XAML über {Binding ...} verknüpft
        public string AktuellerFragentext { get; set; }
        public string AntwortA { get; set; }
        public string AntwortB { get; set; }
        public string AntwortC { get; set; }
        public string AntwortD { get; set; }

        public ICommand AntwortAuswaehlenCommand { get; }
        public ICommand BeendenCommand { get; }

        // Der Konstruktor fängt die Daten ab, die aus der Schwierigkeitsauswahl kommen
        public QuizViewModel(MainViewModel mainViewModel, string schwierigkeit)
        {
            _mainViewModel = mainViewModel;
            _schwierigkeitsgrad = schwierigkeit;

            // Testweise befüllen wir die Eigenschaften mit Text, um zu sehen ob es klappt:
            AktuellerFragentext = $"Du spielst auf {schwierigkeit}! Wer ist Ruffys Vize-Kapitän?";
            AntwortA = "Zorro";
            AntwortB = "Nami";
            AntwortC = "Sanji";
            AntwortD = "Garp";

            // Commands an leere Test-Methoden binden, damit nichts abstürzt
            AntwortAuswaehlenCommand = new Common.ActionCommand(parameter => AntwortGeklickt(parameter), o => true);
            BeendenCommand = new Common.ActionCommand(o => Beenden(), o => true);
        }

        private void AntwortGeklickt(object buchstabe)
        {
            // Zeigt an, welchen Button (A, B, C oder D) du geklickt hast
            MessageBox.Show($"Du hast Antwort {buchstabe} gewählt!");
        }

        private void Beenden()
        {
            // Bringt uns im Moment einfach zurück zum Hauptmenü
            _mainViewModel.CurrentView = new StartView(_mainViewModel);
        }
    }
}
