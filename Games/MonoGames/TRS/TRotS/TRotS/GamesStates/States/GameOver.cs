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
    class GameOver : GameState
    {
        SpriteFont font;

        private object previouseKeyboardState;
        private object currentKeyboardState;

        private int margin = 10;

        Rectangle pauseMenuBox;
        private int boxWidth;
        private int boxHeight;
        private int boxPosX;
        private int boxPosY;

        private Vector2 fontPos;

        Button quit;

        public GameOver(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "Pause";
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            boxWidth = _graphicsDevice.Viewport.Width;
            boxHeight = 300;

            boxPosX = 0;
            boxPosY = (_graphicsDevice.Viewport.Height / 2) - (boxHeight / 2);

            fontPos = new Vector2(boxPosX + margin, boxPosY + margin);
            pauseMenuBox = new Rectangle(boxPosX, boxPosY, boxWidth, boxHeight);
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
            quit = new Button(content, "buttonLong_beige.png", "Quit", 200, 100, new Vector2(boxWidth / 2 - 100, boxPosY + 100), "menuFont");
        }

        // Unload any content here
        public override void UnloadContent()
        {
            StateManager.Instance.RemoveScreen();
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

            if (quit.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                //get rid of previous screen
                StateManager.Instance.ClearScreens();
            }
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            //keep previous screen's layer
            StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch, pauseMenuBox, Color.DarkGreen);
            spriteBatch.DrawString(font, "GameOver :(", fontPos, Color.White);
            quit.Draw(spriteBatch);
        }
    }
}
