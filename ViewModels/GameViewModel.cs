using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using WpfAppDiceGame.Models;

namespace WpfAppDiceGame.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        PlayerData playerData = new PlayerData();
        //public List<string> list = new List<string>();
        public List<string> list { get; set; }

        private int _preset;
        private int _players = 3;
        private int _games = 100;

        public int Players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged();
            }
        }
        public int Games
        {
            get { return _games; }
            set
            {
                _games = value;
                OnPropertyChanged();
            }
        }

        public int Preset
        {
            get { return _preset; }
            set
            {
                _preset = value;
                switch (_preset)
                {
                    case 0:
                        Players = 3;
                        Games = 100;
                        break;
                    case 1:
                        Players = 4;
                        Games = 100;
                        break;
                    case 2:
                        Players = 5;
                        Games = 100;
                        break;
                    case 3:
                        Players = 5;
                        Games = 1000;
                        break;
                    case 4:
                        Players = 5;
                        Games = 10000;
                        break;
                    case 5:
                        Players = 5;
                        Games = 100000;
                        break;
                    case 6:
                        Players = 6;
                        Games = 100;
                        break;
                    case 7:
                        Players = 7;
                        Games = 100;
                        break;
                    default:
                        break;
                }
            }
        }


        public ICommand PlayCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public int GameTurns { get; private set; }
        public int NumberOfTotalGamesTurns { get; private set; }
        public int GrandChampion { get; private set; }
        public int ShortGameWinner { get; private set; }
        public int LongGameWinner { get; private set; }
        public int NumberOfTotalTurns { get; private set; }
        public string play = "play";
        public string cancel = "cancel";

        public GameViewModel()
        {
            PlayCommand = new RelayCommand(() => SelectedGameAndTurn(play));
            CancelCommand = new RelayCommand(() => SelectedGameAndTurn(cancel));
            list = playerData.GetPlayerData();
        }
        private void SelectedGameAndTurn(string playcancel)
        {
            try
            {
                switch (Preset)
                {
                    case 0:
                        RollTheDice(3, 100);
                        break;
                    case 1:
                        RollTheDice(4, 100);
                        break;
                    case 2:
                        RollTheDice(5, 100);
                        break;
                    case 3:
                        RollTheDice(5, 1000);
                        break;
                    case 4:
                        RollTheDice(5, 10000);
                        break;
                    case 5:
                        RollTheDice(5, 100000);
                        break;
                    case 6:
                        RollTheDice(6, 100);
                        break;
                    case 7:
                        RollTheDice(7, 100);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RollTheDice(int a, int b)
        {
            int shortestGame = 1000;
            int longestGame = 0;
            bool Winner = false;
            int games = b;
            int Result = 0;
            int[] BiggestWinner = Enumerable.Repeat(0, a).ToArray();

            //Instantiate a new random number generator
            var rng = new Random();

            while (games > 0)
            {
                int[] playerChipCount = Enumerable.Repeat(3, a).ToArray();
                Result = 0;

                while (!Winner)
                {
                    try
                    {
                        for (int players = 0; players < a; players++)
                        {
                            ModifyChipCount(ref a, playerChipCount, rng, players, Result);
                        }

                        Result = playerChipCount.Count(i => i == 0);
                        if (Result == a - 1)
                        {
                            Winner = true;

                            var index = Array.FindIndex(playerChipCount, i => i != 0);
                            BiggestWinner[index]++;

                            if (GameTurns < shortestGame)
                                shortestGame = GameTurns;

                            if (GameTurns > longestGame)
                                longestGame = GameTurns;

                            GameTurns = 0;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                Winner = false;
                games--;
            }

            ShortGameWinner = shortestGame;
            LongGameWinner = longestGame;
            NumberOfTotalGamesTurns = NumberOfTotalTurns;
            GrandChampion = BiggestWinner.OrderByDescending(x => x).First();

            Array.Clear(BiggestWinner, 0, BiggestWinner.Length);
            NumberOfTotalTurns = 0;
            shortestGame = 0;
            longestGame = 0;

        }

        private void ModifyChipCount(ref int a, int[] playerChipCount, Random rng, int players, int Result)
        {
            int IndividualTosses = 0;

            Result = playerChipCount.Count(i => i != 0);
            if (Result > 1)
            {
                for (int i = 0; i < playerChipCount[players]; i++)
                {
                    while (IndividualTosses < 3 && IndividualTosses < playerChipCount[players])
                    {
                        GameTurns++;
                        NumberOfTotalTurns++;
                        int randomNumber = rng.Next(6);
                        IndividualTosses++;

                        switch (players)
                        {
                            case 0:
                                SpecialCaseMinIndex(a, playerChipCount, players, randomNumber);
                                break;
                            case 1:
                                SwitchCode(playerChipCount, players, randomNumber);
                                break;
                            case 2:
                                if (players == a - 1)
                                    SpecialCaseMaxIndex(a, playerChipCount, players, randomNumber);
                                else
                                    SwitchCode(playerChipCount, players, randomNumber);
                                break;
                            case 3:
                                if (players == a - 1)
                                    SpecialCaseMaxIndex(a, playerChipCount, players, randomNumber);
                                else
                                    SwitchCode(playerChipCount, players, randomNumber);
                                break;
                            case 4:
                                if (players == a - 1)
                                    SpecialCaseMaxIndex(a, playerChipCount, players, randomNumber);
                                else
                                    SwitchCode(playerChipCount, players, randomNumber);
                                break;
                            case 5:
                                if (players == a - 1)
                                    SpecialCaseMaxIndex(a, playerChipCount, players, randomNumber);
                                else
                                    SwitchCode(playerChipCount, players, randomNumber);
                                break;
                            case 6:
                                if (players == a - 1)
                                    SpecialCaseMaxIndex(a, playerChipCount, players, randomNumber);
                                else
                                    SwitchCode(playerChipCount, players, randomNumber);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void SpecialCaseMinIndex(int a, int[] playerChipCount, int players, int randomNumber)
        {
            if (randomNumber == 0)
                playerChipCount[a - 1]++;
            else if (randomNumber == 1)
                playerChipCount[players + 1]++;

            if (randomNumber == 0 || randomNumber == 1 || randomNumber == 2)
                playerChipCount[players]--;
        }

        private void SpecialCaseMaxIndex(int a, int[] playerChipCount, int players, int randomNumber)
        {
            if (randomNumber == 0)
                playerChipCount[a - 1]++;
            else if (randomNumber == 1)
                playerChipCount[0]++;

            if (randomNumber == 0 || randomNumber == 1 || randomNumber == 2)
                playerChipCount[players]--;
        }
        private void SwitchCode(int[] playerChipCount, int players, int randomNumber)
        {
            if (randomNumber == 0)
                playerChipCount[players - 1]++;
            else if (randomNumber == 1)
                playerChipCount[players + 1]++;

            if (randomNumber == 0 || randomNumber == 1 || randomNumber == 2)
                playerChipCount[players]--;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}