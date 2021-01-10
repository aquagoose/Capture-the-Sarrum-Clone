using System;

namespace Sarrum
{
    static class Board // This has apparently become the main logic class. Right.
    {
        /* The functions in this class allow easy manipulating of the board.
         * I'm not sure why I put this in a static class now, but oh well, I did.
         * The functions in this class means that the board does never need to be directly accessed,
         * this prevents the program from writing to sections of memory that are unavailable,
         * as the functions here do the checking before any action is performed. */


        private static int _boardSize = 1; // Worst case scenario.
        private static Piece[,] board; // A board of pieces.
        // Each piece has a type, and a colour.

        public static int BoardSize { get { return _boardSize; } }

        public static bool SarrumCaptured { get; private set; } = false;

        /// <summary>
        /// Create a board with the chosen board size. Overwrites any previously active board.
        /// </summary>
        /// <param name="boardSize">The size of the board. Boards are square.</param>
        /// <returns>Whether the creation was successful or not.</returns>
        public static bool CreateBoard(int boardSize) // Initialise the main board of pieces.
        {
            _boardSize = boardSize;
            try
            {
                board = new Piece[_boardSize, _boardSize]; // Try to create the board
                for (int y = 0; y < _boardSize; y++)
                {
                    for (int x = 0; x < _boardSize; x++)
                    {
                        board[x, y] = new Piece();
                    }
                }
                return true;
            }
            catch (Exception e) // If there is an error, return false.
            {
                System.Diagnostics.Debug.WriteLine($"Error has occured\n{e.StackTrace}"); // Oopsie, the program did a whoopsie (no I can't be bothered to use system.diagnostics, what are you looking at?)
                return false;
            }
        }

        /// <summary>
        /// Display the board in it's current state.
        /// </summary>
        public static void DisplayBoard() // Display the board
        {
            Console.WriteLine("  -------------------------"); // Top border of the board
            for (int y = 0; y < _boardSize; y++)
            {
                Console.Write($"{y+1} "); // Prints the Y
                for (int x = 0; x < _boardSize; x++)
                {
                    Console.Write("|"); // Beginning side border
                    // You will see me casting the enums to chars here, this is because the enums (located in Piece.cs)
                    // have integer values that match the ASCII value of that char. This allows easy drawing of each
                    // piece in the correct color.
                    Console.Write((char)board[x, y].color);
                    if (board[x, y].type != PieceType.None) Console.Write((char)board[x, y].type);
                    else Console.Write(" "); // Write blank, if the type is none.
                }
                Console.WriteLine("|"); // End side border.
                Console.WriteLine("  -------------------------");
            }
            Console.WriteLine("   1  2  3  4  5  6  7  8");
        }

        /// <summary>
        /// Create a piece at the X and Y coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pieceType">What piece gets placed.</param>
        /// <param name="color">What color the piece is</param>
        public static void SetPiece(int x, int y, PieceType pieceType, Color color)
        {
            if ((x < 1 || x > _boardSize) || (y < 1 || y > _boardSize))
             throw new Exception("Cannot place piece outside of board range!"); // Prevent unauthorised memory accesses. In theory should never be called.
            else board[x - 1, y - 1].type = pieceType;
            board[x - 1, y - 1].type = pieceType;
            board[x - 1, y - 1].color = color;
        }

        /// <summary>
        /// Delete the piece at the X and Y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void RemovePiece(int x, int y)
        {
            if ((x < 1 || x > _boardSize) || (y < 1 || y > _boardSize))
                throw new Exception("Cannot place piece outside of board range!");
            board[x - 1, y - 1].type = PieceType.None;
            board[x - 1, y - 1].color = Color.None;
        }

        /// <summary>
        /// Find the current piece at the X and Y coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>The piece found at the coordinates.</returns>
        public static Piece GetPiece(int x, int y)
        {
            if ((x < 1 || x > _boardSize) || (y < 1 || y > _boardSize))
                return new Piece();
            else return board[x - 1, y - 1];
        }

        /// <summary>
        /// Move the piece at the start X and Y to the destination X and Y
        /// </summary>
        /// <param name="startX">Starting X coordinate.</param>
        /// <param name="startY">Starting Y coordinate.</param>
        /// <param name="destX">Destination X coordinate.</param>
        /// <param name="destY">Destination Y coordinate.</param>
        public static void MovePiece(int startX, int startY, int destX, int destY)
        {
            if ((startX < 1 || startX > _boardSize) || (startY < 1 || startY > _boardSize)
                || (destX < 1 || destX > _boardSize) || (destY < 1 || destY > _boardSize)) throw new Exception("Cannot place piece outside of board range!");
            Piece piece = GetPiece(startX, startY); // Get the current piece.
            SetPiece(destX, destY, piece.type, piece.color); // Get the piece at the destination to the current piece's information
            RemovePiece(startX, startY); // Delete the piece at the current location.
        }

        /// <summary>
        /// Attempt to move the piece from the start X and Y to the destination X and Y.
        /// </summary>
        /// <param name="startX">Starting X coordinate.</param>
        /// <param name="startY">Starting Y coordinate.</param>
        /// <param name="destX">Destination X coordinate.</param>
        /// <param name="destY">Destination Y coordinate.</param>
        /// <returns>Whether the piece move action was successful or not.</returns>
        public static bool PerformPieceAction(int startX, int startY, int destX, int destY, Color currentTurn) // This performs the piece action. A start x and y is given, and a destination.
        {
            if (startX < 1 || startX > _boardSize || startY < 1 || startY > _boardSize
                || destX < 1 || destX > _boardSize || destY < 1 || destY > _boardSize) return false; // Checks to make sure the move isn't out of the boundaries of the board.

            bool isSarrum = GetPiece(destX, destY).type == PieceType.Sarrum;

            if (startX == destX && startY == destY) return false; // Makes sure you don't move to your current square.
            else if (GetPiece(startX, startY).type == PieceType.None) return false; // Makes sure the current selected piece actually exists.
            else if (GetPiece(startX, startY).color != currentTurn) return false;
            else if (GetPiece(destX, destY).color == GetPiece(startX, startY).color) return false; // Makes sure you're not moving the piece to a piece of the same colour.
            else if (GetPiece(startX, startY).MovePiece(startX, startY, destX, destY))
            {
                if (isSarrum) SarrumCaptured = true;
                return true;
            }
            else return false;
        }
    }
}
