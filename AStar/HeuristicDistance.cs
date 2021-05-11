using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class HeuristicDistance : IHeuristic
    {
        private readonly Node a;
        private readonly Node b;

        public HeuristicDistance(Node a, Node b)
        {
            this.a = a;
            this.b = b;
        }

        public float GetH()
        {
            return Vector2.Distance(new Vector2(a.I, a.J), new Vector2(b.I, b.J));
        }
    }
}
