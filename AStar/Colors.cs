using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public static class Colors
    {
        private static Dictionary<Color, Texture2D> rectangles = new();
        private static GraphicsDeviceManager _graphics;

        public static void Init(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;

            Texture2D rect;

            rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
            rectangles.Add(Color.White, rect);

            rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.DimGray });
            rectangles.Add(Color.DimGray, rect);

            rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.LightGreen });
            rectangles.Add(Color.LightGreen, rect);

            rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.Red });
            rectangles.Add(Color.Red, rect);

            rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.Cyan });
            rectangles.Add(Color.Cyan, rect);

            rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.Yellow });
            rectangles.Add(Color.Yellow, rect);
        }

        public static Texture2D GetTexture(Color color) => rectangles[color];
    }
}
