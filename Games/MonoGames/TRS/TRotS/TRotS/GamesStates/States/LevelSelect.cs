using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRotS.Entity;

namespace TRotS.GamesStates.States
{
    class LevelSelect : GameState
    {
        Button level1;
        Button level2;

        int MenuBoxWidth = 700;
        Rectangle MenuBox;

        Texture2D bg;
        SpriteFont font;
        public LevelSelect(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "LevelSelect";
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            bg = RC_Framework.Util.texFromFile(_graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\levelSelectBG.jpg");
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
            //need a more effectient solution, somehow in a list
            level1 = new Button(content, "buttonLong_beige.png", "Level 1", 200, 100, new Vector2((_graphicsDevice.Viewport.Width / 2) - 100, 50), "menuFont_20");
            level2 = new Button(content, "buttonLong_beige.png", "Level 2", 200, 100, new Vector2((_graphicsDevice.Viewport.Width / 2) - 100, 250), "menuFont_20");

            MenuBox = new Rectangle(_graphicsDevice.Viewport.Width / 2 - MenuBoxWidth / 2, 0, MenuBoxWidth, _graphicsDevice.Viewport.Height);
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            if (level1.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                StateManager.Instance.AddScreen(new Levels.Level1(_graphicsDevice, _graphicsDeviceManager));
            }
            if (level2.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                StateManager.Instance.AddScreen(new Levels.Level2(_graphicsDevice, _graphicsDeviceManager));
            }

        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Aqua);
            spriteBatch.Draw(bg, _graphicsDevice.ScissorRectangle, Color.White);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch, MenuBox, new Color(0,0,0,150));
            RC_Framework.LineBatch.drawLetterbox(spriteBatch, MenuBox, 3, Color.White);
            level1.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Level1 Highscore: " + ScoreBoard.Instance.GetScore("Level1"), 
                new Vector2(_graphicsDevice.Viewport.Width / 2 - font.MeasureString("Level1 Highscore: " + ScoreBoard.Instance.GetScore("Level1")).X / 2, level1.Pos.Y + 110), Color.White);

            level2.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Level2 Highscore: " + ScoreBoard.Instance.GetScore("Level2"), 
                new Vector2(_graphicsDevice.Viewport.Width / 2 - font.MeasureString("Level2 Highscore: " + ScoreBoard.Instance.GetScore("Level2")).X / 2, level2.Pos.Y + 110), Color.White);

        }
    }
}