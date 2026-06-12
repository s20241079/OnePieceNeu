using OnePieceNeu.ViewModels;
using OnePieceNeu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnePieceNeu.ViewModels
{
    public class SchwierigkeitViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;

        private bool _isLeichtSelected = true;
        public bool IsLeichtSelected
        {
            get => _isLeichtSelected;
            set { _isLeichtSelected = value; OnPropertyChanged(); }
        }

        private bool _isMittelSelected;
        public bool IsMittelSelected
        {
            get => _isMittelSelected;
            set { _isMittelSelected = value; OnPropertyChanged(); }
        }

        private bool _isSchwerSelected;
        public bool IsSchwerSelected
        {
            get => _isSchwerSelected;
            set { _isSchwerSelected = value; OnPropertyChanged(); }
        }

        public ICommand WeiterCommand { get; }
        public ICommand ZurückCommand { get; }

        public SchwierigkeitViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            WeiterCommand = new Common.ActionCommand(o => Weiter(), o => true);
            ZurückCommand = new Common.ActionCommand(o => Zurück(), o => true);
        }

        private void Weiter()
        {
            string gewaehlteSchwierigkeit = "";

            if (IsLeichtSelected) gewaehlteSchwierigkeit = "leicht";
            else if (IsMittelSelected) gewaehlteSchwierigkeit = "mittel";
            else if (IsSchwerSelected) gewaehlteSchwierigkeit = "schwer";

            // Jetzt übergeben wir den ermittelten Text an die QuizView
            _mainViewModel.CurrentView = new QuizView(_mainViewModel, gewaehlteSchwierigkeit);
        }

        private void Zurück()
        {
            // Zurück zum Hauptmenü / Startbildschirm
            _mainViewModel.CurrentView = new StartView(_mainViewModel);
        }
    }
}
