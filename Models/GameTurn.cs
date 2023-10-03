using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfAppDiceGame.Models
{
    public class GameTurn {
        public string Game { get; set; } = String.Empty;
        public string Turn { get; set; } = string.Empty;

        public SeriesCollection? SeriesCollection { get; set; }

        //async Task<SeriesCollection> ICartesianService.GetGamesAndTurns(int games, int turns)
        //{
        //    games = 30;
        //    turns = 3;
        //    return await ;
        //}

        private void PlotGraph()
        {
            SeriesCollection = new LiveCharts.SeriesCollection();
            var lineSeries1 = new LineSeries
            {
                Title = "S1",
                Values = new ChartValues<double>() { 2.3, 2.0, 3.1, 1.3, 0.5, 3.8, 7.3, 2.4, 1.2, 0.1 },
                DataLabels = true,
                Stroke = Brushes.Green,
                Fill = Brushes.Transparent,
                ScalesYAt = 0
            };

            var lineSeries2 = new LineSeries
            {
                Title = "S2",
                Values = new ChartValues<double>() { 32.5, 34.5, 29.5, 26.0, 25.8, 30.5, 32.1, 36.5, 32.4, 24.5 },
                DataLabels = true,
                Stroke = Brushes.HotPink,
                Fill = Brushes.Transparent,
                ScalesYAt = 1
            };

            SeriesCollection.Add(lineSeries1);
            SeriesCollection.Add(lineSeries2);
        }
    }
}