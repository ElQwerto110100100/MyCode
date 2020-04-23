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

        private Texture2D background;
        Rectangle HelpRect;

        Rectangle HelpMenu;
        int HelpMenuWidth;

        int margin = 50;

        Dictionary<string, Button> HelpButtons = new Dictionary<string, Button>();

        private int waitTimer = 0;
        private int waitSec = 40;
        private bool Phase1 = true;

        public HelpScreen(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            HelpMenuWidth = _graphicsDevice.Viewport.Width / 2;
            HelpMenu = new Rectangle(HelpMenuWidth /2 , 0, HelpMenuWidth, _graphicsDevice.Viewport.Height);

            HelpRect = new Rectangle(HelpMenuWidth / 2 + margin / 2, margin, 200, 150);


            HelpPlayer = new Player(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane.png")
            );

            HelpCovid = new Enemy(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\COVID-19.png")
            );

            HelpAmmo = new Ammo(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\AmmoCrate.png")
                );

            background = RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\SkyBg.jpeg"
                );
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            Button temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - 50 - margin), margin + 50), "menuFont");
            temp.AttachSprite("arrowSilver_right.png", 0); //right
            HelpButtons.Add("RightArrow", temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 2 - margin), margin), "menuFont");
            // the pi method was from https://stackoverflow.com/questions/10527730/microsoft-xna-texture2d-and-rotation so the arrow can be rotated
            temp.AttachSprite("arrowSilver_right.png", (float)Math.PI / -2.0f); // up 
            HelpButtons.Add("UpArrow", temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 3 - margin), margin + temp.Height), "menuFont");
            temp.AttachSprite("arrowSilver_right.png", ((float)Math.PI / -2.0f) * 2); // left
            HelpButtons.Add("LeftArrow",temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 2 - margin), margin + temp.Height * 2), "menuFont");
            temp.AttachSprite("arrowSilver_right.png", ((float)Math.PI / 2.0f)); // down
            HelpButtons.Add("DownArrow",temp);

            foreach (KeyValuePair<string, Button> butt in HelpButtons)
            {
                butt.Value.disabled = true;  
            }
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            HelpPlayer.Update(gameTime);
            HelpCovid.Update(gameTime);
            HelpAmmo.Update(gameTime);

            //start phase of movement tutorial
            if (Phase1)
            {
                HelpPlayer.PosX = HelpRect.X + (HelpRect.Width / 2) - (HelpPlayer.sourceRectangle.Width / 2);
                HelpPlayer.PosY = HelpRect.Y + (HelpRect.Height / 2) - (HelpPlayer.sourceRectangle.Height / 2);
                Phase1 = false;
            }           

            if (waitTimer >= waitSec && waitTimer <= waitSec * 2)
            {
                HelpButtons["RightArrow"].SetToPressed();
                HelpPlayer.PosX++;
            }
            else if (waitTimer >= waitSec * 2 && waitTimer <= waitSec * 3)
            {
                HelpButtons["RightArrow"].SetToUnpressed();
                HelpButtons["UpArrow"].SetToPressed();
                HelpPlayer.PosY--;
            }
            else if (waitTimer >= waitSec * 3 && waitTimer <= waitSec * 5)
            {
                HelpButtons["UpArrow"].SetToUnpressed();
                HelpButtons["LeftArrow"].SetToPressed();
                HelpPlayer.PosX--;
            }
            else if (waitTimer >= waitSec * 5 && waitTimer <= waitSec * 6)
            {
                HelpButtons["LeftArrow"].SetToUnpressed();
                HelpButtons["DownArrow"].SetToPressed();
                HelpPlayer.PosY++;
            }
            else if (waitTimer >= waitSec *6 && waitTimer <= waitSec * 7)
            {
                HelpButtons["DownArrow"].SetToUnpressed();
                HelpButtons["RightArrow"].SetToPressed();
                HelpPlayer.PosX++;
            }
            else if (waitTimer >= waitSec * 6)
            {
                waitTimer = 0;
                foreach (KeyValuePair<string, Button> butt in HelpButtons)
                {
                    butt.Value.SetToUnpressed();
                }
            }
            waitTimer++;

        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            //keep previous screen's layer
            StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch,HelpMenu, Color.DarkGoldenrod);

            spriteBatch.Draw(background, HelpRect, new Rectangle(100,100, HelpRect.Width, HelpRect.Height) ,Color.White);

            foreach (KeyValuePair<string, Button> butt in HelpButtons)
            {
                butt.Value.Draw(spriteBatch);
            }
            HelpPlayer.PlayerDraw(spriteBatch);
        }
    }
}

