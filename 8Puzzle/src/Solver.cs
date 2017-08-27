using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _8Puzzle
{
    public class Solver
    {
        public Vertex frontier;
        public Vertex endPoint;
        public List<int[]> solution;
        public MainWindow main;
        Puzzle game;
        public int[] completedBoard = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

        public Solver(int[] board, MainWindow Window,Puzzle game)
        {
            this.solution = new List<int[]>();
            this.frontier = new Vertex(board);
            this.main = Window;
            this.game = game;
            this.endPoint = null;
        }

        private void PrintBoard(int[] board)
        {
            foreach(int i in board)
            {
                Console.Write(i + "  ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Checks if the puzzle is completed
        /// </summary>
        /// <param name="currentBoard"></param>
        /// <returns>true|false</returns>
        private bool IsCompletedAI(int[] currentBoard)
        {
            return currentBoard.SequenceEqual<int>(completedBoard);
        }

        /// <summary>
        /// Backtracks the Vertexes to find the solution, and then reverses the list
        /// </summary>
        /// <param name="V">Vertex that contains the final state</param>
        public void CreateSolution(Vertex V)
        {
            while (V.IsNotFirst())
            {
                solution.Add(V.board);
                V = V.GetParent();
                V.children = null;
            }
            solution.Reverse();
            this.frontier = null;
            GC.Collect();
        }

        /// <summary>
        /// iterates through the list in order to update the UI with the solution
        /// </summary>
        public void PrintSolution()
        {
            foreach(int[] b in this.solution)
            {
                this.game.SetMoveCount(game.GetMoveCount() + 1);
                main.UpdateBoard(b);
                Thread.Sleep(200);
            }
            this.solution = null;
            GC.Collect();
        }

        /// <summary>
        /// Breadth-First Search in the Vertexes
        /// Until it finds the Vertex with the board that is the solution
        /// </summary>
        public void BFS()
        {
            Queue<Vertex> fila = new Queue<Vertex>();
            fila.Enqueue(this.frontier);
            Vertex V;
            while (fila.Count > 0)
            {
                V = fila.Dequeue();
                V.GenerateStates();
                if (IsCompletedAI(V.board))
                {
                    endPoint = V;
                    fila.Clear();
                    break;
                }

                foreach (Vertex item in V.children)
                {
                    fila.Enqueue(item);
                }

            }
            CreateSolution(endPoint);
            PrintSolution();
        }


        /// <summary>
        /// Peforms the A* algorithm and stores the solution
        /// </summary>
        public void AStar()
        {
            List<Vertex> heapOpen = new List<Vertex> { this.frontier };
            List<Vertex> heapClosed = new List<Vertex>();
            while(heapOpen.Count > 0)
            {
                Vertex V = GetBestFromList(heapOpen);
                heapOpen.RemoveAt(0);
                V.GenerateStates();
                //PrintBoard(V.board);
                if (IsCompletedAI(V.board))
                {
                    this.endPoint = V;
                    heapOpen.Clear();
                    heapClosed.Clear();
                    break;
                }
                foreach(Vertex Q in V.children)
                {
                    Q.height = Q.GetParent().height + 1;
                    Q.priority = Q.priority + Q.height;
                    if (ListHasBetterVersion(Q, heapOpen))
                    {
                        continue;
                    }
                    if (ListHasBetterVersion(Q, heapClosed))
                    {
                        continue;
                    }
                    heapOpen.Add(Q);
                    heapOpen.Sort(new CompareByPriority());
                }
                heapClosed.Add(V);
            }
            PrintBoard(this.endPoint.board);
            CreateSolution(endPoint);
            PrintSolution();
            Console.WriteLine("Found it !");
        }

        /// <summary>
        /// Returns the vertex with the least priority from the list
        /// </summary>
        /// <param name="vertexList"></param>
        /// <returns>the best vertex to go</returns>
        private Vertex GetBestFromList(List<Vertex> vertexList)
        {
            vertexList.Sort(new CompareByPriority());
            return vertexList.First();
        }

        /// <summary>
        /// Checks if the list has a better candidate passing through that vertex
        /// </summary>
        /// <param name="V">Vertex to test</param>
        /// <param name="heapOpen">List</param>
        /// <returns>true if it has a better candidate, false otherwise</returns>
        private bool ListHasBetterVersion(Vertex V, List<Vertex> heapOpen)
        {
            return heapOpen.Find(n => n.board.SequenceEqual(V.board) && n.priority <= V.priority) != null;
        }
    }
}
