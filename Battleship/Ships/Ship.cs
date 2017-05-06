using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace BattleshipGame
{
    abstract class Ship
    {
        public string shipType;
        public int    shipSize;
        public int    shipHealth;
        public bool   shipPlaced;
        public SolidColorBrush shipColor;

        public void ShipAttacked(Ship ship, List<Ship> gridShips)
        {
            ship.shipHealth--;
            if (ship.shipHealth == 0)
            {
                gridShips.Remove(ship);
            }
        }
    }
}
