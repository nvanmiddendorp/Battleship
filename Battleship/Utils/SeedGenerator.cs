using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Utils
{
    class SeedGenerator
    {
        Random seed;
        int gridSize;

        public SeedGenerator(int gridSize)
        {
            this.gridSize = gridSize;
            seed = new Random();
        }

        public int getGridSeed()
        {
            return seed.Next(gridSize);
        }

        public int getShipOrientation()
        {
            return seed.Next(2);
        }
    }
}
