using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MarkTut1.Resources;
using TRotS.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace TRotS.GamesStates.States
{
    class IntroScreen : GameState
    {
        string creditString = "Created by Josua Hoban";
        string assighmentString = "For game programing assighment";

        Color introColor;
        Color assighmentStrColor;

        SpriteFont font;
        Vector2 creditFontPos;
        Vector2 assighmentFontPos;

        Vector2 assighmentStrLength;

        int margin = 10;

        Player PlayerPlane;
        SpriteEffects planeEx = SpriteEffects.None; 
        Enemy CovidSpore;

        bool phase1 = true;
        bool phase2 = false;

        //this will be my intro screen that will play a short makeshift 'cutscene' to give it a more complete feel
        public IntroScreen(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            introColor = new Color(0, 0, 0, 0);
            assighmentStrColor = new Color(0, 0, 0, 0);

            PlayerPlane = new Player(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane.png")
            );
            PlayerPlane.health = 0;

            CovidSpore = new Enemy(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\COVID-19.png")
            );

            PlayerPlane.PosX = -20;
            PlayerPlane.PosY = _graphicsDevice.Viewport.Height / 2 + PlayerPlane.sourceRectangle.Height / 2 + (margin * 2);

            CovidSpore.PosX = -300;
            CovidSpore.PosY = _graphicsDevice.Viewport.Height / 2 + CovidSpore.sourceRectangle.Height / 2 + (margin * 2);
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
            creditFontPos = new Vector2(
                _graphicsDevice.Viewport.Width / 2 - font.MeasureString(creditString).X / 2, 
                _graphicsDevice.Viewport.Height / 2 - font.MeasureString(creditString).Y / 2
                );

            assighmentStrLength = font.MeasureString(assighmentString);

            assighmentFontPos = new Vector2(
                _graphicsDevice.Viewport.Width / 2 - assighmentStrLength.X / 2,
                _graphicsDevice.Viewport.Height / 2 - assighmentStrLength.Y / 2 + margin * 13
                );

        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            if (phase1)
            {
                    PlayerPlane.Update(gameTime);
                    PlayerPlane.PosX += 5;
                    CovidSpore.Update(gameTime);
                    CovidSpore.PosX += 5;
                //play next segment a bit earlier
                if (PlayerPlane.PosX > creditFontPos.X && introColor.A < 225)
                {
                    introColor.A += 3;
                }
            }

            if (CovidSpore.PosX >= _graphicsDevice.Viewport.Width + 50 && introColor.A == 225)
            {
                phase1 = false;
                phase2 = true;
            }

            if (phase2)
            {
                planeEx = SpriteEffects.FlipHorizontally;
                if (PlayerPlane.PosX != -150)
                {
                    PlayerPlane.Update(gameTime);
                    PlayerPlane.PosX -= 5;
                    CovidSpore.Update(gameTime);
                    CovidSpore.PosX -= 5;
                }
                else
                {
                    phase1 = false;
                    phase2 = false;
                }

                if (CovidSpore.PosX < assighmentStrLength.X  && assighmentStrColor.A < 225)
                {
                    assighmentStrColor.A += 3;
                }
            }
            //adding a skip gives it more of a complete feel
            if (!phase1 && !phase2 || MouseClass.Instance.IsKeyPressed(Keys.Space))
            {
                //once intro is over start the game
                StateManager.Instance.AddScreen(new StartMenu(_graphicsDevice, _graphicsDeviceManager));
            }
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.White);
            spriteBatch.DrawString(font, "Please Space to skip", new Vector2(10,10), Color.Black);
            spriteBatch.DrawString(font, creditString, creditFontPos, introColor);
            spriteBatch.DrawString(font, assighmentString, assighmentFontPos, assighmentStrColor);
            PlayerPlane.PlayerDraw(spriteBatch, planeEx);
            CovidSpore.Draw(spriteBatch, SpriteEffects.None);
        }
    }
}
