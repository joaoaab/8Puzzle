using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    public class Vertex
    {
        public int[] board;
        private bool hasParent;
        public List<Vertex> children;
        private Vertex parent;
        public int height { get; set; }
        public int priority { get; set; }

        /// <summary>
        /// List that contains all the moves possible
        /// </summary>
        public List<bool[]> validMoves = new List<bool[]>() {new bool[] { false, false, true, true } ,
                                                            new bool[] { true, false, true, true } ,
                                                            new bool[] { true, false, false, true } ,
                                                            new bool[] { false, true, true, true } ,
                                                            new bool[] { true, true, true, true } ,
                                                            new bool[] { true, true, false, true } ,
                                                            new bool[] { false, true, true, false } ,
                                                            new bool[] { true, true, true, false } ,
                                                            new bool[] { true, true, false, false }};

        
        public Vertex(int[] board)
        {
            this.board = board;
            this.children = new List<Vertex>();
            this.parent = null;
            this.hasParent = false;
            this.height = 0;
            this.priority = ManhattanHeuristic(this.board);
        }

        public int ChildrenCount()
        {
            return this.children.Count;
        }

        public void AddChild(Vertex V)
        {
            V.hasParent = true;
            V.parent = this;
            this.children.Add(V);
        }

        public void ResetParent()
        {
            this.parent = null;
            this.hasParent = false;
        }

        public Vertex GetParent()
        {
            return this.parent;
        }

        public bool IsNotFirst()
        {
            return this.hasParent;
        }

        /// <summary>
        /// Generates the states avaliables in the next iteration
        /// According to the current state and the possible actions
        /// </summary>
        public void GenerateStates()
        {
            int index = GetIndex0();
            int temp;
            bool[] movesAvaliables = this.validMoves[index];
            if (movesAvaliables[0])
            {
                int[] newBoard = (int[])this.board.Clone();
                temp = newBoard[index];
                newBoard[index] = newBoard[index - 1];
                newBoard[index - 1] = temp;
                this.AddChild(new Vertex(newBoard));
            }
            if (movesAvaliables[1])
            {
                int[] newBoard = (int[])this.board.Clone();
                temp = newBoard[index];
                newBoard[index] = newBoard[index - 3];
                newBoard[index - 3] = temp;
                this.AddChild(new Vertex(newBoard));
            }
            if (movesAvaliables[2])
            {
                int[] newBoard = (int[])this.board.Clone();
                temp = newBoard[index];
                newBoard[index] = newBoard[index + 1];
                newBoard[index + 1] = temp;
                this.AddChild(new Vertex(newBoard));
            }
            if (movesAvaliables[3])
            {
                int[] newBoard = (int[])this.board.Clone();
                temp = newBoard[index];
                newBoard[index] = newBoard[index + 3];
                newBoard[index + 3] = temp;
                this.AddChild(new Vertex(newBoard));
            }
        }

        /// <summary>
        /// Finds the index of the 0(blank)
        /// in the array that represents the board
        /// </summary>
        /// <returns>Index of the blank symbol</returns>
        private int GetIndex0()
        {
            int index0 = 0;
            for (int i = 0; i < this.board.Length; i++)
            {
                if (this.board[i] == 0)
                {
                    index0 = i;
                    break;
                }
            }
            return index0;
        }

        /// <summary>
        /// Calculates the distance of the numbers from it's rightful place
        /// </summary>
        /// <param name="board">the board to be evaluated</param>
        /// <returns>The Heuristic Value of the Node</returns>
        public int ManhattanHeuristic(int[] board)
        {
            int heuristicValue = 0;
            for (int i = 0; i < board.Length; i++)
            {
                int value = board[i];
                int x = i / 3;
                int y = i % 3;
                int xTarget = 0;
                int yTarget = 0;
                if (value != 0)
                {
                    xTarget = value / 3;
                    yTarget = value - (xTarget * 3);
                }
                else
                {
                    xTarget = 2;
                    yTarget = 3;
                }

                if (yTarget == 0)
                {
                    xTarget -= 1;
                    yTarget = 2;
                }
                else
                {
                    yTarget -= 1;
                }
                heuristicValue += Math.Abs(x - xTarget) + Math.Abs(y - yTarget);
            }
            return heuristicValue;
        }

    }

}
