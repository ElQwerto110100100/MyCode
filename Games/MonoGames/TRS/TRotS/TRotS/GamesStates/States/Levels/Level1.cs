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
        bool freeze = false;
        private object previouseKeyboardState;
        private object currentKeyboardState;

        Texture2D obj;
        int xx = 0;
        int yy = 0;

        public Level1(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
        }


        // Initialize the game settings here      
        public override void Initialize()
        {
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            obj = RC_Framework.Util.texFromFile(_graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\Test_Room.png");
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            if (!freeze)
            {
                previouseKeyboardState = currentKeyboardState;
                currentKeyboardState = Keyboard.GetState();

                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    StateManager.Instance.AddScreen(new Pause(_graphicsDevice, _graphicsDeviceManager));
                    freeze = !freeze;
                }

                xx += 2;
                yy += 2;
            }
            else if (StateManager.Instance._screens.Peek().Name != "Pause")
            {
                //do nthing and wait for the pause to finish
                freeze = !freeze;
            }
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Brown);
            spriteBatch.Draw(obj,new Rectangle(xx,yy,200,200),Color.White);
        }
    }
}
