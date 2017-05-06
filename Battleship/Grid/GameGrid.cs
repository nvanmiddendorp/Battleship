using BattleshipGame.Ships;
using BattleshipGame.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleshipGame
{
    abstract class GameGrid
    {  
        //Constants for game rules
        public const int GRID_SIZE = 100;
        public enum SHIPS
        {
            Carrier = 1,
            Battleship = 2,
            Cruiser = 3,
            Submarine = 4,
            Destroyer = 5,
        }
        public enum SHIP_SIZES
        {
            CarrierSize = 5,
            BattleshipSize = 4,
            CruiserSize = 3,
            SubmarineSize = 3,
            DestroyerSize = 2
        }
        public enum SHIP_ORIENTATION
        {
            Horizontal = 0,
            Vertical = 1
        }

        //Grid Elements
        public Grid[] _gameGrid;
        public List<GridSquare> _gridSquares;
        public List<Ship> _gridShips;
        public Carrier carrier;
        public Battleship battleship;
        public Cruiser cruiser;
        public Submarine submarine;
        public Destroyer destroyer;

        //Grid Properties
        public bool isComputer;

        //Grid Utilities
        public SeedGenerator seedGen = new SeedGenerator(GRID_SIZE);

        //Grid Methods
        /// <summary>
        /// Initialize the grid with ships.  For human players it keeps the squares colored
        /// and for computer players it keeps the squares grey so you don't know where the
        /// ships are.
        /// </summary>
        abstract public void InitializeGrid();

        /// <summary>
        /// Creates new instances of the ships and adds them to a list that tracks if they are alive or dead
        /// </summary>
        public void CreateShips()
        {
            _gridShips = new List<Ship>();

            //Loop through each type of ship
            foreach (SHIPS ship in Enum.GetValues(typeof(SHIPS)))
            {
                switch(ship)
                {
                    case SHIPS.Carrier:
                        carrier = new Carrier(ship.ToString(), (int)SHIP_SIZES.CarrierSize, new SolidColorBrush(Colors.DarkGreen));
                        _gridShips.Add(carrier);
                        break;
                    case SHIPS.Battleship:
                        battleship = new Battleship(ship.ToString(), (int)SHIP_SIZES.BattleshipSize, new SolidColorBrush(Colors.SeaGreen));
                        _gridShips.Add(battleship);
                        break;
                    case SHIPS.Cruiser:
                        cruiser= new Cruiser(ship.ToString(), (int)SHIP_SIZES.CruiserSize, new SolidColorBrush(Colors.Cyan));
                        _gridShips.Add(cruiser);
                        break;
                    case SHIPS.Submarine:
                        submarine = new Submarine(ship.ToString(), (int)SHIP_SIZES.SubmarineSize, new SolidColorBrush(Colors.Yellow));
                        _gridShips.Add(submarine);
                        break;
                    case SHIPS.Destroyer:
                        destroyer = new Destroyer(ship.ToString(), (int)SHIP_SIZES.DestroyerSize, new SolidColorBrush(Colors.Violet));
                        _gridShips.Add(destroyer);
                        break;
                }
            }
        }

        /// <summary>
        /// Places the ships in the grid
        /// </summary>
        public void PlaceShips()
        {
            //Place each ship horizontally or vertically
            foreach (Ship ship in _gridShips)
            {
                //Keep trying to find a valid spot to place the ship
                while (ship.shipPlaced == false)
                {
                    int gridSeed = seedGen.getGridSeed();

                    //Random roll to place the ships vertically or horizontally
                    int orientation = seedGen.getShipOrientation();

                    //Horizontal
                    if (orientation == (int)SHIP_ORIENTATION.Horizontal && TryHorizontalPlacement(gridSeed, ship))
                    {
                        //For horizontal placement we take the starting square and place in each consecutive right square
                        for (int i = gridSeed; i < gridSeed + ship.shipSize; i++)
                        {
                            _gridSquares[i].squareContents = ship.shipType;
                            _gameGrid[i].Tag = ship.shipType;

                            //Keep computer squares hidden
                            if (!this.isComputer)
                            {
                                _gameGrid[i].Background = ship.shipColor;
                            }
                        }
                    }
                    //Vertical
                    else if (orientation == (int)SHIP_ORIENTATION.Vertical && TryVerticalPlacement(gridSeed, ship))
                    {
                        //For vertical placement we need to go down one row and place up to down, so we have to add 10 to the index
                        //The loop index also must go from the initial seed number to shipSize*10 to accomodate this range
                        for (int i = gridSeed; i < gridSeed + (ship.shipSize * 10); i = i + 10)
                        {
                            _gridSquares[i].squareContents = ship.shipType;
                            _gameGrid[i].Tag = ship.shipType;

                            //Keep computer squares hidden
                            if (!this.isComputer)
                            {
                                _gameGrid[i].Background = ship.shipColor;
                            }
                        }
                    }
                }
            }
        }

        public bool TryHorizontalPlacement(int gridSeed, Ship ship)
        {
            /*
             * The first statement ensures that the ship isn't going to be placed off the end of the grid.
             * For example if the seed was 98 and the ship was 3 long, the ship would extend passed the game grid
             * since there are only 100 grid squares.
             *
             * The second statement ensures that the ship isn't going to overlap from one row to the next.
             * For example if the seed was 19 and the ship size was 4 the ship would wrap to the next row and that's just silly.
            */
            if ((gridSeed + ship.shipSize) < GRID_SIZE && ((gridSeed + ship.shipSize - 1) % 10 > ship.shipSize - 1))
            {
                for (int i = gridSeed; i < gridSeed + ship.shipSize; i++)
                {
                    if (_gridSquares[i].squareContents != "Water")
                    {
                        return ship.shipPlaced;
                    }
                }

                ship.shipPlaced = true;
            }

            return ship.shipPlaced;
        }

        public bool TryVerticalPlacement(int gridSeed, Ship ship)
        {
            /*
             * This statement ensures that the ship is not going to be placed off the bottom of the game grid.
             * For example if the seed was 70 and the ship was 5 long 70 + 50 > 100 so it wouldn't fit!
            */
            if ((gridSeed + (ship.shipSize * 10)) < GRID_SIZE)
            {
                for (int i = gridSeed; i < gridSeed + (ship.shipSize * 10); i = i + 10)
                {
                    if (_gridSquares[i].squareContents != "Water")
                    {
                        return ship.shipPlaced;
                    }
                }

                ship.shipPlaced = true;
            }

            return ship.shipPlaced;
        }
    }
}
