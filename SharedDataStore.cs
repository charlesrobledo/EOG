using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDiceGame
{
    public class SharedDataStore : ISharedDataStore
    {
        public int _games = 0;
        public int _turns = 0;
        
        public int Games
        {
            get { return _games; }
            set { _games = value; }
        }
        
        public int Turns
        {
            get { return _turns; }
            set { _turns = value; }

        }
    }

    public interface ISharedDataStore
    {
        int Games { get; set; }
        int Turns { get; set; }
    }
}
