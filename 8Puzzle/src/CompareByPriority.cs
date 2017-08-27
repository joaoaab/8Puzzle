﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    class CompareByPriority : Comparer<Vertex>
    {
        /// <summary>
        /// Compares 2 Vertexes by it's priority
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(Vertex x, Vertex y)
        {
            if(x.priority.CompareTo(y.priority) != 0)
            {
                return x.priority.CompareTo(y.priority);
            }
            else
            {
                return 0;
            }
        }
    }
}
