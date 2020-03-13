using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//this class is a base for any other class i need to make
namespace TRotS.GamesStates.States
{
    class basic : GameState
    {
        public basic(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            //SpriteSheet.Instance.AddSpriteSheet();
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
            _graphicsDevice.Clear(Color.Aqua);
        }
    }
}
