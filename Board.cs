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
            opponentIsAI = (Form1.getNewGameWindowName()).getOpponentIsAI();
        }
        


        // firstClicked points to the first Label control 
        // that the player clicks, but it will be null 
        // if the player hasn't clicked a squre yet
        Label firstClicked = null;

        // secondClicked points to the second Label control 
        // that the player clicks
        Label secondClicked = null;
        
                   
        
        
        private void label_Click(object sender, EventArgs e)
        {

           

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // gets the col/row of the currently clicked label
                int col = this.tableLayoutPanel1.GetColumn(clickedLabel);
                int row = this.tableLayoutPanel1.GetRow(clickedLabel);

                //clears canJump list that holds list of pieces that must jump
                if (canJump != null) canJump.Clear();
                


                       
                // goes through all squares and finds pieces that have to make a jump
                if (player1Turn)
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        for (int j = 0; j <= 7; j++)
                        {
                            Label temp = (Label)tableLayoutPanel1.GetControlFromPosition(i, j);
                            if (temp != null && temp.ForeColor == Color.Black && canJumpAgainPlayer1(temp) && board[i, j].player1Checker)
                                canJump.Add(temp);
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
                                canJump.Add(temp);
                        }
                    }
                }

                // if a pieces has to make a jump then the first click must be one of the square in the canJump list
                if (canJump.Count() != 0 && player1Turn && firstClicked == null)
                {
                    if (canJump.Contains(clickedLabel))
                    {
                        firstClicked = clickedLabel;
                        if (highlighting) highlightMovesRed(clickedLabel);
                        firstClicked.BackColor = Color.GreenYellow;
                        board[col, row].isOccupied = false;
                        mustJump = true;
                    }
                    
                }
                // player 2 version of looking in canJump list to ensure player chooses a piece that must jump
                else if (canJump.Count() != 0 && !player1Turn && firstClicked == null)
                {
                    if (canJump.Contains(clickedLabel))
                    {
                        firstClicked = clickedLabel;
                        if (highlighting) highlightMovesGreen(clickedLabel);
                        firstClicked.BackColor = Color.GreenYellow;
                        board[col, row].isOccupied = false;
                        mustJump = true;
                    }
                }
                // allows to deselect a piece as long as there isn't a second jump available
                // all jumps must be taken that can be
                else if (firstClicked == clickedLabel && !consecutiveJumpAvailable)
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
                // once a square has been selected from the must jump list
                // this is the jump logic for player 1
                else if (mustJump && player1Turn)
                {
                    // calls function to ensure that the player can jump to the destination clicked label
                    if (canJumpPlayer1(firstClicked, clickedLabel))
                    {
                        bool pieceMoved = false;
                        // checks to see if player1CanMove from firstClicked label to destination label
                        // jump piece is called in player1CanMove()
                        if (player1Turn == true && player1CanMove(firstClicked, clickedLabel))
                        {
                            // col and row from top of code holds for current clicked
                            // col2 and row2 holds location for firstClicked
                            int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                            int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                            //update checker moves the piece and makes sure it's king or regular
                            updateChecker(clickedLabel, firstClicked);

                            // moves piece on board
                            // updates boardArrayofStructs new location
                            board[col, row].isOccupied = true;
                            board[col, row].player1Checker = true;
                            if (board[col2, row2].pieceIsKing) board[col, row].pieceIsKing = true;
                            else board[col, row].pieceIsKing = false;

                            //updates boardArrayofStructs old location
                            board[col2, row2].isOccupied = false;
                            board[col2, row2].player1Checker = false;
                            board[col2, row2].pieceIsKing = false;

                            // if the piece can jump again it stays player 1's turn and the new square is highlighted
                            if (highlighting) unHighlightMovesRed(firstClicked);
                            if (pieceJumped && canJumpAgainPlayer1(clickedLabel))
                            {
                                firstClicked.Image = null;
                                firstClicked.BackColor = backColor.BackColor;
                                clickedLabel.BackColor = Color.GreenYellow;
                                consecutiveJumpAvailable = true;
                                firstClicked = clickedLabel;
                                secondClicked = null;
                                
                            }
                            else
                            {
                                player1Turn = false;
                                consecutiveJumpAvailable = false;
                                pieceMoved = true;
                            }
                        }
                        // if a piece is moved and there are no more jumps clicks are reset and it is player2's turn
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
                // once a square has been selected from the must jump list
                // this is the jump logic for player 2
                else if (mustJump && !player1Turn)
                {
                    //calls canJump for player 2 to determine if checker can jump from first clicked to second clicked
                    if (canJumpPlayer2(firstClicked, clickedLabel))
                    {
                        bool pieceMoved = false;
                        // player2canmove is called, jump piece is called from withing player2canmove
                        if (player1Turn == false && player2CanMove(firstClicked, clickedLabel))
                        {
                            // col/row from above is grid location for current clicked label
                            // col2/row2 is grid location for first clickedLabel
                            int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                            int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                            // updates checker to new location with correct piece Image
                            updateChecker(clickedLabel, firstClicked);
                            board[col, row].isOccupied = true;
                            board[col, row].player1Checker = false;
                            if(board[col2,row2].pieceIsKing) board[col,row].pieceIsKing = true;
                            else board[col, row].pieceIsKing = false;

                            // clears old location of checker
                            board[col2, row2].isOccupied = false;
                            board[col2, row2].player1Checker = false;
                            board[col2, row2].pieceIsKing = false;

                            // checks to see if checker can jump again
                            // if not, turn goes to next player
                            if (highlighting) unHighlightMovesRed(firstClicked);
                            if (pieceJumped && canJumpAgainPlayer2(clickedLabel))
                            {
                                firstClicked.Image = null;
                                firstClicked.BackColor = backColor.BackColor;
                                clickedLabel.BackColor = Color.GreenYellow;
                                consecutiveJumpAvailable = true;
                                firstClicked = clickedLabel;
                                secondClicked = null;
                            }
                            else
                            {
                                player1Turn = true;
                                pieceMoved = true;
                                consecutiveJumpAvailable = false;
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
                // If fist clicked is null and current clicked isn't empty and is player1's
                // set first clicked to this square and highlight this square using appropriate 
                // checker
                else if (firstClicked == null && board[col, row].isOccupied == true && board[col, row].player1Checker == true && player1Turn == true)
                {
                    firstClicked = clickedLabel;
                    if (highlighting) highlightMovesRed(clickedLabel);
                    firstClicked.BackColor = Color.GreenYellow;
                    board[col, row].isOccupied = false;
                }
                // this is the same as the if loop, except it is for the condition of a player2's
                // instead of player 1. Then sets the first clicked square to  
                // player 2's highlighted.
                else if (firstClicked == null && board[col, row].isOccupied == true && board[col, row].player1Checker == false && player1Turn == false)
                {
                    firstClicked = clickedLabel;
                    if (highlighting) highlightMovesGreen(clickedLabel);
                    firstClicked.BackColor = Color.GreenYellow;
                    board[col, row].isOccupied = false;
                }
                
                // First clicked is NOT null, there for a checker to be moved has already been selected
                // and it's location is highlighted, AND the destination square is empty  !board[col,row].isOccupied
                else if (firstClicked != null && board[col, row].isOccupied == false)
                {
                    bool pieceMoved = false;
                    pieceJumped = false;
                    secondClicked = clickedLabel;

                    // player1 can move is called and jump is called within
                    if (player1Turn == true && player1CanMove(firstClicked, secondClicked))
                    {
                        // col2/row2 are grid location for first clicked                        
                        int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                        int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                        // update new square with checker image and data
                        updateChecker(clickedLabel, firstClicked);
                        board[col, row].isOccupied = true;
                        board[col, row].player1Checker = true;
                        if(board[col2,row2].pieceIsKing ) board[col, row].pieceIsKing = true;
                        
                        // clears old location of checker information
                        board[col2, row2].isOccupied = false;
                        board[col2, row2].player1Checker = false;
                        board[col2, row2].pieceIsKing = false;

                        // if piece can jump again, new location is highlighted
                        // if not it becomes player2's turn
                        if (highlighting) unHighlightMovesRed(firstClicked);
                        if (pieceJumped && canJumpAgainPlayer1(secondClicked))
                        {
                            firstClicked.Image = null;
                            firstClicked.BackColor = backColor.BackColor;
                            clickedLabel.BackColor = Color.GreenYellow;
                            consecutiveJumpAvailable = true;
                            firstClicked = clickedLabel;
                            secondClicked = null;
                        }
                        else
                        {
                            player1Turn = false;
                            consecutiveJumpAvailable = false;
                            pieceMoved = true;
                        }
                    }
                    // player2canMove is called with jump being called within if a jump is available 
                    if (player1Turn == false && player2CanMove(firstClicked, secondClicked))
                    {
                        // col2/row2 are grid location for first clicked          
                        int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                        int row2 = this.tableLayoutPanel1.GetRow(firstClicked);

                        // update new square with checker image and data
                        updateChecker(clickedLabel, firstClicked);
                        board[col, row].isOccupied = true;
                        board[col, row].player1Checker = false;
                        if (board[col2, row2].pieceIsKing) board[col, row].pieceIsKing = true;

                        // clears old location of checker information
                        board[col2, row2].isOccupied = false;
                        board[col2, row2].player1Checker = false;
                        board[col2, row2].pieceIsKing = false;
                        
                        // if piece can jump again, new location is highlighted
                        // if not it becomes player2's turn
                        if (highlighting) unHighlightMovesGreen(firstClicked);
                        if (pieceJumped && canJumpAgainPlayer2(secondClicked))
                        {
                            firstClicked.Image = null;
                            firstClicked.BackColor = backColor.BackColor;
                            clickedLabel.BackColor = Color.GreenYellow;
                            consecutiveJumpAvailable = true;
                            firstClicked = clickedLabel;
                            secondClicked = null;
                        }
                        else
                        {
                            player1Turn = true;
                            pieceMoved = true;
                            consecutiveJumpAvailable = false;
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

                // checks to see if any pieces reached the other edge of the board and need to be kinged
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

                if (opponentIsAI && !player1Turn)
                {
                    AIMove();                
                }
            }

            
        }
    }
}
