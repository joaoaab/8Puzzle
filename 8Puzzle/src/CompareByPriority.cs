using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    class CompareByPriority : Comparer<Vertex>
    {
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
