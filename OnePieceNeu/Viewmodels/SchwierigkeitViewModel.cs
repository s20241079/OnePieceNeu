using OnePieceNeu.ViewModels;
using OnePieceNeu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePieceNeu.Views;
using System.Windows.Input;

namespace OnePieceNeu.Viewmodels
{
    public class SchwierigkeitViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;

        public bool IsLeichtSelected { get; set; } = true;
        public bool IsMittelSelected { get; set; }
        public bool IsSchwerSelected { get; set; }

        public ICommand ZurückCommand { get; }
        public ICommand WeiterCommand { get; }

        public SchwierigkeitViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            ZurückCommand = new Common.ActionCommand(o => Zurück(), o => true);
            WeiterCommand = new Common.ActionCommand(o => Weiter(), o => true);
        }

        private void Zurück()
        {
            _mainViewModel.CurrentView = new StartView(_mainViewModel);
        }

        private void Weiter()
        {
            string gewaehlteSchwierigkeit = "Leicht";
            if (IsMittelSelected) gewaehlteSchwierigkeit = "Mittel";
            if (IsSchwerSelected) gewaehlteSchwierigkeit = "Schwer";

            _mainViewModel.CurrentView = new QuizView(_mainViewModel, gewaehlteSchwierigkeit);
        }
    }
}
