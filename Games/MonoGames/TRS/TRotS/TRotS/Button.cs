using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace TRotS
{
    class Button
    {
        public string SpriteTextureName;
        public string Message;
        public int Width;
        public int Height;
        public bool isPressed = false;
        string FontName;
        Vector2 Pos;
        SpriteSheetExtract SpriteSheetExtract;
        SpriteFont fontstyle;

        public Button(ContentManager content, SpriteSheetExtract spriteSheetExtract, string spriteTextureName, string message, int width, int height, Vector2 pos, string fontName)
        {
            this.SpriteTextureName = spriteTextureName;
            this.Message = message;
            this.Width = width;
            this.Height = height;
            this.FontName = fontName;
            this.SpriteSheetExtract = spriteSheetExtract;
            this.Pos = pos;
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");
        }

        public void Draw(SpriteBatch spriteBatch, Color? color = null)
        {
            Vector2 scale = new Vector2(Width / SpriteSheetExtract.GetSpritWidth(this.SpriteTextureName), Height / SpriteSheetExtract.GetSpritHeight(this.SpriteTextureName));

            Vector2 stringMeasurement = fontstyle.MeasureString(Message);
            //makeing the text size and positon relative to the button object
            Vector2 centre = new Vector2((Width / 2), Height / 2 - 5) - (stringMeasurement / 2) + Pos;

            SpriteSheetExtract.Draw(spriteBatch, Pos, scale, SpriteTextureName, color);
            spriteBatch.DrawString(fontstyle, Message, centre, Color.White);
        }
    }
}
