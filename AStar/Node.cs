using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Node
    {
        private readonly Random rnd = new();
        private readonly SpriteBatch spriteBatch;
        private Rectangle coords;
        private readonly int size;

        public int I { get; private set; }
        public int J { get; private set; }
        public float F { get; set; }
        public float G { get; set; }
        public float H { get; private set; }
        public HashSet<Node> Neighbors { get; private set; }
        public Node PreviousNode { get; set; }
        public bool IsWall { get; set; }

        public Node(int i, int j, int rows, int cols, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            I = i;
            J = j;
            this.spriteBatch = spriteBatch;

            Neighbors = new();

            if (rnd.NextDouble() < Math.Min(MathF.Min(I, J) / 100, 0.5))
                IsWall = true;

            double height = graphics.GraphicsDevice.Viewport.Bounds.Height / rows;
            double width = graphics.GraphicsDevice.Viewport.Bounds.Width / cols;

            size = (int)Math.Min(height, width);

            coords = new Rectangle(I * size, J * size, size, size);
        }

        public void Draw(Color color)
        {            
            spriteBatch.Draw(Colors.GetTexture(color), coords, color);
        }

        public void AddNeighbors(Node[,] grid)
        {
            var cols = grid.GetLength(0);
            var rows = grid.GetLength(1);

            if (I < cols - 1)
                Neighbors.Add(grid[I + 1, J]);
            if (I > 0)
                Neighbors.Add(grid[I - 1, J]);
            if (J < rows - 1)
                Neighbors.Add(grid[I, J + 1]);
            if (J > 0)
                Neighbors.Add(grid[I, J - 1]);
            if (I > 0 && J > 0)
                Neighbors.Add(grid[I - 1, J - 1]);
            if (I < cols - 1 && J > 0)
                Neighbors.Add(grid[I + 1, J - 1]);
            if (I > 0 && J < rows - 1)
                Neighbors.Add(grid[I - 1, J + 1]);
            if (I < cols - 1 && J < rows - 1)
                Neighbors.Add(grid[I + 1, J + 1]);
        }

        internal void SetH(IHeuristic heuristic)
        {
            H = heuristic.GetH();
        }
    }
}