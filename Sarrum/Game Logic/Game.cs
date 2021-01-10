using System;

namespace Sarrum
{
    class Game
    {
        readonly int _boardSize = 8;

        public void Begin()
        {
            bool sampleGame;
            Console.Write("Welcome to Capture the Sarrum!\nBefore we begin, would you like a sample game? (y/n) ");
            string wantsSampleGame = Console.ReadLine();
            if (wantsSampleGame.ToLower() == "y") sampleGame = true;
            else sampleGame = false;
            
            GenerateBoard(sampleGame); // Generate the board.
            Board.DisplayBoard(); // Display the board
            Color currentTurn = Color.White; // Stores the current turn.
            while (!Board.SarrumCaptured) // Runs all the time that a sarrum has not been captured.
            {
                Console.WriteLine($"It is {currentTurn}'s turn.");
                string inputs = ""; // The string of inputs. You'll see what this does later.
                Console.Write("Enter the coordinates of the square containing the piece to move (file first): ");
                inputs += Console.ReadLine(); // Get the first set of coords.
                Console.WriteLine("Enter the coordinates of the square you want to move the piece to (file first): ");
                inputs += Console.ReadLine(); // Get the second sets
                char[] coordData = inputs.ToCharArray(); // Convert the inputs string into a char array.
                if (coordData.Length < 4) { Console.WriteLine("That's an illegal move, try again!"); continue; }
                for (int coord = 0; coord < coordData.Length; coord++) coordData[coord] -= (char)48; // Subtract 48 from each char (converting the char to an int essentially);
                // TODO: Adjust for allowing double digit numbers, error checking.
                if (Board.PerformPieceAction(coordData[0], coordData[1], coordData[2], coordData[3], currentTurn)) // Attempt to perform the move
                {
                    if (Board.SarrumCaptured) break; // Break out of the loop if the sarrum is captured.
                    if (currentTurn == Color.White) currentTurn = Color.Black; // Set the current turn.
                    else if (currentTurn == Color.Black) currentTurn = Color.White;
                    Board.DisplayBoard(); // If successful, display the board.
                    
                }
                else Console.WriteLine("That's an illegal move, try again!"); // Otherwise :O that's illegal, prison time oh no
            }
            Board.DisplayBoard(); // Display the final board.
            if (currentTurn == Color.White) Console.WriteLine("\n\nBlack's Sarrum has been captured!"); // Display the correct sarrum. I can probably do this better.
            else if (currentTurn == Color.Black) Console.WriteLine("\n\nWhite's Sarrum has been captured!");
            Console.Write("Play again? (y/n) "); // Play again?
            string playAgain = Console.ReadLine();
            if (playAgain.ToLower() == "y") Begin();
            else Console.WriteLine("\n\n\n  ----------------------------------------\n  | Goodbye, thanks for playing!         |\n  | Developed by Ollie Robinson (c) 2021 |\n  ----------------------------------------\n");
        }

        void GenerateBoard(bool sampleGame = false)
        {
            if (!Board.CreateBoard(_boardSize)) Console.WriteLine("Unable to create board!");
            if (!sampleGame)
            {
                PieceType currentPieceType = PieceType.None;
                for (int x = 1; x < Board.BoardSize+1; x++) // I could potentially make this loop better
                {
                    switch(x) // This sets the correct piece type based on the X position
                    {
                        case 1: currentPieceType = PieceType.Gisgigir; break;
                        case 2: currentPieceType = PieceType.Etlu; break;
                        case 3: currentPieceType = PieceType.Nabu; break;
                        case 4: currentPieceType = PieceType.MarzazPani; break;
                        case 5: currentPieceType = PieceType.Sarrum; break;
                        case 6: currentPieceType = PieceType.Nabu; break;
                        case 7: currentPieceType = PieceType.Etlu; break;
                        case 8: currentPieceType = PieceType.Gisgigir; break;
                    }
                    Board.SetPiece(x, 1, currentPieceType, Color.Black); // Draw the pieces to the board.
                    Board.SetPiece(x, 8, currentPieceType, Color.White);
                    Board.SetPiece(x, 2, PieceType.Redum, Color.Black); // Draw redums for the entire width of the board.
                    Board.SetPiece(x, 7, PieceType.Redum, Color.White);
                }
            }
            else
            {
                Board.SetPiece(2, 1, PieceType.Gisgigir, Color.Black); // This is pre-set.
                Board.SetPiece(4, 1, PieceType.Sarrum, Color.Black);
                Board.SetPiece(8, 1, PieceType.Gisgigir, Color.White);
                Board.SetPiece(1, 2, PieceType.Redum, Color.White);
                Board.SetPiece(1, 3, PieceType.Sarrum, Color.White);
                Board.SetPiece(2, 3, PieceType.Etlu, Color.Black);
                Board.SetPiece(8, 2, PieceType.Etlu, Color.Black);
                Board.SetPiece(8, 6, PieceType.Redum, Color.Black);
            }
        }
    }
}
