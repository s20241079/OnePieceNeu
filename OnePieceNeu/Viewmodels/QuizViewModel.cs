using OnePieceNeu.Models;
using OnePieceNeu.Viewmodels;
using OnePieceNeu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;



namespace OnePieceNeu.ViewModels
{
    public class QuizViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        private string _schwierigkeitsgrad;
        private int _score = 0;
        private List<Frage> _gefilterteFragen;
        private int _aktuelleFrageIndex = 0;
        private Frage _aktuelleFrage = new Frage();

        private const string StandardGelb = "#FFF1F19F";

        private bool _istEingabeGesperrt = false;

        public string AktuellerFragentext
        {
            get => _aktuelleFrage.Fragentext;
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

        private string _hintergrundA = StandardGelb;
        public string HintergrundA
        {
            get => _hintergrundA;
            set { _hintergrundA = value; OnPropertyChanged(); }
        }

        private string _hintergrundB = StandardGelb;
        public string HintergrundB
        {
            get => _hintergrundB;
            set { _hintergrundB = value; OnPropertyChanged(); }
        }

        private string _hintergrundC = StandardGelb;
        public string HintergrundC
        {
            get => _hintergrundC;
            set { _hintergrundC = value; OnPropertyChanged(); }
        }

        private string _hintergrundD = StandardGelb;
        public string HintergrundD
        {
            get => _hintergrundD;
            set { _hintergrundD = value; OnPropertyChanged(); }
        }

        public ICommand AntwortAuswaehlenCommand { get; }
        public ICommand BeendenCommand { get; }

        public QuizViewModel(MainViewModel mainViewModel, string schwierigkeit)
        {
            _mainViewModel = mainViewModel;
            _schwierigkeitsgrad = schwierigkeit;
            _score = 0;
            AntwortAuswaehlenCommand = new Common.ActionCommand(parameter => AntwortGeklickt(parameter), o => true);
            BeendenCommand = new Common.ActionCommand(o => Beenden(), o => true);

            FragenAusDatenbankLaden();
        }

        private void FragenAusDatenbankLaden()
        {
            using (var db = new QuizContext())
            {
                string gesuchteSchwierigkeit = _schwierigkeitsgrad.Trim().ToLower();

                _gefilterteFragen = db.Fragen
                    .Where(f => f.Schwierigkeitsgrad.Trim().ToLower() == gesuchteSchwierigkeit)
                    .ToList();
            }

            Random rnd = new Random();
            _gefilterteFragen = _gefilterteFragen.OrderBy(f => rnd.Next()).ToList();

            _aktuelleFrageIndex = 0;
            ZeigeAktuelleFrage();
        }

        private void ZeigeAktuelleFrage()
        {
            if (_aktuelleFrageIndex < 10)
            {
                _aktuelleFrage = _gefilterteFragen[_aktuelleFrageIndex];

                OnPropertyChanged(nameof(AktuellerFragentext));
                AntwortA = _aktuelleFrage.AntwortA;
                AntwortB = _aktuelleFrage.AntwortB;
                AntwortC = _aktuelleFrage.AntwortC;
                AntwortD = _aktuelleFrage.AntwortD;
            }
            else
            {
                var _ErgebnisView = new ErgebnisView();
                _ErgebnisView.DataContext = new ErgebnisViewModel(_mainViewModel, _score);
                _mainViewModel.CurrentView = _ErgebnisView;

            }
        }

        private async void AntwortGeklickt(object buchstabe)
        {
            if (_istEingabeGesperrt) return;

            _istEingabeGesperrt = true;

            string gewaehlterBuchstabe = buchstabe.ToString().ToUpper();
            string richtigeAntwort = _aktuelleFrage.KorrekteAntwort.ToUpper();

            FärbeButton(richtigeAntwort, "LightGreen");

            if (gewaehlterBuchstabe != richtigeAntwort)
            {
                FärbeButton(gewaehlterBuchstabe, "Salmon"); 
            }
            else
            {
                _score++;
            }


                await Task.Delay(2500);

            HintergrundA = StandardGelb;
            HintergrundB = StandardGelb;
            HintergrundC = StandardGelb;
            HintergrundD = StandardGelb;

            _istEingabeGesperrt = false;

            _aktuelleFrageIndex++;
            ZeigeAktuelleFrage();
        }

        private void FärbeButton(string buchstabe, string farbe)
        {
            switch (buchstabe)
            {
                case "A": HintergrundA = farbe; break;
                case "B": HintergrundB = farbe; break;
                case "C": HintergrundC = farbe; break;
                case "D": HintergrundD = farbe; break;
            }
        }

        private void Beenden()
        {
            _mainViewModel.CurrentView = new StartView(_mainViewModel);
        }

    }
}
