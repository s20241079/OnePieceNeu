using OnePieceNeu.Common;
using OnePieceNeu.Models;
using OnePieceNeu.Viewmodels;
using OnePieceNeu.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnePieceNeu.ViewModels
{
    public class StartViewModel : NotifyPropertyChanged
    {
        private MainViewModel _mainViewModel;

        public ICommand StartenCommand { get; }
        public ICommand BountyCommand { get; } 
        public ICommand BeendenCommand { get; }

        public StartViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            StartenCommand = new Common.ActionCommand(o => Starten(), o => true);
            BountyCommand = new Common.ActionCommand(o => ZeigeBountyListe(), o => true);
            BeendenCommand = new Common.ActionCommand(o => Beenden(), o => true);

            FragenImportieren();
        }

        private void Starten()
        {
            _mainViewModel.CurrentView = new SchwierigkeitView(_mainViewModel);
        }

        private void ZeigeBountyListe()
        {
            _mainViewModel.CurrentView = new BountyViewModel(_mainViewModel);
        }

        private void Beenden()
        {
            Application.Current.Shutdown();
        }

        private void FragenImportieren()
        {
            using (var db = new QuizContext())
            {
                db.Database.EnsureCreated();
                if (!db.Fragen.Any())
                {
                    string basisOrdner = AppDomain.CurrentDomain.BaseDirectory;
                    string dateiPfad = @"C:\OnePieceNeu\OnePieceNeu\fagen.txt";

                    if (File.Exists(dateiPfad))
                    {
                        string[] zeilen = File.ReadAllLines(dateiPfad);

                        foreach (string zeile in zeilen)
                        {
                            if (string.IsNullOrWhiteSpace(zeile)) continue;

                            string[] teile = zeile.Split(';');

                            if (teile.Length == 7)
                            {
                                db.Fragen.Add(new Frage
                                {
                                    Fragentext = teile[0].Trim(),
                                    AntwortA = teile[1].Trim(),
                                    AntwortB = teile[2].Trim(),
                                    AntwortC = teile[3].Trim(),
                                    AntwortD = teile[4].Trim(),
                                    KorrekteAntwort = teile[5].Trim(),
                                    Schwierigkeitsgrad = teile[6].Trim()
                                });
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}