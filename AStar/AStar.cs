using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class AStar
    {
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDeviceManager graphics;

        const int cols = 150;
        const int rows = 150;
        private Node[,] grid = new Node[cols, rows];
        private bool isFinished;

        private readonly List<Node> openSet = new();
        private readonly List<Node> closedSet = new();
        private readonly List<Node> unckeckedSet = new();
        private Node startNode;
        private Node endNode;
        private Node currentNode;

        public bool IsLoaded { get; private set; }

        public AStar(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;

            Init();
            IsLoaded = true;
        }

        public void Init()
        {
            if (IsLoaded)
                return;

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                {
                    Node node = new(i, j, rows, cols, spriteBatch, graphics);
                    unckeckedSet.Add(node);
                    grid[i, j] = node;
                }

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    grid[i, j].AddNeighbors(grid);

            startNode = grid[0, 0];
            endNode = grid[cols - 1, rows - 1];
            startNode.IsWall = false;
            endNode.IsWall = false;

            openSet.Add(startNode);
        }

        public void Run()
        {
            if (openSet.Count > 0)
            {
                int lowestIndex = 0;
                lowestIndex = openSet.FindIndex(n => n.F == openSet.Min(n => n.F));

                currentNode = openSet.ElementAt(lowestIndex);
                unckeckedSet.Remove(currentNode);

                if (currentNode == endNode)
                {
                    isFinished = true;
                    return;
                }

                openSet.Remove(currentNode);

                closedSet.Add(currentNode);

                HashSet<Node> neighbors = currentNode.Neighbors;
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var neighborNode = neighbors.ElementAt(i);

                    if (!closedSet.Contains(neighborNode) && !neighborNode.IsWall)
                    {
                        float tempG = currentNode.G + 1;

                        if (openSet.Contains(neighborNode))
                        {
                            if (tempG < neighborNode.G)
                            {
                                neighborNode.G = tempG;
                            }
                        }
                        else
                        {
                            neighborNode.G = tempG;
                            openSet.Add(neighborNode);
                        }


                        neighborNode.SetH(new HeuristicDistance(neighborNode, endNode));
                        //neighborNode.SetH(new HeuristicNone());
                        neighborNode.F = neighborNode.G + neighborNode.H;
                        neighborNode.PreviousNode = currentNode;
                    }
                }
            }
            else
            {
                return;
            }
        }

        public void Draw()
        {
            Color color;

            foreach (Node node in unckeckedSet)
                node.Draw(node.IsWall ? Color.DimGray : Color.White);

            color = isFinished ? Color.White : Color.LightGreen;
            foreach (Node node in openSet)
                node.Draw(color);

            color = isFinished ? Color.White : Color.Red;
            foreach (Node node in closedSet)
                node.Draw(color);

            Node temp = currentNode;

            color = isFinished ? Color.Cyan : Color.Yellow;
            while (temp.PreviousNode != null)
            {
                temp.Draw(color);
                temp = temp.PreviousNode;
            }
        }
    }
}