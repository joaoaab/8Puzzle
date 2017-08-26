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

        private bool IsCompletedAI(int[] currentBoard)
        {
            return currentBoard.SequenceEqual<int>(completedBoard);
        }


        public void BFS()
        {
            Queue<Vertex> fila = new Queue<Vertex>();
            fila.Enqueue(this.frontier);
            Vertex V;
            while(fila.Count > 0)
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

        public void CreateSolution(Vertex V)
        {
            while (V.IsNotFirst())
            {
                solution.Add(V.board);
                V = V.GetParent();
            }
            solution.Reverse();
            this.frontier = null;
        }

        public void PrintSolution()
        {
            foreach(int[] b in this.solution)
            {
                this.game.SetMoveCount(game.GetMoveCount() + 1);
                main.UpdateBoard(b);
                Thread.Sleep(500);
            }
            this.solution = null;
            GC.Collect();
        }

        public void AStar()
        {

        }



    }
}
