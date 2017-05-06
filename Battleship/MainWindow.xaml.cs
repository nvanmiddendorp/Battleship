using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleshipGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player human;
        Player computer;
        PlayerGrid playerGrid;
        ComputerGrid computerGrid;

        public MainWindow()
        {
            InitializeComponent();

            //Make game objects
            human = new Player();
            computer = new Player();
            playerGrid = new PlayerGrid(PlayerGrid);
            computerGrid = new ComputerGrid(CompGrid);

            //Initialize game elements
            playerGrid.InitializeGrid();
            computerGrid.InitializeGrid();
        }

        /// <summary>
        /// Attack event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Set sender to square chosen
            Grid square = (Grid)sender;
            square.Background = computerGrid.AttackGrid(square);

            //Ensure that the user can't select the same square twice and have the computer attack
            square.IsEnabled = false;

            //Computer is attacking
            playerGrid.AttackGrid();          

            if(human.checkIfPlayerHasWon(computerGrid._gridShips))
            {
                MessageBox.Show("The player is victorious and gets free COBB products and tuning for LIFE!!!");
                reset();
            }
            else if(computer.checkIfPlayerHasWon(playerGrid._gridShips))
            {
                MessageBox.Show("The computer is victorious and you will spin a bearing on the way home.  Sucks for you.");
                reset();
            }
        }   

        private void btnStartOver_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void reset()
        {
            human = new Player();
            computer = new Player();
            playerGrid = new PlayerGrid(PlayerGrid);
            computerGrid = new ComputerGrid(CompGrid);
            playerGrid.InitializeGrid();
            computerGrid.InitializeGrid();
        }
    }
}
