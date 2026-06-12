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

        private async void AntwortGeklickt(object buchstabe)
        {
            if (_aktuelleFrage == null || buchstabe == null || _istEingabeGesperrt) return;

            _istEingabeGesperrt = true;

            string gewaehlterBuchstabe = buchstabe.ToString().ToUpper();
            string richtigeAntwort = _aktuelleFrage.KorrekteAntwort.ToUpper();

            FärbeButton(richtigeAntwort, "LightGreen");

            if (gewaehlterBuchstabe != richtigeAntwort)
            {
                FärbeButton(gewaehlterBuchstabe, "Salmon"); 
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
