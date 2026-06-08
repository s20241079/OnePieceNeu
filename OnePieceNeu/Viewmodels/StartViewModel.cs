using OnePieceNeu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using OnePieceNeu.Models;
using System.IO;

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

            FragenImportieren();
        }

        private void Starten()
        {
        }

        private void Beenden()
        {
            Application.Current.Shutdown();
        }

        private void FragenImportieren()
        {
            using (var db = new QuizContext())
            {

                if (!db.Fragen.Any())
                {
                    string dateiPfad = "fragen.txt";

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