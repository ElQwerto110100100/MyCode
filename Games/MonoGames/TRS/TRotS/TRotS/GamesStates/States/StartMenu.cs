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
        public StartMenu(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {

        }
        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            fontstyle = content.Load<SpriteFont>("Fonts/menuFont_20");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.PaleVioletRed);
            spriteBatch.Begin();
            spriteBatch.DrawString(fontstyle,"YEET", new Vector2(0,0), Color.White);
            spriteBatch.End();
        }
    }
}
