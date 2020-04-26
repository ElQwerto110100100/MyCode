using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TRotS.GamesStates.States
{
    class LevelSelect : GameState
    {
        Button level1;

        public LevelSelect(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "LevelSelect";
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            //need a more effectient solution, somehow in a list
            level1 = new Button(content, "buttonLong_beige.png", "Level 1", 200, 100, new Vector2((_graphicsDevice.Viewport.Width / 2) - 100, 50), "menuFont_20");
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            if (level1.IsPressed(MouseClass.Instance.GetState(), MouseClass.Instance.GetPrevState()))
            {
                StateManager.Instance.AddScreen(new Levels.Level1(_graphicsDevice, _graphicsDeviceManager));
            }

        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Aqua);
            level1.Draw(spriteBatch);
        }
    }
}