using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class HeuristicNone : IHeuristic
    {
        public float GetH()
        {
            return 0;
        }
    }
}
