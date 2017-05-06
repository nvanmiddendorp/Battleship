using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleshipGame
{
    class ComputerGrid : GameGrid
    {
        public ComputerGrid(Grid gameGrid)
        {
            _gameGrid = new Grid[100];
            isComputer = true;

            //Sets datasource for grid from UI
            gameGrid.Children.CopyTo(_gameGrid, 0);
        }

        public override void InitializeGrid()
        {
            _gridSquares = new List<GridSquare>();

            for (int i = 0; i < GRID_SIZE; i++)
            {
                _gridSquares.Add(new GridSquare("Water", new SolidColorBrush(Colors.Gray)));
                _gameGrid[i].Tag = _gridSquares[i].squareContents;
                _gameGrid[i].Background = _gridSquares[i].squareColor;

                //Make sure the user can click on the square in the UI again
                _gameGrid[i].IsEnabled = true;
            }

            CreateShips();
            PlaceShips();
        }

        /// <summary>
        /// Attacks the computer's game grid and colors the square accordingly.
        /// </summary>
        /// <param name="gameSquare"></param>
        /// <returns></returns>
        public SolidColorBrush AttackGrid(Grid gameSquare)
        {
            SolidColorBrush resultColor = new SolidColorBrush(Colors.DarkRed);
            string squareContents = gameSquare.Tag.ToString();

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
                    resultColor.Color = Colors.DarkBlue;
                    break;
            }

            return resultColor;
        }
    }
}
