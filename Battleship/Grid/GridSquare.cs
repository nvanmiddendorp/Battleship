using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BattleshipGame
{
    class GridSquare
    {
        public string squareContents { get; set; }
        public SolidColorBrush squareColor { get; set; }
        public bool attacked { get; set; }

        public GridSquare(string squareContents, SolidColorBrush squareColor)
        {
            this.squareContents = squareContents;
            this.squareColor = squareColor;
            this.attacked = false;
        }

        public bool AttackGrid()
        {
            return this.attacked;
        }
    }
}
