using OnePieceNeu.Models;
using OnePieceNeu.ViewModels;
using OnePieceNeu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnePieceNeu.Viewmodels
{
    public class ErgebnisViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        private int _score;


        public string ErgebnisText => $"Du hast {_score} von 10 Punkten erreicht!";

        private string _spielerName = "Luffy";
        public string SpielerName
        {
            get => _spielerName;
            set { _spielerName = value; OnPropertyChanged(); }
        }

        public ICommand SpeichernUndHighscoreCommand { get; }

        public ErgebnisViewModel(MainViewModel mainViewModel, int score)
        {
            _mainViewModel = mainViewModel;
            _score = score;

            SpeichernUndHighscoreCommand = new Common.ActionCommand(o => Speichern(), o => true);
        }

        private void Speichern()
        {
            if (string.IsNullOrWhiteSpace(SpielerName))
            {
                System.Windows.MessageBox.Show("Bitte trage einen gültigen Namen ein!");
                return;
            }

            using (var db = new QuizContext())
            {
                var neuerEintrag = new Bounty
                {
                    SpielerName = this.SpielerName,
                    Score = this._score
                };

                db.Bounties.Add(neuerEintrag);

                db.SaveChanges();
            }

            System.Windows.MessageBox.Show("Dein Score wurde erfolgreich in der Bounty-Liste eingetragen!");

            _mainViewModel.CurrentView = new StartView(_mainViewModel);
        }
    }
}
