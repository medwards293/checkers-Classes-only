using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Board : Form
    {
        public Board()
        {
            InitializeComponent();
        }


        // firstClicked points to the first Label control 
        // that the player clicks, but it will be null 
        // if the player hasn't clicked a label yet
        Label firstClicked = null;

        // secondClicked points to the second Label control 
        // that the player clicks
        Label secondClicked = null;

        private void label_Click(object sender, EventArgs e)
        {

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click


                // If firstClicked is null, this is the first icon 
                // in the pair that the player clicked,
                // so set firstClicked to the label that the player 
                // clicked.
                // ForeColor is temporarily being used to keep track of who's piece
                // is in what square. 
                // ForeColor.Red = Player 1
                // ForeColor.Green = Player 2
                // ForeColor.Black = empty square

                // If fist clicked is null and current clicked isn't empty and isn't green
                // set first clicked to this square and highlight this square using appropriate 
                // checker(Red) on highlighted square
                if (firstClicked == null && clickedLabel.ForeColor != Color.Black && clickedLabel.ForeColor == Color.Red)
                {
                    firstClicked = clickedLabel;
                    highlightMovesRed(clickedLabel);
                    firstClicked.BackColor = Color.GreenYellow;
                }
                // this is the same as the if loop, except it is for the condition of a green checker
                // instead of a red checker. Then sets the first clicked square to a 
                // green checker on a highlighted square
                else if (firstClicked == null && clickedLabel.ForeColor != Color.Black && clickedLabel.ForeColor == Color.Green)
                {
                    firstClicked = clickedLabel;
                    highlightMovesGreen(clickedLabel);
                    firstClicked.BackColor = Color.GreenYellow;
                }
                else if (firstClicked == clickedLabel)
                {
                    firstClicked.BackColor = Color.Black;
                    if (firstClicked.ForeColor == Color.Red)
                    {
                        unHighlightMovesRed(firstClicked);
                    }
                    else
                        unHighlightMovesGreen(firstClicked);
                    firstClicked = null;
                    secondClicked = null;
                
                }
                // First clicked is NOT null, there for a checker to be moved has already been selected
                // and it's location is highlighted, AND the destination square is empty(forecolor.black)
                else if (firstClicked != null && clickedLabel.ForeColor == Color.Black)
                {
                    secondClicked = clickedLabel;

                    if (firstClicked.ForeColor == Color.Red)
                    {
                        secondClicked.Image = Checkers.Properties.Resources.checkerRed;
                        secondClicked.ForeColor = Color.Red;
                        unHighlightMovesRed(firstClicked);
                    }
                    else
                    {
                        secondClicked.Image = Checkers.Properties.Resources.checkerGreen;
                        secondClicked.ForeColor = Color.Green;
                        unHighlightMovesGreen(firstClicked);
                    }

                    firstClicked.Image = null;
                    firstClicked.ForeColor = Color.Black;

                    firstClicked.BackColor = Color.Black;

                    firstClicked = null;
                    secondClicked = null;

                    return;
                }
            }
        }
    }
}
