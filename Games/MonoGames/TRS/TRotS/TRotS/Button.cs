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
        public bool visable = true;
        string FontName;
        Vector2 Pos;
        int textOffset = 5;
        SpriteSheet SpriteSheetExtract;
        SpriteFont fontstyle;

        public Button(ContentManager content, string spriteTextureName, string message, int width, int height, Vector2 pos, string fontName)
        {
            this.SpriteTextureName = spriteTextureName;
            this.Message = message;
            this.Width = width;
            this.Height = height;
            this.FontName = fontName;
            this.Pos = pos;
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");
        }

        public void Draw(SpriteBatch spriteBatch, Color? color = null)
        {
            if (visable == true)
            {
                Vector2 scale = new Vector2(Width / SpriteSheet.Instance.GetSpritWidth(this.SpriteTextureName), Height / SpriteSheet.Instance.GetSpritHeight(this.SpriteTextureName));

                Vector2 stringMeasurement = fontstyle.MeasureString(Message);
                //makeing the text size and positon relative to the button object
                Vector2 centre = new Vector2((Width / 2), (Height / 2 - textOffset)) - (stringMeasurement / 2) + Pos;

                SpriteSheet.Instance.Draw(spriteBatch, Pos, scale, SpriteTextureName, color);
                spriteBatch.DrawString(fontstyle, Message, centre, Color.White);
            } else
            {
                //dont draw it
            }
        }

        public void IsVisable(bool flag)
        {
            visable = flag;
        }

        public bool IsPressed(MouseState currentMouseState)
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && 
                currentMouseState.X >= Pos.X && 
                currentMouseState.X <= (Width + Pos.X) && 
                currentMouseState.Y >= Pos.Y && 
                currentMouseState.Y <= (Height + Pos.Y))
            {
                if (!SpriteTextureName.Contains("_pressed.png"))
                {
                    SpriteTextureName = SpriteTextureName.Replace(".png", "_pressed.png");
                    textOffset = 0;
                    return true;
                } else
                {
                    return true;
                }
            }
            else
            {
                this.SpriteTextureName = this.SpriteTextureName.Replace("_pressed.png", ".png");
                textOffset = 5;
                return false;
            }
        }
    }
}
