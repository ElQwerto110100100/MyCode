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

        private int margin = 10;

        Rectangle pauseMenuBox;
        private int boxWidth;
        private int boxHeight;
        private int boxPosX;
        private int boxPosY;

        private Vector2 fontPos;

        Button quit;
        Button resume;

        public Pause(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "Pause";
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            boxWidth = 400;
            boxHeight = 300;

            boxPosX = (_graphicsDevice.Viewport.Width / 2) - (boxWidth / 2);
            boxPosY = (_graphicsDevice.Viewport.Height / 2) - (boxHeight / 2);

            pauseMenuBox = new Rectangle(boxPosX, boxPosY, boxWidth, boxHeight);
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
            fontPos = new Vector2(boxPosX + boxWidth / 2 - font.MeasureString("Pause").X / 2, boxPosY + margin);
            resume = new Button(content, "buttonLong_beige.png", "Resume", 200, 100, new Vector2(boxPosX + boxWidth/2 - 100, font.LineSpacing + fontPos.Y + margin), "menuFont_20");
            quit = new Button(content, "buttonLong_beige.png", "Exit level", 200, 100, new Vector2(boxPosX + boxWidth / 2 - 100, resume.BottomPos.Y + margin), "menuFont_20");
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

            if (resume.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                StateManager.Instance.RemoveScreen();
            }

            if (quit.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                //return to previous screen
                StateManager.Instance._screens.Skip(1).First().UnloadContent();
                StateManager.Instance.ChangeScreen(StateManager.Instance._screens.Skip(2).First());
            }
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            //keep previous screen's layer
            StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch, pauseMenuBox, Color.Black);
            spriteBatch.DrawString(font, "Pause", fontPos, Color.White);
            resume.Draw(spriteBatch);
            quit.Draw(spriteBatch);
            RC_Framework.LineBatch.drawLetterbox(spriteBatch,pauseMenuBox, 3, Color.White);
        }
    }
}
