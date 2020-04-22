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
        private object previouseKeyboardState;
        private object currentKeyboardState;

        private int margin = 10;

        Rectangle pauseMenuBox_1;
        Rectangle pauseMenuBox_2;
        Rectangle pauseMenuBox_3;
        private int boxWidth;
        private int boxHeight;
        private int boxPosX;
        private int boxPosY;

        SpriteFont font;
        private Vector2 fontPos;
        private string message = "GameOver :(";
        Button quit;

        public GameOver(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "Pause";
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            boxWidth = 1;
            boxHeight = 300;

            boxPosX = (_graphicsDevice.Viewport.Width / 2) - (boxWidth / 2);
            boxPosY = (_graphicsDevice.Viewport.Height / 2) - (boxHeight / 2);
            pauseMenuBox_1 = new Rectangle(boxPosX, boxPosY, boxWidth, boxHeight);
            pauseMenuBox_2 = pauseMenuBox_1;
            pauseMenuBox_3 = pauseMenuBox_1;
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
            fontPos = new Vector2(boxPosX - font.MeasureString(message).X / 2, boxPosY + margin);
            quit = new Button(content, "buttonLong_beige.png", "Quit", 200, 100, new Vector2(_graphicsDevice.Viewport.Width / 2 - 100, boxPosY + 100), "menuFont");
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

            if (_graphicsDevice.ScissorRectangle.Contains(pauseMenuBox_1))
            {
                //creats a plese eye candy game over menu
                int speed = 30;
                pauseMenuBox_1.Width += speed;
                pauseMenuBox_1.X -= speed/2;

                speed += 30;
                pauseMenuBox_2.Width += speed;
                pauseMenuBox_2.X -= speed / 2;

                speed += 30;
                pauseMenuBox_3.Width += speed;
                pauseMenuBox_3.X -= speed / 2;
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
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch, pauseMenuBox_3, Color.Black);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch, pauseMenuBox_2, Color.Cyan);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch, pauseMenuBox_1, Color.Coral);
            spriteBatch.DrawString(font, message, fontPos, Color.White);
            quit.Draw(spriteBatch, Color.White);
        }
    }
}
