﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MarkTut1.Resources;
using TRotS.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            CovidSpore = new Enemy(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\COVID-19.png")
            );

            PlayerPlane.PosX = -20;
            PlayerPlane.PosY = _graphicsDevice.Viewport.Height / 2 + PlayerPlane.sourceRectangle.Height / 2 + margin;

            CovidSpore.PosX = -100;
            CovidSpore.PosY = _graphicsDevice.Viewport.Height / 2 + CovidSpore.sourceRectangle.Height / 2 + margin * 2;
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
                if (CovidSpore.PosX != -100)
                {
                    PlayerPlane.Update(gameTime);
                    PlayerPlane.PosX -= 5;
                    CovidSpore.Update(gameTime);
                    CovidSpore.PosX -= 5;
                }

                if (CovidSpore.PosX < assighmentStrLength.X  && assighmentStrColor.A < 225)
                {
                    assighmentStrColor.A += 3;
                }
            }
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.White);
            PlayerPlane.PlayerDraw(spriteBatch);
            spriteBatch.DrawString(font, creditString, creditFontPos, introColor);
            spriteBatch.DrawString(font, assighmentString, assighmentFontPos, assighmentStrColor);
            PlayerPlane.PlayerDraw(spriteBatch, planeEx);
            CovidSpore.Draw(spriteBatch, SpriteEffects.None);
        }
    }
}
