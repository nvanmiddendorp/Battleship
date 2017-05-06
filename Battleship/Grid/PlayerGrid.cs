using BattleshipGame.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleshipGame
{
    class PlayerGrid : GameGrid
    {
        public PlayerGrid(Grid gameGrid)
        {
            _gameGrid = new Grid[100];
            isComputer = false;

            //Sets datasource for grid from UI
            gameGrid.Children.CopyTo(_gameGrid, 0);          
        }

        public override void InitializeGrid()
        {
            _gridSquares = new List<GridSquare>();

            for (int i = 0; i < GRID_SIZE; i++)
            {
                _gridSquares.Add(new GridSquare("Water", new SolidColorBrush(Colors.DarkBlue)));
                _gameGrid[i].Tag = _gridSquares[i].squareContents;
                _gameGrid[i].Background = _gridSquares[i].squareColor;
            }

            CreateShips();
            PlaceShips();
        }

        /// <summary>
        /// Attacks the player's grid and colors the square accordingly
        /// </summary>
        public void AttackGrid()
        {
            bool aquiredSeed = false;
            SolidColorBrush resultColor = new SolidColorBrush();
            int gridSeed = seedGen.getGridSeed();
            aquiredSeed = _gridSquares[gridSeed].AttackGrid();

            //Keep trying to find a random square that hasn't been attacked
            while(aquiredSeed)
            {
                gridSeed = seedGen.getGridSeed();
                aquiredSeed = _gridSquares[gridSeed].AttackGrid();
            }

            if (!_gridSquares[gridSeed].attacked)
            {
                //Need to set the square to attacked so the computer doesn't try to attack it again
                _gridSquares[gridSeed].attacked = true;

                //Get what is in the square
                string squareContents = _gridSquares[gridSeed].squareContents;
                _gameGrid[gridSeed].Background = new SolidColorBrush(Colors.DarkRed);

                switch (squareContents)
                {
                    case "Carrier":
                        carrier.ShipAttacked(carrier, _gridShips);
                        break;
                    case "Battleship":
                        battleship.ShipAttacked(battleship, _gridShips);
                        break;
                    case "Cruiser":
                        cruiser.ShipAttacked(cruiser, _gridShips);
                        break;
                    case "Submarine":
                        submarine.ShipAttacked(submarine, _gridShips);
                        break;
                    case "Destroyer":
                        destroyer.ShipAttacked(destroyer, _gridShips);
                        break;
                    default:
                        _gameGrid[gridSeed].Background = new SolidColorBrush(Colors.DarkGray);
                        break;
                }
            }
        }
    }
}
