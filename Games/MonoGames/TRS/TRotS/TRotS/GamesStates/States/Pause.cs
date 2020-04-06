using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TRotS.GamesStates.States
{
    class Pause : GameState
    {
        SpriteFont font;

        private object previouseKeyboardState;
        private object currentKeyboardState;

        public Pause(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "Pause";
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            previouseKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                StateManager.Instance.RemoveScreen();
            }
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            //keep previous layer
            StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);
            spriteBatch.DrawString(font, "Pause screen", new Vector2(0,0), Color.White);
        }
    }
}
