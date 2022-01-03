using System;

namespace TIcTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            
            do
            {
                Console.Clear();
                //vars
                string[,] ticTacToeBoard = NewBoard(3);
                int winner = 0;
                
                //loop as long at there are free spots or someone wins
                while (MovesLeft(ticTacToeBoard) > 0 && winner == 0)
                {
                    int x_Coord = 0;
                    int y_Coord = 0;
                    //if player one's turn get turn
                    if (MovesLeft(ticTacToeBoard) % 2 == 1 && winner == 0)
                    {
                        Console.WriteLine($"Moves left: {MovesLeft(ticTacToeBoard)}");
                        DrawBoard(ticTacToeBoard);
                        Console.WriteLine();

                        Console.WriteLine("Player 1");
                        Console.WriteLine("Enter the location you would like to place your marker");
                        x_Coord = limitingRangeInt(0, 2, PromptInt("X Coord: "));
                        y_Coord = limitingRangeInt(0, 2, PromptInt("Y Coord: "));
                        //check valid move
                        if (PlaceMarkerCheck(ticTacToeBoard, x_Coord, y_Coord))
                        {
                            //place move on board
                            PlaceMarker(ticTacToeBoard, "X", x_Coord, y_Coord);
                        }
                        //check if win
                        winner = WinCheck2(ticTacToeBoard);
                    }
                    //if player 2's turn get turn
                    if (MovesLeft(ticTacToeBoard) % 2 == 0 && winner == 0)
                    {
                        Console.WriteLine($"Moves left: {MovesLeft(ticTacToeBoard)}");
                        DrawBoard(ticTacToeBoard);
                        Console.WriteLine();

                        Console.WriteLine("Player 2");
                        Console.WriteLine("Enter the location you would like to place your marker");
                        x_Coord = limitingRangeInt(0, 2, PromptInt("X Coord: "));
                        y_Coord = limitingRangeInt(0, 2, PromptInt("Y Coord: "));
                        //check if valid move
                        if (PlaceMarkerCheck(ticTacToeBoard, x_Coord, y_Coord))
                        {
                            //place move
                            PlaceMarker(ticTacToeBoard, "O", x_Coord, y_Coord);
                        }
                        //check if win
                        winner = WinCheck2(ticTacToeBoard);
                    }
                   
                }
                if (winner == 1)
                {
                    DrawBoard(ticTacToeBoard);
                    Console.WriteLine("Congrats Player 1 you win");
                }
                else if (winner == 2)
                {
                    DrawBoard(ticTacToeBoard);
                    Console.WriteLine("Congrats Player 2 you win");
                }
                else if (winner == 0)
                {
                    Console.WriteLine("Tie");
                }
            } while (PromptYes("Run Again?[Y/N]"));

            

        }//end main
        static string[,] NewBoard(int boardSize)
        {//makes a clear board of any size ^2
            //Declare new array in a square
            string[,] board = new string[boardSize, boardSize];

            //loop through horizontally
            for (int x_Axis = 0; x_Axis < boardSize; x_Axis++)
            {
                //loop through verticly
                for (int y_Axis = 0; y_Axis < boardSize; y_Axis++)
                {
                    board[x_Axis, y_Axis] = "*";
                }//end for y axis
            }//end for x axis
            return board;
        }//END NEW BOARD FUNCTION

        static void DrawBoard(string[,] board)
        {
            //print graph key
            Console.WriteLine("  x\ny");
            Console.Write("  ");

            //get dimension of square board
            double boardSize = Math.Sqrt(board.Length);

            //board header
            for (int columns = 0; columns < (int)boardSize; columns++)
            {
                Console.Write($"{columns}  ");
            }//end for


            Console.WriteLine();
            for (int rows = 0; rows < (int)boardSize; rows++)
            {
                //display y axis of right side of grid
                Console.Write($"{rows} ");

                //display grid 1 row at a time
                for (int x_Axis = 0; x_Axis < (int)boardSize; x_Axis++)
                {
                    Console.Write(board[x_Axis, rows] + "  ");
                }
                Console.WriteLine();
            }//end for
        }//END DRAW BOARD FUNCTION

        static void PlaceMarker(string[,] board, string marker, int x_coord, int y_coord)
        {
            if (board[x_coord, y_coord] == "*")
            {
                board[x_coord, y_coord] = marker;
            }

        }//END PLACEMARKER FUNCTION
        static int WinCheck(string[,] board)
        {
            string winner = "";
            #region rows
            if (board[0,0] == board[1,0] && board[0,0] == board[2,0] && board[0,0] != "*")
            {
                winner = board[0, 0];
            }
            else if (board[1,1] == board[0,1] && board[1,1] == board[2,1] && board[1,1] != "*")
            {
                winner = board[1, 1];
            }
            else if (board[2,2] == board[0,2] && board[2,2] == board[1,2] && board[2,2] != "*")
            {
                winner = board[0, 2];
            }
            #endregion
            #region columns
            if (board[0,0] == board[0,1] && board[0,0] == board[0,2] && board[0,0] != "*")
            {
                winner = board[0, 0];
            }
            else if(board[1,0] == board[1,1] && board[1,0] == board[1,2] && board[1,0] != "*")
            {
                winner = board[1, 0];
            }
            else if (board[2,0] == board[2,1] && board[2,0] == board[2,2] && board[2,0] != "*")
            {
                winner = board[2, 0];
            }
            #endregion
            #region Diagonal
            if (board[0,0] == board[1,1] && board[0,0] == board[2,2] && board[0,0] != "*")
            {
                winner = board[1, 1];
            }
            else if (board[0,2] == board[1,1] && board[0,2] == board[2,0] && board[0,2] != "*")
            {
                winner = board[1, 1];
            }
            switch (winner)
            {
                case "X": return 1;
                case "O": return 2;
                default: return 0;
            }
            #endregion
        }//end win check function
        
        static int WinCheck2(string[,] board)
        {
            int columnCheckResult = ColumnCheck(board);
            if (columnCheckResult != 0)
            {
                return columnCheckResult;
            }
            int rowCheckResult = RowCheck(board);
            if (rowCheckResult != 0)
            {
                return rowCheckResult;
            }
            int downDiagonalCheckResult = DownDiagonalCheck(board);
            if (downDiagonalCheckResult != 0)
            {
                return downDiagonalCheckResult;
            }
            return UpDiagonalCheck(board);
            
            
        }
        static int ColumnCheck(string[,] board)
        {
            int boardSize = board.GetLength(1);
            string winLine = "";
            //each column
            for (int xAxis = 0; xAxis < boardSize; xAxis++)
            {
                winLine = "";
                //each item in column
                for (int yAxis = 0; yAxis < boardSize; yAxis++)
                {
                    winLine += board[xAxis, yAxis];
                }//end for
                if (winLine.ToUpper() == "XXX")
                {
                    return 1;
                }//end if
                else if (winLine.ToUpper() == "OOO")
                {
                    return 2;
                }//END else if
            }//end FOR

            //if no winning line
            return 0;
        }//END FUNCTION ROW CHECK

        static int RowCheck(string[,] board)
        {
            int boardSize = board.GetLength(1);
            string winLine = "";
            //each row
            for (int yAxis = 0; yAxis < board.GetLength(1); yAxis++)
            {
                winLine = "";
                //each item in column
                for (int xAxis = 0; xAxis < (int)boardSize; xAxis++)
                {
                    winLine += board[xAxis, yAxis];
                }//end for

                if (winLine.ToUpper() == "XXX")
                {
                    return 1;
                }//end if

                else if (winLine.ToUpper() == "OOO")
                {
                    return 2;
                }//END else if
            }//end FOR

            //if no winning line
            return 0;
        }//END ROW CHECK

        static int DiagonalCheck(string[,] board)
        {
            
            if (board[1, 1] == board[0, 0] && board[1, 1] == board[2, 2])
            {
                if (board[1, 1] == "X")
                {
                    return 1;
                }
                else if (board[1, 1] == "O")
                {
                    return 2;
                } 
            }
            if (board[1, 1] == board[2, 0] && board[1, 1] == board[0, 2])
            {
                if (board[1, 1] == "X")
                {
                    return 1;
                }
                else if (board[1, 1] == "O")
                {
                    return 2;
                }
            }
            return 0;
        }//END FUNCTION
        
        static int DownDiagonalCheck(string[,] board)
        {
            int boardSize = board.GetLength(1);
            string winLine = "";
            //down diagonal
            for (int index = 0; index < boardSize; index++)
            {
                winLine += board[index, index];
            }//end for

            //check for win
            if (winLine.ToUpper() == "XXX")
            {
                return 1;
            }
            else if (winLine.ToUpper() == "OOO")
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }//end function

        static int UpDiagonalCheck(string[,] board)
        {
            int boardSize = board.GetLength(1);
            string winLine = "";
            int y_Axis = boardSize - 1;
            for (int x_Axis = 0; x_Axis < boardSize; x_Axis++)
            {
                winLine += board[x_Axis, y_Axis];
                y_Axis--;
            }
            
            //check for win
            if (winLine.ToUpper() == "XXX")
            {
                return 1;
            }//end for

            else if (winLine.ToUpper() == "OOO")
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }//end function
        static int MovesLeft(string[,] board)
        {
            int moveCounter = 0;
            foreach (string move in board)
            {
                if (move == "*")
                {
                    moveCounter++;
                }
            }
            return moveCounter;
        }

        static bool PlaceMarkerCheck(string[,] board, int x_coord, int y_Coord)
        {
            if (board[x_coord, y_Coord] != "*")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region Prompt Functions
        static string Prompt(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }//end function prompt

        static double PromptDouble(string message)
        {
            double parsedValue = 0.0;

            //GET INPUT CONVERT AND VALIDATE
            while (double.TryParse(Prompt(message), out parsedValue) == false)
            {
                Console.WriteLine("INVALID VALUE");
            }//end while
            return parsedValue;
        }//end function PromptDouble

        static bool PromptYes(string message)
        {
            string userReply = "";
            do
            {
                Console.WriteLine(message);
                userReply = Console.ReadLine().ToUpper();
                Console.WriteLine();

                if (userReply == "Y")
                {
                    return true;
                }
                else if (userReply == "N")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("INVALID ENTRY \nPlease Enter [ Y / N ]");
                }
            } while (true);


        }//END FUNCTION PROMPT YES

        static int PromptInt(string message)
        {
            int userInt = 0;

            //GET INPUT CONVERT AND VALIDATE
            while (int.TryParse(Prompt(message), out userInt) == false)
            {
                Console.WriteLine("INVALID VALUE");
            }//END WHILE

            return userInt;
        }//END FUNCTION PROMPT INT

        static double limitingRangeDouble(double lowerLimit, double upperLimit, double proposedValue)
        {
            while (proposedValue < lowerLimit || proposedValue > upperLimit)
            {
                Console.WriteLine("VALUE OUTSIDE OF RANGE");
                proposedValue = PromptDouble("Enter Value inside range");
            }
            return proposedValue;
        }
        static int limitingRangeInt(int lowerLimit, int upperLimit, int proposedValue)
        {
            while (proposedValue < lowerLimit || proposedValue > upperLimit)
            {
                Console.WriteLine("VALUE OUTSIDE OF RANGE");
                proposedValue = PromptInt("Enter Value inside range");
            }
            return proposedValue;
        }
        static string PromptXO(string message)
        {
            string userReply = "";
            do
            {
                Console.WriteLine(message);
                userReply = Console.ReadLine().ToUpper();
                Console.WriteLine();

                if (userReply == "X")
                {
                    return "X";
                }
                else if (userReply == "O")
                {
                    return "O";
                }
                else
                {
                    Console.WriteLine("INVALID ENTRY \nPlease Enter [ X / O ]");
                }
            } while (true);
        }
            #endregion
        }//end class
}//end name
