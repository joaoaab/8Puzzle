using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    public class Puzzle
    {
        /// Boards to keep track of win condition
        public int[] completedBoard = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        int[] currentBoard = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        public const int Right = 1;
        public const int Left = -1;
        public const int Up = -3;
        public const int Down = +3;
        private int moveCount;
        
        /// Represents Left Up Right Down
        public List<bool[]> validMoves = new List<bool[]>() {new bool[] { false, false, true, true } ,
                                                            new bool[] { true, false, true, true } ,
                                                            new bool[] { true, false, false, true } ,
                                                            new bool[] { false, true, true, true } ,
                                                            new bool[] { true, true, true, true } ,
                                                            new bool[] { true, true, false, true } ,
                                                            new bool[] { false, true, true, false } ,
                                                            new bool[] { true, true, true, false } ,
                                                            new bool[] { true, true, false, false }};

        public Puzzle()
        {
            this.moveCount = 0;
        }

        public void SetBoard(int[] board)
        {
            this.currentBoard = board;
        }

        public int[] GetBoard()
        {
            return this.currentBoard;
        }


        public String GetBoardString()
        {
            String array = "";
            foreach (int val in this.currentBoard)
            {
                array += val.ToString();
            }
            return array;
        }
        
        public int GetMoveCount()
        {
            return this.moveCount;
        }

        public void SetMoveCount(int moves)
        {
            this.moveCount = moves;
        }

        /// Checks if the Player Solved the Puzzle
        public bool IsComplete()
        {
            return currentBoard.SequenceEqual<int>(completedBoard);
        }


        /// Checks if the board is solvable
        public bool IsSolvable()
        {
            int inversions = 0;
            for(int i = 0; i < currentBoard.Length - 1; i++)
            {
                for(int j = i+1; j < currentBoard.Length; j++)
                {
                    if (currentBoard[i] > currentBoard[j]) inversions++;
                }
                if (currentBoard[i] == 0 && i % 2 == 1) inversions++;
            }
            return (inversions % 2 == 0);
        }

        /// <summary>
        /// Linear-Shuffle Algorithm
        /// </summary>
        public void Shuffle()
        {
            System.Random Randomness = new System.Random();
            for (int i = 0; i < this.currentBoard.Length; i++)
            {
                int j = Randomness.Next(i, this.currentBoard.Length);
                int temp = this.currentBoard[i];
                this.currentBoard[i] = this.currentBoard[j];
                this.currentBoard[j] = temp;
            }
        }


        /// <summary>
        /// Checks if the moves is possible, if so, does it
        /// </summary>
        /// <param name="index">the index of the number in the puzzle array</param>
        public void Move(int index)
        {
            bool[] moves = validMoves[index];
            if (moves[0])
            {
                if(currentBoard[index + Left] == 0)
                {
                    swap(index,Left);
                    moveCount++;
                }
            }
            if (moves[1])
            {
                if (currentBoard[index + Up] == 0)
                {
                    swap(index, Up);
                    moveCount++;
                }
            }
            if (moves[2])
            {
                if (currentBoard[index + Right] == 0)
                {
                    swap(index, Right);
                    moveCount++;
                }
            }
            if (moves[3])
            {
                if (currentBoard[index + Down] == 0)
                {
                    swap(index, Down);
                    moveCount++;
                }
            }
        }

        private void swap(int index,int shift)
        {
            int temp = currentBoard[index];
            currentBoard[index] = currentBoard[index + shift];
            currentBoard[index + shift] = temp;
        }

    }

}
