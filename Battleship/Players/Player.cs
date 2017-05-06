using System;
using System.Collections.Generic;

namespace BattleshipGame
{
    class Player
    {
        public bool hasWon;

        public Player()
        {
            this.hasWon = false;
        }
        
        public bool checkIfPlayerHasWon(List<Ship> gridShips)
        {
            if (gridShips.Count == 0)
            {
                this.hasWon = true;
            }

            return this.hasWon;
        }
    }
}
