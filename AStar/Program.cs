using System;

namespace AStar
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using Game1 game = new();
            game.Run();
        }
    }
}
