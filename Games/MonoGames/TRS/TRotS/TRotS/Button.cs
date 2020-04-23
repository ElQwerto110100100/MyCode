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
        public Vector2 Pos;
        public Vector2 BottomPos;
        int textOffset = 5;

        Rectangle rec;
        SpriteFont fontstyle;

        string attachSpriteName;
        float attachSprtireRotation = 0;
        Vector2 buttonCentre;

        public Button(ContentManager content, string spriteTextureName, string message, int width, int height, Vector2 pos, string fontName)
        {
            this.SpriteTextureName = spriteTextureName;
            this.Message = message;
            this.Width = width;
            this.Height = height;
            this.FontName = fontName;
            this.Pos = pos;
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");
            BottomPos = new Vector2(pos.X + width, pos.Y + height);
            rec = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
        }

        public void AttachSprite(string spriteName, float rot = 0)
        {
            attachSprtireRotation = rot;
            attachSpriteName = spriteName;
            buttonCentre = new Vector2((Width / 2), (Height / 2));
            buttonCentre += Pos;
        }

        public void Draw(SpriteBatch spriteBatch, Color? color = null, float? alpha = null)
        {
            if (visable == true)
            {
                //eye candy feature to look like button is pressed
                if (MouseClass.Instance.GetState().LeftButton == ButtonState.Pressed &&
                    MouseClass.Instance.GetRect().Intersects(rec))
                {
                    if (!SpriteTextureName.Contains("_pressed.png"))
                    {
                        SpriteTextureName = SpriteTextureName.Replace(".png", "_pressed.png");
                        textOffset = 0;
                        isPressed = true;
                    }
                }
                else
                {
                    this.SpriteTextureName = this.SpriteTextureName.Replace("_pressed.png", ".png");
                    textOffset = 5;
                    isPressed = false;
                }

                Vector2 scale = new Vector2(Width / SpriteSheet.Instance.GetSpritWidth(this.SpriteTextureName), Height / SpriteSheet.Instance.GetSpritHeight(this.SpriteTextureName));

                Vector2 stringMeasurement = fontstyle.MeasureString(Message);
                //makeing the text size and positon relative to the button object
                Vector2 centre = new Vector2((Width / 2), (Height / 2 - textOffset)) - (stringMeasurement / 2) + Pos;

                SpriteSheet.Instance.Draw(spriteBatch, Pos, scale, SpriteTextureName, new Color(color ?? Color.White, alpha ?? 1f));
                spriteBatch.DrawString(fontstyle, Message, centre, Color.White);

                if (attachSpriteName != null)
                {
                    //nudge the arrow down to show movement of the button press
                    SpriteSheet.Instance.Draw(
                        spriteBatch, 
                        buttonCentre, 
                        new Vector2(1,1), 
                        attachSpriteName, 
                        new Color(color ?? Color.White, alpha ?? 1f),
                        attachSprtireRotation, 
                        true
                        );
                }

                //eye candy fature so its notices if selected
                if (MouseClass.Instance.GetRect().Intersects(rec))
                {
                    RC_Framework.LineBatch.drawLineRectangleOuter(spriteBatch, rec, Color.White, 3);
                }

            }
            else
            {
                //dont draw it
            }
        }

        public void IsVisable(bool flag)
        {
            visable = flag;
        }

        public bool IsPressed(MouseState currentMouseState, MouseState previousMouseState)
        {
            //return true on release
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                currentMouseState.LeftButton != ButtonState.Pressed &&
                MouseClass.Instance.GetRect().Intersects(rec)) {
                isPressed = true;
                return true;
            }
            else
            {
                this.SpriteTextureName = this.SpriteTextureName.Replace("_pressed.png", ".png");
                textOffset = 5;
                isPressed = false;
                return false;
            }
        }
    }
}
