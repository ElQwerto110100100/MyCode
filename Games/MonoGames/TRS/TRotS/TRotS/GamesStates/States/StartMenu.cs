using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TRotS.GamesStates.States
{
    class StartMenu : GameState
    {
        SpriteFont fontstyle;
        Button start;

        string title = "Shoot down COVID-19!!!!";
        Vector2 titlePos;

        Texture2D bg;

        public StartMenu(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "StartMenu";
        }

        public override void Initialize()
        {
           
        }

        public override void LoadContent(ContentManager content)
        {
            start = new Button(content, "buttonLong_beige.png", "START", 200, 100, new Vector2((_graphicsDevice.Viewport.Width/ 2) - 100 , (_graphicsDevice.Viewport.Height / 2) - 50), "menuFont_20");
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");
            titlePos = new Vector2(_graphicsDevice.Viewport.Width / 2 - fontstyle.MeasureString(title).X / 2, 50);

            //useing a background to fill the splash screen requirment
            bg = RC_Framework.Util.texFromFile(_graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\startMenuBG.jpg");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (start.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                StateManager.Instance.AddScreen(new LevelSelect(_graphicsDevice, _graphicsDeviceManager));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(bg, _graphicsDevice.ScissorRectangle, Color.White);
            start.Draw(spriteBatch);
            spriteBatch.DrawString(fontstyle, title, titlePos, Color.White);
            //a bit of overhead however there isnt going to be much of anything else on screen so it should be fine like this
            spriteBatch.DrawString(fontstyle, "Press 'H' for Help", new Vector2(_graphicsDevice.Viewport.Width / 2 - fontstyle.MeasureString("Press 'H' for Help").X / 2, titlePos.Y + 50), Color.White);
            spriteBatch.DrawString(fontstyle, "Press 'P' to pause", new Vector2(_graphicsDevice.Viewport.Width / 2 - fontstyle.MeasureString("Press 'P' to pause").X / 2, titlePos.Y + 100), Color.White);
            spriteBatch.DrawString(fontstyle, "Press 'esc' to quit", new Vector2(_graphicsDevice.Viewport.Width / 2 - fontstyle.MeasureString("Press 'esc' to quit").X / 2, titlePos.Y + 150), Color.White);
            //didnt want to use any special background so a simplist one with a nice border should be fine
            RC_Framework.LineBatch.drawLetterbox(spriteBatch, _graphicsDevice.ScissorRectangle, 3, Color.White);

        }
    }
}
