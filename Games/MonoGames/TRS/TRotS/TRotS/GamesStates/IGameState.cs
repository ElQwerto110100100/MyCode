using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//https://rareelementgames.wordpress.com/2017/04/21/game-state-management/
//this creates a blueprint for a class to use
namespace TRotS.GamesStates
{
   interface IGameState
    {
        // Initialize the game settings here      
        void Initialize();

        // Load all content here
        void LoadContent(ContentManager content);

        // Unload any content here
        void UnloadContent();

        // Updates the game
        void Update(GameTime gameTime);

        // Draws the game
        void Draw(SpriteBatch spriteBatch);
    }
}