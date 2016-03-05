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
            initializeCheckerBoard();
            backColor = label33;
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
                int col = this.tableLayoutPanel1.GetColumn(clickedLabel);
                int row = this.tableLayoutPanel1.GetRow(clickedLabel);

                if (canMove != null) canMove.Clear();
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
                

                if (player1Turn)
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        for (int j = 0; j <= 7; j++)
                        {
                            Label temp = (Label)tableLayoutPanel1.GetControlFromPosition(i, j);
                            if (temp != null && temp.ForeColor == Color.Black && canJumpAgainPlayer1(temp) && board[i, j].player1Checker)
                                canMove.Add(temp);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        for (int j = 0; j <= 7; j++)
                        {
                            Label temp = (Label)tableLayoutPanel1.GetControlFromPosition(i, j);
                            if (temp != null && temp.ForeColor == Color.Black && canJumpAgainPlayer2(temp) && !board[i, j].player1Checker && board[i,j].isOccupied)
                                canMove.Add(temp);
                        }
                    }
                }

                if (canMove.Count() != 0 && player1Turn && firstClicked == null)
                {
                    if (canMove.Contains(clickedLabel))
                    {
                        firstClicked = clickedLabel;
                        if (highlighting) highlightMovesRed(clickedLabel);
                        firstClicked.BackColor = Color.GreenYellow;
                        board[col, row].isOccupied = false;
                        mustJump = true;
                    }
                    
                }
                else if (canMove.Count() != 0 && !player1Turn && firstClicked == null)
                {
                    if (canMove.Contains(clickedLabel))
                    {
                        firstClicked = clickedLabel;
                        if (highlighting) highlightMovesGreen(clickedLabel);
                        firstClicked.BackColor = Color.GreenYellow;
                        board[col, row].isOccupied = false;
                        mustJump = true;
                    }
                }
                else if (firstClicked == clickedLabel && !secondJumpAvailable)
                {
                    firstClicked.BackColor = backColor.BackColor;
                    board[col, row].isOccupied = true;

                    if (highlighting)
                        if (board[col, row].player1Checker == true)
                        {
                            unHighlightMovesRed(firstClicked);
                        }
                        else
                            unHighlightMovesGreen(firstClicked);

                    firstClicked = null;
                    secondClicked = null;
                }
                else if (mustJump && player1Turn)
                {
                    if (canJumpPlayer1(firstClicked, clickedLabel))
                    {
                        bool pieceMoved = false;

                        if (player1Turn == true && player1CanMove(firstClicked, clickedLabel))
                        {
                            int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                            int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                            updateChecker(clickedLabel, firstClicked);

                            board[col, row].isOccupied = true;
                            board[col, row].player1Checker = true;
                            if (board[col2, row2].pieceIsKing) board[col, row].pieceIsKing = true;
                            else board[col, row].pieceIsKing = false;

                            board[col2, row2].isOccupied = false;
                            board[col2, row2].player1Checker = false;
                            board[col2, row2].pieceIsKing = false;

                            if (highlighting) unHighlightMovesRed(firstClicked);
                            if (pieceJumped && canJumpAgainPlayer1(clickedLabel))
                            {
                                firstClicked.Image = null;
                                firstClicked.BackColor = backColor.BackColor;
                                clickedLabel.BackColor = Color.GreenYellow;
                               
                                firstClicked = clickedLabel;
                                secondClicked = null;
                                
                            }
                            else
                            {
                                player1Turn = false;
                                pieceMoved = true;
                            }
                        }
                        if (pieceMoved == true)
                        {
                            firstClicked.Image = null;

                            firstClicked.BackColor = backColor.BackColor;

                            firstClicked = null;
                            secondClicked = null;
                        }
                        mustJump = false;
                    }
                }
                else if (mustJump && !player1Turn)
                {
                    if (canJumpPlayer2(firstClicked, clickedLabel))
                    {
                        bool pieceMoved = false;

                        if (player1Turn == false && player2CanMove(firstClicked, clickedLabel))
                        {
                            int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                            int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                            updateChecker(clickedLabel, firstClicked);
                            board[col, row].isOccupied = true;
                            board[col, row].player1Checker = false;
                            if(board[col2,row2].pieceIsKing) board[col,row].pieceIsKing = true;
                            else board[col, row].pieceIsKing = false;

                            board[col2, row2].isOccupied = false;
                            board[col2, row2].player1Checker = false;
                            board[col2, row2].pieceIsKing = false;

                            if (highlighting) unHighlightMovesRed(firstClicked);
                            if (pieceJumped && canJumpAgainPlayer2(clickedLabel))
                            {
                                firstClicked.Image = null;
                                firstClicked.BackColor = backColor.BackColor;
                                clickedLabel.BackColor = Color.GreenYellow;

                                firstClicked = clickedLabel;
                                secondClicked = null;
                            }
                            else
                            {
                                player1Turn = true;
                                pieceMoved = true;
                            }
                        }
                        if (pieceMoved == true)
                        {
                            firstClicked.Image = null;

                            firstClicked.BackColor = backColor.BackColor;

                            firstClicked = null;
                            secondClicked = null;
                        }
                        mustJump = false;
                    }
                }
                // If fist clicked is null and current clicked isn't empty and isn't green
                // set first clicked to this square and highlight this square using appropriate 
                // checker(Red) on highlighted square
                else if (firstClicked == null && board[col, row].isOccupied == true && board[col, row].player1Checker == true && player1Turn == true)
                {
                    firstClicked = clickedLabel;
                    if (highlighting) highlightMovesRed(clickedLabel);
                    firstClicked.BackColor = Color.GreenYellow;
                    board[col, row].isOccupied = false;
                }
                // this is the same as the if loop, except it is for the condition of a green checker
                // instead of a red checker. Then sets the first clicked square to a 
                // green checker on a highlighted square
                else if (firstClicked == null && board[col, row].isOccupied == true && board[col, row].player1Checker == false && player1Turn == false)
                {
                    firstClicked = clickedLabel;
                    if (highlighting) highlightMovesGreen(clickedLabel);
                    firstClicked.BackColor = Color.GreenYellow;
                    board[col, row].isOccupied = false;
                }
                
                // First clicked is NOT null, there for a checker to be moved has already been selected
                // and it's location is highlighted, AND the destination square is empty(forecolor.black)
                else if (firstClicked != null && board[col, row].isOccupied == false)
                {
                    bool pieceMoved = false;
                    pieceJumped = false;
                    secondClicked = clickedLabel;


                    if (player1Turn == true && player1CanMove(firstClicked, secondClicked))
                    {
                        int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                        int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                        updateChecker(clickedLabel, firstClicked);
                        board[col, row].isOccupied = true;
                        board[col, row].player1Checker = true;
                        if(board[col2,row2].pieceIsKing ) board[col, row].pieceIsKing = true;
                        
                        board[col2, row2].isOccupied = false;
                        board[col2, row2].player1Checker = false;
                        board[col2, row2].pieceIsKing = false;

                        if (highlighting) unHighlightMovesRed(firstClicked);
                        if (pieceJumped && canJumpAgainPlayer1(secondClicked))
                        {
                            firstClicked.Image = null;
                            firstClicked.BackColor = backColor.BackColor;
                            clickedLabel.BackColor = Color.GreenYellow;

                            firstClicked = clickedLabel;
                            secondClicked = null;
                        }
                        else
                        {
                            player1Turn = false;
                            pieceMoved = true;
                        }
                    }
                    if (player1Turn == false && player2CanMove(firstClicked, secondClicked))
                    {
                        int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                        int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                        updateChecker(clickedLabel, firstClicked);
                        board[col, row].isOccupied = true;
                        board[col, row].player1Checker = false;
                        if (board[col2, row2].pieceIsKing) board[col, row].pieceIsKing = true;
                        
                        board[col2, row2].isOccupied = false;
                        board[col2, row2].player1Checker = false;
                        board[col2, row2].pieceIsKing = false;

                        if (highlighting) unHighlightMovesGreen(firstClicked);
                        if (pieceJumped && canJumpAgainPlayer2(secondClicked))
                        {
                            firstClicked.Image = null;
                            firstClicked.BackColor = backColor.BackColor;
                            clickedLabel.BackColor = Color.GreenYellow;

                            firstClicked = clickedLabel;
                            secondClicked = null;
                        }
                        else
                        {
                            player1Turn = true;
                            pieceMoved = true;
                        }
                    }
                    // if a piece was moved rest first/second clicked for next move
                    // also reset fistClicked image and backcolor
                    if (pieceMoved == true)
                    {
                        firstClicked.Image = null;

                        firstClicked.BackColor = backColor.BackColor;

                        firstClicked = null;
                        secondClicked = null;
                    }
                    // if no move was made, reset second clicked to test next move
                    else
                    {
                        secondClicked = null;
                    }
                    
                }

                for (int i = 1; i <= 7; i += 2)
                {
                    if (board[i, 0].isOccupied && board[i, 0].player1Checker)
                    {
                        kingPiece("player1", i, 0);
                    }
                }
                
                for (int i = 0; i <= 7; i += 2)
                {
                    if (board[i, 7].isOccupied && !board[i, 7].player1Checker)
                    {
                        kingPiece("player2", i, 7);
                    }
                }
            }

            
        }
    }
}
