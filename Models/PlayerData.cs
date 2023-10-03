using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDiceGame.Models
{
    public class PlayerData
    {
        public string? PlayerCount { get; set; }

        public string? GameCount { get; set; }

        public List<string> GetPlayerData()
        {
            List<string> list = new List<string>() {
            { "3 players x 100 games" },
            { "4 players x 100 games" },
            { "5 players x 100 games" },
            { "5 players x 1,000 games" },
            { "5 players x 10,000 games" },
            { "5 players x 100,000 games" },
            { "6 players x 100 games" },
            { "7 players x 100 games" }
        };

            return list;
        }
    }
}
