using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//https://rareelementgames.wordpress.com/2017/04/21/game-state-management/
// based on the blue print from the IGamesStates interface it will allow for multiple state objects created to use this system
namespace TRotS.GamesStates
{
    public abstract class GameState : IGameState
    {
        public string Name;
        protected GraphicsDevice _graphicsDevice;
        protected GraphicsDeviceManager _graphicsDeviceManager;
        public GameState(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphicsDevice = graphicsDevice;
            _graphicsDeviceManager = graphicsDeviceManager;
        }
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
