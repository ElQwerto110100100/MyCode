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

        int margin = 10;

        List<Button> HelpButtons = new List<Button>();

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
            Button temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - 50 - margin), margin + 50), "menuFont");
            temp.AttachSprite("arrowSilver_right.png", 0); //right
            HelpButtons.Add(temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 2 - margin), margin), "menuFont");
            // the pi method was from https://stackoverflow.com/questions/10527730/microsoft-xna-texture2d-and-rotation
            temp.AttachSprite("arrowSilver_right.png", (float)Math.PI / -2.0f); // up 
            HelpButtons.Add(temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 3 - margin), margin + temp.Height), "menuFont");
            temp.AttachSprite("arrowSilver_right.png", ((float)Math.PI / -2.0f) * 2); // left
            HelpButtons.Add(temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 2 - margin), margin + temp.Height * 2), "menuFont");
            temp.AttachSprite("arrowSilver_right.png", ((float)Math.PI / 2.0f)); // down
            HelpButtons.Add(temp);
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
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch,HelpMenu, Color.Coral);
            foreach (Button butt in HelpButtons)
            {
                butt.Draw(spriteBatch);
            }
        }
    }
}

