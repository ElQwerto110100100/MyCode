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
            start.Draw(spriteBatch);
            spriteBatch.DrawString(fontstyle, title, titlePos, Color.White);
            spriteBatch.DrawString(fontstyle, "Press 'H' for Help", new Vector2(0, 50) + titlePos, Color.White);
            spriteBatch.DrawString(fontstyle, "Press 'P' to pause", new Vector2(0, 100) + titlePos, Color.White);
            spriteBatch.DrawString(fontstyle, "Press 'esc' to quit", new Vector2(0, 150) + titlePos, Color.White);

        }
    }
}
