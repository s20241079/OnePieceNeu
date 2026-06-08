using OnePieceNeu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnePieceNeu.ViewModels
{
    public class StartViewModel : NotifyPropertyChanged
    {
        private MainViewModel _mainViewModel;

        public ICommand StartenCommand { get; }
        public ICommand BeendenCommand { get; }

        public StartViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            StartenCommand = new Common.ActionCommand(o => Starten(), o => true);
            BeendenCommand = new Common.ActionCommand(o => Beenden(), o => true);
        }

        private void Starten()
        {

        }

        private void Beenden()
        { 

            Application.Current.Shutdown();
        }
    }
}