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
        int num = 0;
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
            start = new Button(content, "buttonLong_beige.png", "START", 200, 100, new Vector2(200, 200), "menuFont");
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            num++;
            if (start.IsPressed(MouseClass.Instance.GetState()))
            {
                StateManager.Instance.AddScreen(new basic(_graphicsDevice));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.PaleVioletRed);
            start.Draw(spriteBatch);
            spriteBatch.DrawString(fontstyle, num.ToString(), new Vector2(0,0), Color.White);
            MouseClass.Instance.Draw(spriteBatch);
        }
    }
}
