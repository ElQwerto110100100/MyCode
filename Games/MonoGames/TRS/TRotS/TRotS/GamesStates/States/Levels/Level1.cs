using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TRotS.GamesStates.States.Levels
{
    class Level1 : GameState
    {
        public Level1(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            Grid.Instance.MakeGrid(5,5,_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {

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
            _graphicsDevice.Clear(Color.Brown);
            Grid.Instance.DrawGrid(spriteBatch);
        }
    }
}
