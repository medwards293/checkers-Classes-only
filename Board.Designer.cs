else if (mustJump && player1Turn)
                {
                    if (canJumpPlayer1(firstClicked, clickedLabel))
                    {
                        bool pieceMoved = false;

                        if (player1Turn == true && player1CanMove(firstClicked, clickedLabel))
                        {
                            clickedLabel.Image = player1Checker;
                            board[col, row].isOccupied = true;
                            board[col, row].player1Checker = true;
                            int col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                            int row2 = this.tableLayoutPanel1.GetRow(firstClicked);
                            board[col2, row2].isOccupied = false;
                            board[col2, row2].player1Checker = false;
                            if (highlighting) unHighlightMovesRed(firstClicked);
                            if (pieceJumped && canJumpAgainPlayer1(clickedLabel))
                            {
                                firstClicked.Image = null;
                                firstClicked.BackColor = backColor.BackColor;
                                clickedLabel.BackColor = Color.GreenYellow;
                                col2 = this.tableLayoutPanel1.GetColumn(firstClicked);
                                row2 = this.tableLayoutPanel1.GetRow(firstClicked);
                                board[col2, row2].isOccupied = false;
                                board[col2, row2].player1Checker = false;
                                board[col, row].isOccupied = false;
                                board[col, row].player1Checker = false;

                                firstClicked = clickedLabel;
                                
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
