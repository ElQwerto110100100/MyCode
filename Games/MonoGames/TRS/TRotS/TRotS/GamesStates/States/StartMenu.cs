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
        GraphicsDeviceManager graphics;
        Button start;
        public StartMenu(GraphicsDevice graphicsDevice, GraphicsDeviceManager Graphics) : base(graphicsDevice)
        {
            graphics = Graphics;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {

            SpriteSheet.Instance.AddSpriteSheet(
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.png",
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.xml");
            start = new Button(content, "buttonLong_beige.png", "START", 200, 100, new Vector2((_graphicsDevice.Viewport.Width/ 2) - 100 , (_graphicsDevice.Viewport.Height / 2) - 50), "menuFont");
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");

        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (start.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                StateManager.Instance.AddScreen(new LevelSelect(_graphicsDevice));
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.White);
            start.Draw(spriteBatch);
            
        }
    }
}
