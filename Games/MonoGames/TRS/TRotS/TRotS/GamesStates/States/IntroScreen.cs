using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS.GamesStates.States
{
    class IntroScreen : GameState
    {
        string introString = "Created by Josua Hoban";
        float introStringAlpha = 1.0f;
        SpriteFont font;
        Vector2 fontPos;
        int margin = 10;

        //this will be my intro screen that will play a short makeshift 'cutscene' to give it a more complete feel
        public IntroScreen(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
            fontPos = new Vector2(
                _graphicsDevice.Viewport.Width - font.MeasureString(introString).X / 2, 
                _graphicsDevice.Viewport.Width - font.MeasureString(introString).Y / 2
                );
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(font,introString, fontPos, new Color(Color.White, introStringAlpha));
        }
    }
}
