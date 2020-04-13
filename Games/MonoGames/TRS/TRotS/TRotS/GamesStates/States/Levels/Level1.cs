using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TRotS.GamesStates.States.Levels
{
    class Level1 : GameState
    {
        
        
        private object previouseKeyboardState;
        private object currentKeyboardState;

        public Level1(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            Grid.Instance.MakeGrid(3,3,300,300, 50, 50);
            
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            StateManager.Instance.AddScreen(new Overlays.RoomSelector(_graphicsDevice));
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {


            previouseKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                StateManager.Instance.ChangeScreen(new Pause(_graphicsDevice));
            }
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Brown);
            Grid.Instance.DrawGrid(spriteBatch);            
        }
    }
}
