using OnePieceNeu.Common;
using OnePieceNeu.Models;
using OnePieceNeu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OnePieceNeu.Views;
using Microsoft.EntityFrameworkCore;

namespace OnePieceNeu.ViewModels
{
    public class BountyViewModel : NotifyPropertyChanged
    {
        private MainViewModel _mainViewModel;

        // Die Liste für deine View
        public List<Bounty> BountyListe { get; set; }

        public ICommand ZurueckCommand { get; }

        public BountyViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ZurueckCommand = new Common.ActionCommand(o => Zurueck(), o => true);

            DatenLaden();
        }

        private void DatenLaden()
        {
            using (var db = new QuizContext())
            {
                BountyListe = db.Bounties
                                .OrderByDescending(b => b.Score)
                                .ToList();
            }
        }

        private void Zurueck()
        {
            _mainViewModel.CurrentView = new StartViewModel(_mainViewModel);
        }
    }
}
