using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarkTut1.Resources;
using TRotS.Entity;

namespace TRotS.GamesStates.States.Levels
{
    class Level1 : GameState
    {
        bool freeze = false;
        Texture2D background;

        Player MainPlayer;
        Enemy Enemies;

        public Level1(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            Enemies = new Enemy(_graphicsDevice, RC_Framework.Util.texFromFile(_graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\COVID-19.png"))
            {
                NumOfEnimes = 4
            };
            MainPlayer = new Player(_graphicsDevice, RC_Framework.Util.texFromFile(_graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane.png"));
            background = RC_Framework.Util.texFromFile(_graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\SkyBg.jpeg");
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
        }

        // Unload any content here
        public override void UnloadContent()
        {
            StateManager.Instance.RemoveScreen();
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            if (!freeze)
            {
                if (MouseClass.Instance.GetKeyState().IsKeyDown(Keys.P))
                {
                    StateManager.Instance.AddScreen(new Pause(_graphicsDevice, _graphicsDeviceManager));
                    freeze = !freeze;
                }

                MainPlayer.UpdatePlayer(gameTime);
                Enemies.EnemyUpdate(gameTime);

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
            spriteBatch.Draw(background, new Rectangle(0,0,_graphicsDevice.Viewport.Width,_graphicsDevice.Viewport.Height),Color.White);
            MainPlayer.PlayerDraw(spriteBatch);
            Enemies.EnemyDraw(spriteBatch);

        }
    }
}
