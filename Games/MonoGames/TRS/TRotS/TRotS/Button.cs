using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS
{
    class Button
    {
        public string SpriteTextureName;
        public string Message;
        public int Width;
        public int Height;

        public Button(string spriteTextureName, string message, int width, int height)
        {
            this.SpriteTextureName = spriteTextureName;
            this.Message = message;
            this.Width = width;
            this.Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheetExtract spriteSheetExtract, Vector2 Pos, Vector2 scale)
        {
            spriteSheetExtract.Draw(spriteBatch, Pos, scale, SpriteTextureName);
        }
    }
}
