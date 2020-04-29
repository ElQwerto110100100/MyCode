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
        List<Enemy> Enemies = new List<Enemy>();
        int numOfEnemies = 3;

        List<Sprite> tempList = new List<Sprite>();
        Ammo AmmoCrates;

        private SpriteFont font;

        public Level1(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            for (int i = 0; i < numOfEnemies; i++)
            {
                Enemies.Add(new Enemy(_graphicsDevice, RC_Framework.Util.texFromFile(
                    _graphicsDevice,
                    @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\COVID-19.png")
                    ));
            }

            AmmoCrates = new Ammo(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice, 
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\AmmoCrate.png")
                );

            MainPlayer = new Player(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice, 
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane.png")
                );

            background = RC_Framework.Util.texFromFile(
                _graphicsDevice, 
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\SkyBg.jpeg"
                );
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/menuFont_20");
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
                if (MainPlayer.health == 0)
                {
                    StateManager.Instance.AddScreen(new GameOver(_graphicsDevice, _graphicsDeviceManager));
                    freeze = !freeze;
                }

                if (MouseClass.Instance.GetKeyState().IsKeyDown(Keys.P))
                {
                    StateManager.Instance.AddScreen(new Pause(_graphicsDevice, _graphicsDeviceManager));
                    freeze = !freeze;
                }

                if (MouseClass.Instance.GetKeyState().IsKeyDown(Keys.H))
                {
                    StateManager.Instance.AddScreen(new HelpScreen(_graphicsDevice, _graphicsDeviceManager));
                    freeze = !freeze;
                }

                MainPlayer.UpdatePlayer(gameTime);

                foreach (Enemy enemy in Enemies)
                { 
                    enemy.EnemyUpdate(gameTime);
                    tempList = MainPlayer.bullets.FindAll(bullets => bullets.tempRect.Intersects(enemy.tempRect));
                    MainPlayer.bullets.RemoveAll(bullets => bullets.tempRect.Intersects(enemy.tempRect));
                    enemy.Damage(tempList);

                    // update score for every hit
                    MainPlayer.score += 100 * tempList.Count;

                    if (MainPlayer.tempRect.Intersects(enemy.tempRect))
                    {
                        MainPlayer.PlaneHit();
                        enemy.Exsplosed();
                        enemy.Reset(enemy);
                    }
                }

                if (MainPlayer.tempRect.Intersects(AmmoCrates.tempRect))
                {
                    AmmoCrates.Collected();
                    MainPlayer.ammunation += AmmoCrates.ammoRefill;
                    if (MainPlayer.ammunation > 20)
                    {
                        MainPlayer.ammunation = 20;
                    }
                }

                AmmoCrates.AmmoUpdate(gameTime);
            }
            freeze = false;
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Brown);
            spriteBatch.Draw(background, new Rectangle(0,0,_graphicsDevice.Viewport.Width,_graphicsDevice.Viewport.Height),Color.White);
            MainPlayer.PlayerDraw(spriteBatch);
            foreach (Enemy enemy in Enemies)
            {
                enemy.EnemyDraw(spriteBatch);
            }

            AmmoCrates.Draw(spriteBatch, SpriteEffects.None);

            spriteBatch.DrawString(font, 
                "Score: " + MainPlayer.score.ToString() + 
                "\nAmmunation: " + 
                MainPlayer.ammunation.ToString(), new Vector2(0,0), Color.White);
        }
    }
}
