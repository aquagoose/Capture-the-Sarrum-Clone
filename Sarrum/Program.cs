/* Hello! I see you are looking at the source code for my C# implementation of Capture the Sarrum!
 * This file might not look like much. Head on over to Game.cs to get a look at some bits.
 * Board.cs contains the most interesting code.
 * Piece.cs is a load of repetitive bulls***e
 * 
 * (c) Ollie Robinson 2021, this code is for educational purposes ONLY
 * Any unauthorised copying will be noted and action may be taken. */

namespace Sarrum
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Begin();
        }
    }
}
