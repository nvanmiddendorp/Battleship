using System;
using System.Windows.Media;

namespace BattleshipGame.Ships
{
    class Destroyer : Ship
    {
        public Destroyer(string shipType, int shipSize, SolidColorBrush shipColor)
        {
            this.shipType = shipType;
            this.shipSize = shipSize;
            this.shipHealth = shipSize;
            this.shipPlaced = false;
            this.shipColor = shipColor;
        }
    }
}
