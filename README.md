# Capture the Sarrum Clone
A Capture the Sarrum clone written in C#.
Based on AQA Capture the Sarrum. True to the given instructions.
Written and adapted for C# by Ollie Robinson.

## How to play
When you start the game, you will be asked if you want to play a sample game. A sample game is a demo version of the main game.
Choose `y` to start the sample game, and you will be brought to a board with a few pre-set pieces on it.
Feel free to experiment with moving the pieces around, and see what they all do and interact.

Once you're ready to play, just type `n` when prompted and the game will begin.

### The grid
The grid is an 8x8 board. The rows are called **ranks** and the columns are called **files**.

### The pieces
Here is a list of the pieces and how they move.

| In-Game Letter | Piece Name | How the piece can move |
| -------------- | ---------- | ---------------------- |
| S | Sarrum | A sarrum can move in any direction, one square at a time |
| M | Marzaz Pani | A marzaz pani can move either horizontally or vertically, **one** square at a time. They **cannot** move diagonally. |
| N | Nabu | A nabu can only move diagonally, **one** square at a time |
| E | Etlu | An etlu must move **two** squares at a time either horizontally or vertically. They **cannot** move diagonally and **must** always move two squares at a time. **Can** jump over other pieces. |
| G | Gisgigir | A gisgigir can move **any number** of squares either horizontally or vertically. They **cannot** move diagonally, or jump over other pieces. |
| R | Redum | A redum can **only** move forward **one** square at a time, and **only** into an empty square. However, if the square directly in front of and to the left or right of the redum contains a piece of the opposite colour, the redum **can** move here and capture the piece. (NOTE: I MISSED THIS BEHAVIOUR, WILL BE ADDED SOON) If a redum reaches the back rank (rank 1 for white, rank 8 for black), it will be promoted (changes) into a marzaz pani. |

### Colours
Just like a game of chess, there are white and black pieces. These use the prefix "W" and "B".
For example, a white sarrum will look like "WS" on the board. A black gisgigir will look like "BG" on the board.
These colours alternate per round.

### Moving pieces
When you want to move a piece, the game will ask you the starting, and finishing coordinates of the move. When entering the coordinates, enter just a number, starting with the file first.
So, let's say, in the sample game, you want to move the white sarrum located at file 1, rank 3.
When you are prompted on which piece you want to move, type `13` into the input and press enter. This means you are selecting file 1, rank 3. This corresponds to the white sarrum mentioned earlier.
Let's say we want to move the piece one rank down. Type `14` when prompted. If done correctly, you should see the white sarrum move down on the grid.
Try again a few more times. Once you understand this input mechanic, playing the game is easy!

Pieces **cannot** occupy the same grid slot as another piece with the same colour.
For example, a white redum is at file 3 rank 5. A white marzaz pani occupies file 3 rank 4. If you try this, the redum will not be able to move forward.

**All pieces** can capture a piece of the opposite colour, using the described behaviour of each piece.

Any movement that is disallowed will not occur, and you will be informed that you are trying to perform an illegal move.

### Ending the game
The game ends when the opposite colour's sarrum is captured.
You will be asked if you want to play again or not.

## Development
This was developed in 2 days.
I originally tried to directly translate the python code to C#, however I found it to be poorly written and despite my best efforts, I couldn't get it to work in C# at all. All my moves were illegal.
So, I decided to write it from scratch, in a way I felt was proper.

## Plans
I plan to convert this game to be a chess clone. I also would like to add some extra bits to the game including some extra gameplay options. This version sticks true to the instructions given, so I may fork that version to a new fork, so people can play the original if they want to.

I also need to add the behaviour on the Redum to convert it to a marzaz pani if it reaches the end rank. (I forgot to add this, sorry.)

Some of the code needs error checking and some other bits, but for the most part it's fairly solid.

## Compiling
To anyone who's unsure, to compile, open the solution in Visual Studio and click "run" (I recommend release mode.).

### Errors?
If there are any bugs you can see, or the game doesn't work properly etc. **Please create an issue** and I will fix it in due course.

## Thank you's
Thanks for checking it out! I put a lot of effort into it so thanks for checking it out!