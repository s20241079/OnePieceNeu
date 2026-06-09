using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using OnePieceNeu.Models;
using OnePieceNeu.Views;


namespace OnePieceNeu.ViewModels
{
    public class QuizViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        private string _schwierigkeitsgrad;

        private List<Frage> _gefilterteFragen;
        private int _aktuelleFrageIndex = 0;
        private Frage _aktuelleFrage;

        public string AktuellerFragentext
        {
            get => _aktuelleFrage?.Fragentext;
        }

        private string _antwortA;
        public string AntwortA
        {
            get => _antwortA;
            set { _antwortA = value; OnPropertyChanged(); }
        }

        private string _antwortB;
        public string AntwortB
        {
            get => _antwortB;
            set { _antwortB = value; OnPropertyChanged(); }
        }

        private string _antwortC;
        public string AntwortC
        {
            get => _antwortC;
            set { _antwortC = value; OnPropertyChanged(); }
        }

        private string _antwortD;
        public string AntwortD
        {
            get => _antwortD;
            set { _antwortD = value; OnPropertyChanged(); }
        }

        public ICommand AntwortAuswaehlenCommand { get; }
        public ICommand BeendenCommand { get; }

        public QuizViewModel(MainViewModel mainViewModel, string schwierigkeit)
        {
            _mainViewModel = mainViewModel;
            _schwierigkeitsgrad = schwierigkeit;

            AntwortAuswaehlenCommand = new Common.ActionCommand(parameter => AntwortGeklickt(parameter), o => true);
            BeendenCommand = new Common.ActionCommand(o => Beenden(), o => true);

            FragenAusDatenbankLaden();
        }

        private void FragenAusDatenbankLaden()
        {
            using (var db = new QuizContext())
            {
                // DIAGNOSE: Wie viele Fragen sind INSGESAMT in der DB?
                int gesamtAnzahl = db.Fragen.Count();
                if (gesamtAnzahl == 0)
                {
                    System.Windows.MessageBox.Show(
                        "Deine SQLite-Datenbank ist komplett LEER! Der Import aus der fragen.txt hat nicht geklappt.",
                        "Datenbank leer",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Error);
                    return;
                }
                // Wir trimmen die Auswahl und machen sie zu Kleinbuchstaben,
                // damit "Leicht", "leicht " oder "LEICHT" alle perfekt matchen!
                string gesuchteSchwierigkeit = _schwierigkeitsgrad.Trim().ToLower();

                _gefilterteFragen = db.Fragen
                    .Where(f => f.Schwierigkeitsgrad.Trim().ToLower() == gesuchteSchwierigkeit)
                    .ToList();
            }

            // Wenn die Liste leer ist, zeigen wir eine Warnung an, damit du weißt, was los ist
            if (_gefilterteFragen == null || _gefilterteFragen.Count == 0)
            {
                System.Windows.MessageBox.Show(
                    $"Datenbank-Fehler:\nEs wurden keine Fragen für die Schwierigkeit '{_schwierigkeitsgrad}' in der SQLite-Datenbank gefunden.\n\nPrüfe, ob das Wort exakt so in deiner fragen.txt steht!",
                    "Keine Daten",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                Beenden();
                return;
            }

            // Fragen zufällig durchmischen
            Random rnd = new Random();
            _gefilterteFragen = _gefilterteFragen.OrderBy(f => rnd.Next()).ToList();

            // Erste Frage aktivieren
            _aktuelleFrageIndex = 0;
            ZeigeAktuelleFrage();
        }

        private void ZeigeAktuelleFrage()
        {
            if (_aktuelleFrageIndex < _gefilterteFragen.Count)
            {
                _aktuelleFrage = _gefilterteFragen[_aktuelleFrageIndex];

                OnPropertyChanged(nameof(AktuellerFragentext));
                AntwortA = _aktuelleFrage.AntwortA;
                AntwortB = _aktuelleFrage.AntwortB;
                AntwortC = _aktuelleFrage.AntwortC;
                AntwortD = _aktuelleFrage.AntwortD;
            }
         
        }

        private void AntwortGeklickt(object buchstabe)
        {
            if (_aktuelleFrage == null || buchstabe == null) return;

            string gewaehlterBuchstabe = buchstabe.ToString().ToUpper();

            if (gewaehlterBuchstabe == _aktuelleFrage.KorrekteAntwort.ToUpper())
            {
                MessageBox.Show("Richtig! Hervorragend.", "Ergebnis");
            }
            else
            {
                MessageBox.Show($"Falsch! Die richtige Antwort wäre {_aktuelleFrage.KorrekteAntwort} gewesen.", "Ergebnis");
            }

            _aktuelleFrageIndex++;
            ZeigeAktuelleFrage();
        }

        private void Beenden()
        {
            _mainViewModel.CurrentView = new StartView(_mainViewModel);
        }
    }
}
