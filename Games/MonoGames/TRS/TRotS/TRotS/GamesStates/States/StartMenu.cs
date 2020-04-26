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
            titlePos = new Vector2(_graphicsDevice.Viewport.Width, 50);
        }

        public override void LoadContent(ContentManager content)
        {

            SpriteSheet.Instance.AddSpriteSheet(
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.png",
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.xml");
            start = new Button(content, "buttonLong_beige.png", "START", 200, 100, new Vector2((_graphicsDevice.Viewport.Width/ 2) - 100 , (_graphicsDevice.Viewport.Height / 2) - 50), "menuFont_20");
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");

            titlePos.X -= fontstyle.MeasureString(title).X/2;
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
            spriteBatch.DrawString(fontstyle, title, new Vector2(50, 50), Color.White);
            
        }
    }
}
