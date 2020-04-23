using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MarkTut1.Resources;
using TRotS.Entity;

namespace TRotS.GamesStates.States
{
    class HelpScreen : GameState
    {
        Player HelpPlayer;
        Enemy HelpCovid;
        Ammo HelpAmmo;

        Rectangle HelpMenu;
        int HelpMenuWidth;

        Dictionary<Button, string> HelpButtons = new Dictionary<Button, string>();

        public HelpScreen(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            HelpMenuWidth = _graphicsDevice.Viewport.Width / 2;
            HelpMenu = new Rectangle(HelpMenuWidth /2 , 0, HelpMenuWidth, _graphicsDevice.Viewport.Height);


            HelpPlayer = new Player(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane.png")
            );

            HelpCovid = new Enemy(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\COVID-19.png")
            );
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            HelpButtons.Add(new Button(content, "buttonSquare_brown.png", "", 100, 100, new Vector2(0,0), "menuFont"), "UpHelpButton");

            //HelpButtons.Add(new Button(content, "buttonSquare_brown.png", "", 100, 100, new Vector2(0, 0), "menuFont"), "DownHelpButton");
            //HelpButtons.Add(new Button(content, "buttonSquare_brown.png", "", 100, 100, new Vector2(0, 0), "menuFont"), "LeftHelpButton");
            //HelpButtons.Add(new Button(content, "buttonSquare_brown.png", "", 100, 100, new Vector2(0, 0), "menuFont"), "RightHelpButton");
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
            //keep previous screen's layer
            StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch,HelpMenu, Color.Black);
        }
    }
}

