using System;

namespace Sarrum
{
    class Piece
    {
        public PieceType type = PieceType.None;
        public Color color = Color.None;

        public bool MovePiece(int startX, int startY, int destX, int destY) // The main logic for moving the pieces.
        {
            bool isLegalMove = false; // Always start with legal move being false.
            switch (type) // Get the current type of the piece.
            {
                case PieceType.Sarrum:
                    if (Math.Abs(destX - startX) <= 1 && Math.Abs(destY - startY) <= 1) // Move if both values are equal to or less than 1. This allows all directions of movement.
                        isLegalMove = true;
                    break;
                case PieceType.MarzazPani:
                    if ((Math.Abs(destX - startX) == 1 && Math.Abs(destY - startY) == 0) || (Math.Abs(destX - startX) == 0 && Math.Abs(destY - startY) == 1)) // Restricts the piece to only horizontal or vertical movement.
                        isLegalMove = true;
                    break;
                case PieceType.Nabu:
                    if (Math.Abs(destX - startX) == 1 && Math.Abs(destY - startY) == 1) // Must move exactly diagonal.
                        isLegalMove = true;
                    break;
                case PieceType.Etlu:
                    if ((Math.Abs(destX - startX) == 2 && Math.Abs(destY - startY) == 0) || (Math.Abs(destY - startY) == 2 && Math.Abs(destX - startX) == 0)) // Similar to the Marzaz Pani.
                        isLegalMove = true;
                    break;
                case PieceType.Gisgigir:
                    if (Math.Abs(destX - startX) <= Board.BoardSize && Math.Abs(destY - startY) == 0) // X coord is changing
                    {
                        isLegalMove = true;
                        if (destX - startX < 0) // This checks to see if the piece is moving forward or backward.
                            for (int x = startX - 1; x > destX; x--) // Loop on the X coord until the destination X.
                            {
                                Piece checkPiece = Board.GetPiece(x, startY); // Get the current piece of each part of the grid.
                                if (checkPiece.type != PieceType.None && x > destX) isLegalMove = false; // If the piece isn't nothing, then that's an ILLEGAL move and can lead to PRISON TIME. BAD.
                            }
                        else if (destX - startX > 0) // Works the same as above
                            for (int x = startX + 1; x < destX; x++)
                            {
                                Piece checkPiece = Board.GetPiece(x, startY);
                                if (checkPiece.type != PieceType.None && x < destX) isLegalMove = false;
                            }
                    }
                    else if (Math.Abs(destY - startY) <= Board.BoardSize && Math.Abs(destX - startX) == 0) // Y coord is changing
                    {
                        isLegalMove = true;
                        if (destY - startY < 0) // Also works the same as above, just for Y instead of X. I could maybe do this better.
                            for (int y = startY - 1; y > destY; y--)
                            {
                                Piece checkPiece = Board.GetPiece(startX, y);
                                if (checkPiece.type != PieceType.None && y > destY) isLegalMove = false;
                            }
                        else if (destY - startY > 0)
                            for (int y = startY + 1; y < destY; y++)
                            {
                                Piece checkPiece = Board.GetPiece(startX, y);
                                if (checkPiece.type != PieceType.None && y < destY) isLegalMove = false;
                            }
                    }
                    break;
                case PieceType.Redum:
                    if (color == Color.White) // Makes sure that white pieces can only move "up" and black pieces can only move "down".
                    {
                        isLegalMove = true;
                        if (destY - startY == -1 && destX - startX == 0) // Checks to make sure the piece is only moving forward.
                        {
                            if (Board.GetPiece(startX, startY - 1).color == Color.Black) isLegalMove = false; // Prevents the piece from moving into any piece at all.
                            else if (destY == 1) type = PieceType.MarzazPani;
                        }
                        // Code below checks to see if there is a black piece on the diagonal, since that is a legal move and a court has ruled it as such.
                        else if (Board.GetPiece(startX - 1, startY - 1).color == Color.Black || Board.GetPiece(startX + 1, startY - 1).color == Color.Black)
                        {
                            if (destY == 1) type = PieceType.MarzazPani;
                            isLegalMove = true;
                        }
                        else isLegalMove = false;
                    }
                    else if(color == Color.Black) // Works the exact same as above.
                    {
                        isLegalMove = true;
                        if (destY - startY == 1 && destX - startX == 0)
                        {
                            if (Board.GetPiece(startX, startY + 1).color == Color.White) isLegalMove = false;
                            else if (destY == 8) type = PieceType.MarzazPani;
                        }
                        else if (Board.GetPiece(startX - 1, startY + 1).color == Color.White || Board.GetPiece(startX + 1, startY + 1).color == Color.White)
                        {
                            if (destY == 8) type = PieceType.MarzazPani;
                            isLegalMove = true;
                        }
                        else isLegalMove = false;
                    }
                    break;
            }
            if (isLegalMove) Board.MovePiece(startX, startY, destX, destY); // If the move is legal, move the piece. ILLEGAL moves will be dealt with >:)
            return isLegalMove;
        }
    }
    public enum PieceType // Piece type enum. Affects game logic.
    {
        None,
        Sarrum = 83,
        MarzazPani = 77,
        Nabu = 78,
        Etlu = 69,
        Gisgigir = 71,
        Redum = 82
    }

    public enum Color // Color enum. Affects game logic.
    {
        None,
        White = 87,
        Black = 66
    }
}
