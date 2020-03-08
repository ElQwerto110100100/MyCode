using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://rareelementgames.wordpress.com/2017/04/21/game-state-management/
namespace TRotS.GamesStates
{
    public class StateManager
    {

        // Instance of the game state manager     
        private static StateManager _instance;
        private ContentManager _content;

        // Stack for the screens     
        //can use stack methods to maipulate and move screens around
        private Stack<GameState> _screens = new Stack<GameState>();

        public static StateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateManager();
                }
                return _instance;
            }
        }

        // Sets the content manager
        public void SetContent(ContentManager content)
        {
            _content = content;
        }

        // Adds a new screen to the stack 
        public void AddScreen(GameState screen)
        {
            // Add the screen to the stack
            _screens.Push(screen);
                // Initialize the screen
            _screens.Peek().Initialize();
                // Call the LoadContent on the screen
            if (_content != null)
            {
                _screens.Peek().LoadContent(_content);
            }
        }

        // Removes the top screen from the stack
        public void RemoveScreen()
        {
            if (_screens.Count > 0)
            {
                var screen = _screens.Peek();
                _screens.Pop();
            }
        }

        // Clears all the screen from the list
        public void ClearScreens()
        {
            while (_screens.Count > 0)
            {
                _screens.Pop();
            }
        }

        // Removes all screens from the stack and adds a new one 
        public void ChangeScreen(GameState screen)
        {
            ClearScreens();
            AddScreen(screen);
        }

        // Updates the top screen. 
        public void Update(GameTime gameTime)
        {
            if (_screens.Count > 0)
            {
                _screens.Peek().Update(gameTime);
            }
        }

        // Renders the top screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_screens.Count > 0)
            {
                _screens.Peek().Draw(spriteBatch);
            }
        }

        // Unloads the content from the screen
        public void UnloadContent()
        {
            foreach (GameState state in _screens)
            {
                state.UnloadContent();
            }
        }
    }
}