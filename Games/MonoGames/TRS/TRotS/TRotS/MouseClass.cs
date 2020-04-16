using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TRotS
{
    class MouseClass
    {
        private static MouseClass _instance;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
        private KeyboardState previouseKeyboardState;
        private KeyboardState currentKeyboardState;
        private Texture2D mouseTexture;

        public ContentManager _Content;

        //this will alow the mouse to be called anytime
        //if a instance of mouse hasnt been called than crete one else use the current one
        public static MouseClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MouseClass();
                }
                return _instance;
            }
        }

        public void Update()
        {
            //prevents the next button being clicked after the page loads
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            //added in keyboard got tierd of haveing to repeat this
            previouseKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public KeyboardState GetKeyState()
        {
            return currentKeyboardState;
        }

        public KeyboardState GetPrevKeyState()
        {
            return previouseKeyboardState;
        }

        public MouseState GetState()
        {
            return currentMouseState;
        }

        public MouseState GetPrevState()
        {
            return previousMouseState;
        }

        public Rectangle GetRect()
        {
            return new Rectangle(currentMouseState.Position.X, currentMouseState.Position.Y, mouseTexture.Width, mouseTexture.Height);
        }

        public void SetTexture(Texture2D MouseTexture)
        {
            mouseTexture = MouseTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mouseTexture, new Vector2(currentMouseState.X, currentMouseState.Y), null, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
        }
    }
}