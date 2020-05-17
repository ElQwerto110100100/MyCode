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
        Vector2 scale;

        string FontName;
        Color FontColor;
        public Vector2 Pos;
        public Vector2 BottomPos;

        Rectangle rec;
        SpriteFont fontstyle;
        Vector2 stringMeasurement;

        string attachSpriteName;
        float attachSprtireRotation = 0;
        Vector2 buttonCentre;
        Vector2 centre;

        public bool disabled { get; set; } = false;

        public Button(ContentManager content, string spriteTextureName, string message, int width, int height, Vector2 pos, string fontName, Color? fontcolor = null)
        {
            this.SpriteTextureName = spriteTextureName;
            this.Message = message;
            this.Width = width;
            this.Height = height;
            this.FontName = fontName;
            this.Pos = pos;
            this.FontColor = fontcolor ?? Color.White;

            fontstyle = content.Load<SpriteFont>("Fonts/" + fontName);
            BottomPos = new Vector2(pos.X + width, pos.Y + height);
            rec = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            stringMeasurement = fontstyle.MeasureString(Message);
            scale = new Vector2(Width / SpriteSheet.Instance.GetSpritWidth(this.SpriteTextureName), Height / SpriteSheet.Instance.GetSpritHeight(this.SpriteTextureName));
            centre = new Vector2((Width / 2), (Height / 2) - 5) - (stringMeasurement / 2) + Pos;
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
            if (!disabled)
            {
                //eye candy feature to look like button is pressed
                if (MouseClass.Instance.GetState().LeftButton == ButtonState.Pressed &&
                    MouseClass.Instance.GetRect().Intersects(rec))
                {
                    SetToPressed();
                }
                else
                {
                    SetToUnpressed();
                }

                //eye candy fature so its notices if selected
                if (MouseClass.Instance.GetRect().Intersects(rec))
                {
                    RC_Framework.LineBatch.drawLineRectangleOuter(spriteBatch, rec, Color.White, 3);
                }
            }
            SpriteSheet.Instance.Draw(spriteBatch, Pos, scale, SpriteTextureName, new Color(color ?? (isPressed ? Color.BlanchedAlmond : Color.White), alpha ?? 1f));
            spriteBatch.DrawString(fontstyle, Message, isPressed ? new Vector2(centre.X, centre.Y + 5) : centre, FontColor);

            if (attachSpriteName != null)
            {
                //nudge the arrow down to show movement of the button pres
                SpriteSheet.Instance.Draw(
                    spriteBatch,
                    isPressed ? new Vector2(buttonCentre.X, (buttonCentre.Y + 2)) : buttonCentre,
                    new Vector2(1, 1),
                    attachSpriteName,
                    new Color(color ?? Color.White, alpha ?? 1f),
                    attachSprtireRotation,
                    true
                    );
            }

        }

        public void SetToPressed()
        {
            if (!SpriteTextureName.Contains("_pressed.png"))
            {
                if (!isPressed)
                {
                    SpriteTextureName = SpriteTextureName.Replace(".png", "_pressed.png");
                    scale = new Vector2(Width / SpriteSheet.Instance.GetSpritWidth(this.SpriteTextureName), Height / SpriteSheet.Instance.GetSpritHeight(this.SpriteTextureName));
                    isPressed = true;
                }
            }
        }

        public void SetToUnpressed()
        {
            if (isPressed)
            {
                SpriteTextureName = SpriteTextureName.Replace("_pressed.png", ".png");
                isPressed = false;
            }
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
                isPressed = false;
                return false;
            }
        }
    }
}
