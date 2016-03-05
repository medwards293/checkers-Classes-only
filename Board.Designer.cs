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
    partial class Board
    {
        Label backColor;
        List<Label> canMove = new List<Label>();

        struct checkerBoard
        {
            public bool isOccupied;
            public bool player1Checker;
            public bool pieceIsKing;
        }

        bool player1Turn = true;
        bool highlighting = false;
        int checker1Count = 12;
        int checker2Count = 12;
        bool secondJumpAvailable = false;
        bool pieceJumped = false;
        bool mustJump = false;

        System.Drawing.Bitmap player1Checker = Checkers.Properties.Resources.checkerRed;
        System.Drawing.Bitmap player2Checker = Checkers.Properties.Resources.checkerGreen;

        checkerBoard[,] board = new checkerBoard[8,8];

        bool canJumpPlayer1(Label from, Label to)
        {
            bool jumpAvailable = false;
            int fromCol = this.tableLayoutPanel1.GetColumn(from);
            int fromRow = this.tableLayoutPanel1.GetRow(from);

            int toCol = this.tableLayoutPanel1.GetColumn(to);
            int toRow = this.tableLayoutPanel1.GetRow(to);

            if (board[fromCol, fromRow].pieceIsKing == false && toRow >= 0 && toCol >= 0 && toCol <= 7)
            {

                if (!board[toCol, toRow].isOccupied && fromRow - 2 == toRow)
                {
                    jumpAvailable = true;                    
                }
                
            }

            return jumpAvailable;
        }

        bool canJumpPlayer2(Label from, Label to)
        {
            bool jumpAvailable = false;
            int fromCol = this.tableLayoutPanel1.GetColumn(from);
            int fromRow = this.tableLayoutPanel1.GetRow(from);

            int toCol = this.tableLayoutPanel1.GetColumn(to);
            int toRow = this.tableLayoutPanel1.GetRow(to);

            if (board[fromCol, fromRow].pieceIsKing == false && toRow >= 0 && toCol >= 0 && toCol <= 7)
            {

                if (!board[toCol, toRow].isOccupied && fromRow + 2 == toRow)
                {
                    jumpAvailable = true;
                }

            }

            return jumpAvailable;
        }
        bool canJumpAgainPlayer1(Label clicked)
        {
            secondJumpAvailable = false;
            int fromCol = this.tableLayoutPanel1.GetColumn(clicked);
            int fromRow = this.tableLayoutPanel1.GetRow(clicked);

            int toLeftCol = fromCol - 1;
            int toRightCol = fromCol + 1;
            int toRow = fromRow - 1;

            

            if (board[fromCol, fromRow].pieceIsKing == false && toRow >= 0 && fromCol >= 0 && fromCol <= 7)
            {

                if (toRow >= 0 && toLeftCol >= 0 && board[toLeftCol, toRow].isOccupied && board[toLeftCol, toRow].player1Checker == false)
                {
                    if (toRow >= 1 && toLeftCol >= 1 && board[toLeftCol -1, toRow -1].isOccupied == false)
                    {
                        secondJumpAvailable = true;
                    }
                }
                if (toRow >= 0 && toRightCol <= 7 && board[toRightCol, toRow].isOccupied && board[toRightCol, toRow].player1Checker == false)
                {
                    if (toRow >= 1 && toRightCol <= 6 && board[toRightCol+1,toRow-1].isOccupied == false)
                    {
                        secondJumpAvailable = true;
                    }
                }
            }

            return secondJumpAvailable;
        }

        bool canJumpAgainPlayer2(Label clicked)
        {
            secondJumpAvailable = false;
            int fromCol = this.tableLayoutPanel1.GetColumn(clicked);
            int fromRow = this.tableLayoutPanel1.GetRow(clicked);

            int toLeftCol = fromCol - 1;
            int toRightCol = fromCol + 1;
            int toRow = fromRow + 1;



            if (board[fromCol, fromRow].pieceIsKing == false && toRow >= 0 && fromCol >= 0 && fromCol <= 7)
            {

                if (toRow <= 7 && toLeftCol >= 0 && board[toLeftCol, toRow].isOccupied && board[toLeftCol, toRow].player1Checker == true)
                {
                    if (toRow < 7 && toLeftCol >= 1 && board[toLeftCol - 1, toRow + 1].isOccupied == false)
                    {
                        secondJumpAvailable = true;
                    }
                }
                if (toRow < 7 && toRightCol <= 7 && board[toRightCol, toRow].isOccupied && board[toRightCol, toRow].player1Checker == true)
                {
                    if (toRow >= 1 && toRightCol <= 6 && board[toRightCol + 1, toRow + 1].isOccupied == false)
                    {
                        secondJumpAvailable = true;
                    }
                }
            }

            return secondJumpAvailable;
        }

        public void setPlayer1Checker(System.Drawing.Bitmap checker1)
        {

            player1Checker = checker1;
            
            Label temp;
            int i, j;
            for (i = 0; i < 8; i++)
                for (j = 0; j < 8; j++)
                    if(board[i,j].isOccupied && board[i,j].player1Checker)
                    {
                        temp = (Label)tableLayoutPanel1.GetControlFromPosition(i,j);
                        temp.Image = checker1;
                    }
                    

        }

        public void setPlayer2Checker(System.Drawing.Bitmap checker2)
        {
            player2Checker = checker2;

            Label temp;
            int i, j;
            for (i = 0; i < 8; i++)
                for (j = 0; j < 8; j++)
                    if (board[i, j].isOccupied && !board[i, j].player1Checker)
                    {
                        temp = (Label)tableLayoutPanel1.GetControlFromPosition(i, j);
                        temp.Image = checker2;
                    }
            
        }
        bool player2CanMove(Label from, Label to)
        {
            bool canMove = false;

            int fromCol = this.tableLayoutPanel1.GetColumn(from);
            int fromRow = this.tableLayoutPanel1.GetRow(from);

            int toCol = this.tableLayoutPanel1.GetColumn(to);
            int toRow = this.tableLayoutPanel1.GetRow(to);

            if (board[fromCol, fromRow].pieceIsKing == false)
            {

                if (toCol >= 0 && toCol <= 7 && board[toCol, toRow].isOccupied == false)
                    if ((fromCol - 1 == toCol || fromCol + 1 == toCol) && fromRow + 1 == toRow)
                    {
                        canMove = true;
                    }
                    else if ((fromCol - 2 == toCol || fromCol + 2 == toCol) && fromRow + 2 == toRow)
                        if (toCol == fromCol - 2)
                        {
                            if (board[fromCol - 1, fromRow + 1].player1Checker == true && board[fromCol - 1, fromRow + 1].isOccupied == true)
                            {
                                jumpPiece(fromCol - 1, fromRow + 1);
                                canMove = true;
                            }
                        }
                        else if (toCol == fromCol + 2)
                            if (board[fromCol + 1, fromRow + 1].player1Checker == true && board[fromCol + 1, fromRow + 1].isOccupied == true)
                            {
                                jumpPiece(fromCol + 1, fromRow + 1);
                                canMove = true;
                            }

            }
            return canMove;
        }
        bool player1CanMove(Label from, Label to)
        {
            bool canMove = false;
            
            int fromCol = this.tableLayoutPanel1.GetColumn(from);
            int fromRow = this.tableLayoutPanel1.GetRow(from);

            int toCol = this.tableLayoutPanel1.GetColumn(to);
            int toRow = this.tableLayoutPanel1.GetRow(to);

            if (board[fromCol, fromRow].pieceIsKing == false)
            {

                if (toCol >= 0 && toCol <= 7 && board[toCol, toRow].isOccupied == false)
                    if((fromCol - 1 == toCol || fromCol + 1 == toCol ) && fromRow -1 == toRow)
                    {
                        canMove = true;
                    }
                    else if ((fromCol -2 == toCol || fromCol + 2 == toCol) && fromRow -2 == toRow)
                        if (toCol == fromCol - 2)
                        {
                            if (board[fromCol - 1, fromRow - 1].player1Checker == false && board[fromCol - 1, fromRow - 1].isOccupied == true)
                            {
                                jumpPiece(fromCol - 1, fromRow - 1);
                                canMove = true;
                            }
                        }
                        else if (toCol == fromCol + 2)
                            if (board[fromCol + 1, fromRow - 1].player1Checker == false && board[fromCol + 1, fromRow - 1].isOccupied == true)
                            {
                                jumpPiece(fromCol + 1, fromRow - 1);
                                canMove = true;
                            }
                
            }
            return canMove;
        }

        void jumpPiece(int col, int row)
        {
            Label temp = (Label)tableLayoutPanel1.GetControlFromPosition(col, row);
            temp.Image = null;
            board[col,row].isOccupied = false;
            board[col, row].player1Checker = false;
            board[col, row].pieceIsKing = false;
            pieceJumped = true;
            
            if (player1Turn)
                checker2Count--;
            else
                checker1Count--;

            if (checker2Count == 0)
                Form1.getLeaderboardName().winnerDeclared(1);
            else if(checker1Count == 1)
                Form1.getLeaderboardName().winnerDeclared(2);

        }
        public void setHighlighting()
        {
            if (highlighting == false)
                highlighting = true;
            else highlighting = false;
        }
        void initializeCheckerBoard()
        {

            int i = 1;
            int j = 0;
            //initialize player 2's checkers
            while (i <= 7)
            {
                board[i, j].isOccupied = true;
                board[i, j].player1Checker = false;
                board[i, j].pieceIsKing = false;
                i += 2;                    
            }
            i = 0;
            j = 1;
            while (i <= 7)
            {
                board[i, j].isOccupied = true;
                board[i, j].player1Checker = false;
                board[i, j].pieceIsKing = false;
                i += 2;
            }
            i = 1;
            j = 2;
            while (i <= 7)
            {
                board[i, j].isOccupied = true;
                board[i, j].player1Checker = false;
                board[i, j].pieceIsKing = false;
                i += 2;
            }
            // initialize player 1's checkers
            i = 0;
            j = 5;
            while (i <= 7)
            {
                board[i, j].isOccupied = true;
                board[i, j].player1Checker = true;
                board[i, j].pieceIsKing = false;
                i += 2;
            }
            i = 1;
            j = 6;
            while (i <= 7)
            {
                board[i, j].isOccupied = true;
                board[i, j].player1Checker = true;
                board[i, j].pieceIsKing = false;
                i += 2;
            }
            i = 0;
            j = 7;
            while (i <= 7)
            {
                board[i, j].isOccupied = true;
                board[i, j].player1Checker = true;
                board[i, j].pieceIsKing = false;
                i += 2;
            }
        }
        
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void setBackColor(string newColor)
        {
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromName(newColor);
        }

        public string getBackColor()
        {
            return this.tableLayoutPanel1.BackColor.ToString();

        }

        public void setForeColor(string newColor)
        {

            this.label1.BackColor = System.Drawing.Color.FromName(newColor);
            this.label2.BackColor = System.Drawing.Color.FromName(newColor);
            this.label3.BackColor = System.Drawing.Color.FromName(newColor);
            this.label4.BackColor = System.Drawing.Color.FromName(newColor);
            this.label5.BackColor = System.Drawing.Color.FromName(newColor);
            this.label6.BackColor = System.Drawing.Color.FromName(newColor);
            this.label7.BackColor = System.Drawing.Color.FromName(newColor);
            this.label8.BackColor = System.Drawing.Color.FromName(newColor);
            this.label9.BackColor = System.Drawing.Color.FromName(newColor);
            this.label10.BackColor = System.Drawing.Color.FromName(newColor);
            this.label11.BackColor = System.Drawing.Color.FromName(newColor);
            this.label12.BackColor = System.Drawing.Color.FromName(newColor);
            this.label13.BackColor = System.Drawing.Color.FromName(newColor);
            this.label14.BackColor = System.Drawing.Color.FromName(newColor);
            this.label15.BackColor = System.Drawing.Color.FromName(newColor);
            this.label16.BackColor = System.Drawing.Color.FromName(newColor);
            this.label17.BackColor = System.Drawing.Color.FromName(newColor);
            this.label18.BackColor = System.Drawing.Color.FromName(newColor);
            this.label19.BackColor = System.Drawing.Color.FromName(newColor);
            this.label20.BackColor = System.Drawing.Color.FromName(newColor);
            this.label21.BackColor = System.Drawing.Color.FromName(newColor);
            this.label22.BackColor = System.Drawing.Color.FromName(newColor);
            this.label23.BackColor = System.Drawing.Color.FromName(newColor);
            this.label24.BackColor = System.Drawing.Color.FromName(newColor);
            this.label25.BackColor = System.Drawing.Color.FromName(newColor);
            this.label26.BackColor = System.Drawing.Color.FromName(newColor);
            this.label27.BackColor = System.Drawing.Color.FromName(newColor);
            this.label28.BackColor = System.Drawing.Color.FromName(newColor);
            this.label29.BackColor = System.Drawing.Color.FromName(newColor);
            this.label30.BackColor = System.Drawing.Color.FromName(newColor);
            this.label31.BackColor = System.Drawing.Color.FromName(newColor);
            this.label32.BackColor = System.Drawing.Color.FromName(newColor);
            this.label33.BackColor = System.Drawing.Color.FromName(newColor);
        }

        public string getForeColor()
        {
            return this.label1.BackColor.ToString();
        }
               
        public void highlightMovesRed(Label clickedLabel)
        {
            Label temp = clickedLabel;
            backColor = label33;
            
            int col = this.tableLayoutPanel1.GetColumn(clickedLabel);
            int row = this.tableLayoutPanel1.GetRow(clickedLabel);

            int leftCol = col - 1;
            int rightCol = col + 1;
            int forwardRow = row - 1;

            if (leftCol >= 0)
            {
                temp = (Label)tableLayoutPanel1.GetControlFromPosition(leftCol, forwardRow);
                temp.BackColor = Color.GreenYellow;
            }

            if (rightCol <= 7)
            {
                temp = (Label)tableLayoutPanel1.GetControlFromPosition(rightCol, forwardRow);
                temp.BackColor = Color.GreenYellow;
            }

        }

        public void unHighlightMovesRed(Label label)
        {
            Label temp = label;

            int col = this.tableLayoutPanel1.GetColumn(label);
            int row = this.tableLayoutPanel1.GetRow(label);

            int leftCol = col - 1;
            int rightCol = col + 1;
            int forwardRow = row - 1;

            if (leftCol >= 0)
            {

                temp = (Label)tableLayoutPanel1.GetControlFromPosition(leftCol, forwardRow);
                temp.BackColor = backColor.BackColor;
            }

            if (rightCol <= 7)
            {
                temp = (Label)tableLayoutPanel1.GetControlFromPosition(rightCol, forwardRow);
                temp.BackColor = backColor.BackColor;
            }
        }

        public void highlightMovesGreen(Label clickedLabel)
        {
            Label temp = clickedLabel;
            backColor = label33;

            int col = this.tableLayoutPanel1.GetColumn(clickedLabel);
            int row = this.tableLayoutPanel1.GetRow(clickedLabel);

            int leftCol = col - 1;
            int rightCol = col + 1;
            int rearRow = row + 1;

            if (leftCol >= 0)
            {
                temp = (Label)tableLayoutPanel1.GetControlFromPosition(leftCol, rearRow);
                temp.BackColor = Color.GreenYellow;
            }

            if (rightCol <= 7)
            {
                temp = (Label)tableLayoutPanel1.GetControlFromPosition(rightCol, rearRow);
                temp.BackColor = Color.GreenYellow;
            }

        }
        public void unHighlightMovesGreen(Label label)
        {
            Label temp = label;

            int col = this.tableLayoutPanel1.GetColumn(label);
            int row = this.tableLayoutPanel1.GetRow(label);

            int leftCol = col - 1;
            int rightCol = col + 1;
            int rearRow = row + 1;

            if (leftCol >= 0)
            {

                temp = (Label)tableLayoutPanel1.GetControlFromPosition(leftCol, rearRow);
                temp.BackColor = backColor.BackColor;
            }

            if (rightCol <= 7)
            {
                temp = (Label)tableLayoutPanel1.GetControlFromPosition(rightCol, rearRow);
                temp.BackColor = backColor.BackColor;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Red;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label12, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label14, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label15, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label16, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.label17, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label18, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label19, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.label20, 7, 4);
            this.tableLayoutPanel1.Controls.Add(this.label21, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label22, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label23, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.label24, 6, 5);
            this.tableLayoutPanel1.Controls.Add(this.label25, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label26, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.label27, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.label28, 7, 6);
            this.tableLayoutPanel1.Controls.Add(this.label29, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label30, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.label31, 4, 7);
            this.tableLayoutPanel1.Controls.Add(this.label32, 6, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Red;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.17391F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.6087F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 462);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label1.Location = new System.Drawing.Point(62, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 55);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label2.Location = new System.Drawing.Point(182, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 55);
            this.label2.TabIndex = 1;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label3.Location = new System.Drawing.Point(302, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 55);
            this.label3.TabIndex = 2;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label4.Location = new System.Drawing.Point(422, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 55);
            this.label4.TabIndex = 3;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Click += new System.EventHandler(this.label_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label5.Location = new System.Drawing.Point(2, 59);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 54);
            this.label5.TabIndex = 4;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.label_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label6.Location = new System.Drawing.Point(122, 59);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 54);
            this.label6.TabIndex = 5;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.label_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label7.Location = new System.Drawing.Point(242, 59);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 54);
            this.label7.TabIndex = 6;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Click += new System.EventHandler(this.label_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label8.Location = new System.Drawing.Point(362, 59);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 54);
            this.label8.TabIndex = 7;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.Click += new System.EventHandler(this.label_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label9.Location = new System.Drawing.Point(62, 115);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 56);
            this.label9.TabIndex = 8;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label9.Click += new System.EventHandler(this.label_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label10.Location = new System.Drawing.Point(182, 115);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 56);
            this.label10.TabIndex = 9;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label10.Click += new System.EventHandler(this.label_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label11.Location = new System.Drawing.Point(302, 115);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 56);
            this.label11.TabIndex = 10;
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Click += new System.EventHandler(this.label_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Image = global::Checkers.Properties.Resources.checkerGreen;
            this.label12.Location = new System.Drawing.Point(422, 115);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 56);
            this.label12.TabIndex = 11;
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label12.Click += new System.EventHandler(this.label_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Black;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(2, 173);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 55);
            this.label13.TabIndex = 12;
            this.label13.Click += new System.EventHandler(this.label_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Black;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(122, 173);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 55);
            this.label14.TabIndex = 13;
            this.label14.Click += new System.EventHandler(this.label_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Black;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(242, 173);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 55);
            this.label15.TabIndex = 14;
            this.label15.Click += new System.EventHandler(this.label_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Black;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(362, 173);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 55);
            this.label16.TabIndex = 15;
            this.label16.Click += new System.EventHandler(this.label_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Black;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(62, 230);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 55);
            this.label17.TabIndex = 16;
            this.label17.Click += new System.EventHandler(this.label_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Black;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(182, 230);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 55);
            this.label18.TabIndex = 17;
            this.label18.Click += new System.EventHandler(this.label_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Black;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(302, 230);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 55);
            this.label19.TabIndex = 18;
            this.label19.Click += new System.EventHandler(this.label_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Black;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(422, 230);
            this.label20.Margin = new System.Windows.Forms.Padding(0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 55);
            this.label20.TabIndex = 19;
            this.label20.Click += new System.EventHandler(this.label_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Black;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label21.Location = new System.Drawing.Point(2, 287);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(58, 55);
            this.label21.TabIndex = 20;
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label21.Click += new System.EventHandler(this.label_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Black;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label22.Location = new System.Drawing.Point(122, 287);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 55);
            this.label22.TabIndex = 21;
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label22.Click += new System.EventHandler(this.label_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Black;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label23.Location = new System.Drawing.Point(242, 287);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 55);
            this.label23.TabIndex = 22;
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label23.Click += new System.EventHandler(this.label_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Black;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label24.Location = new System.Drawing.Point(362, 287);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(58, 55);
            this.label24.TabIndex = 23;
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label24.Click += new System.EventHandler(this.label_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Black;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label25.Location = new System.Drawing.Point(62, 344);
            this.label25.Margin = new System.Windows.Forms.Padding(0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(58, 55);
            this.label25.TabIndex = 24;
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label25.Click += new System.EventHandler(this.label_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Black;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label26.Location = new System.Drawing.Point(182, 344);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(58, 55);
            this.label26.TabIndex = 25;
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label26.Click += new System.EventHandler(this.label_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Black;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label27.Location = new System.Drawing.Point(302, 344);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(58, 55);
            this.label27.TabIndex = 26;
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label27.Click += new System.EventHandler(this.label_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Black;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label28.Location = new System.Drawing.Point(422, 344);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(60, 55);
            this.label28.TabIndex = 27;
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label28.Click += new System.EventHandler(this.label_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Black;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label29.Location = new System.Drawing.Point(2, 401);
            this.label29.Margin = new System.Windows.Forms.Padding(0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(58, 59);
            this.label29.TabIndex = 28;
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label29.Click += new System.EventHandler(this.label_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Black;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label30.Location = new System.Drawing.Point(122, 401);
            this.label30.Margin = new System.Windows.Forms.Padding(0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(58, 59);
            this.label30.TabIndex = 29;
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label30.Click += new System.EventHandler(this.label_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Black;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label31.Location = new System.Drawing.Point(242, 401);
            this.label31.Margin = new System.Windows.Forms.Padding(0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(58, 59);
            this.label31.TabIndex = 30;
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label31.Click += new System.EventHandler(this.label_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Black;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Image = global::Checkers.Properties.Resources.checkerRed;
            this.label32.Location = new System.Drawing.Point(362, 401);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(58, 59);
            this.label32.TabIndex = 31;
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label32.Click += new System.EventHandler(this.label_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Black;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.ForeColor = System.Drawing.Color.Green;
            this.label33.Location = new System.Drawing.Point(182, 2);
            this.label33.Margin = new System.Windows.Forms.Padding(0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(58, 55);
            this.label33.TabIndex = 1;
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label33.Click += new System.EventHandler(this.label_Click);
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Board";
            this.Text = "Board";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
    }
}
