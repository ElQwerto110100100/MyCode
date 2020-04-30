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
using Microsoft.Xna.Framework.Input;

namespace TRotS.GamesStates.States
{
    class HelpScreen : GameState
    {
        Player HelpPlayer;
        Enemy HelpCovid;
        Ammo HelpAmmo;
        TolietPaper HelpTolietPaper;
        SpikeBall HelpSpikeBall;
        Sprite DemoBullet;

        private Texture2D background;
        Rectangle BackgroundRec;
        Rectangle HelpMoveRec;
        Rectangle HelpAvoidRec;
        Rectangle HelpSuppliesRec;

        Rectangle HelpMenu;
        int HelpMenuWidth;

        int margin = 50;

        Dictionary<string, Button> HelpButtons = new Dictionary<string, Button>();
        Button CloseButton;

        private int waitTimer = 0;
        private int waitSec = 40;
        private bool Phase1 = true;

        private SpriteFont font;
        //http://rbwhitaker.wikidot.com/render-to-texture
        // Create a new render target
        RenderTarget2D renderTarget;

        public HelpScreen(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, graphicsDeviceManager)
        {
            Name = "HelpScreen";
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            HelpMenuWidth = _graphicsDevice.Viewport.Width / 2;
            HelpMenu = new Rectangle(HelpMenuWidth / 2, 0, HelpMenuWidth, _graphicsDevice.Viewport.Height);

            HelpMoveRec = new Rectangle((HelpMenuWidth / 2) + (margin / 2), margin, 200, 150);
            HelpAvoidRec = new Rectangle(HelpMoveRec.X, HelpMoveRec.Bottom + margin, HelpMoveRec.Width, HelpMoveRec.Height);
            HelpSuppliesRec = new Rectangle(HelpAvoidRec.X, HelpAvoidRec.Bottom + margin, HelpMoveRec.Width, HelpMoveRec.Height);

            HelpPlayer = new Player(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane.png")
            );

            HelpPlayer.health = 0;

            HelpCovid = new Enemy(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\COVID-19.png")
            );

            HelpAmmo = new Ammo(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\AmmoCrate.png")
                );

            HelpSpikeBall = new SpikeBall(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\SpikeBall.png")
                );

            HelpTolietPaper = new TolietPaper(_graphicsDevice, RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\Toliet Paper.png")
                );

            background = RC_Framework.Util.texFromFile(
                _graphicsDevice,
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\SkyBg.jpeg"
                );

            BackgroundRec = new Rectangle(100, 100, HelpMoveRec.Width, HelpMoveRec.Height);

            //this will reduce the lag caused from drawing the pervious screen by saveing it as a imagine dureing start up of this class
            renderTarget = new RenderTarget2D(
                _graphicsDevice,
                _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                _graphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            DrawSceneToTexture(renderTarget, StateManager.Instance._spriteBatch);
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            Button temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - 50 - margin), margin + 50), "menuFont_20");
            temp.AttachSprite("arrowSilver_right.png", 0); //right
            HelpButtons.Add("RightArrow", temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 2 - margin), margin), "menuFont_20");
            // the pi method was from https://stackoverflow.com/questions/10527730/microsoft-xna-texture2d-and-rotation so the arrow can be rotated
            temp.AttachSprite("arrowSilver_right.png", (float)Math.PI / -2.0f); // up 
            HelpButtons.Add("UpArrow", temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 3 - margin), margin + temp.Height), "menuFont_20");
            temp.AttachSprite("arrowSilver_right.png", ((float)Math.PI / -2.0f) * 2); // left
            HelpButtons.Add("LeftArrow",temp);

            temp = new Button(content, "buttonSquare_grey.png", "", 50, 50, new Vector2((HelpMenu.Right - temp.Width * 2 - margin), margin + temp.Height * 2), "menuFont_20");
            temp.AttachSprite("arrowSilver_right.png", ((float)Math.PI / 2.0f)); // down
            HelpButtons.Add("DownArrow",temp);

            temp = new Button(content, "buttonLong_grey.png", "SPACE BAR", 150, 50, new Vector2((HelpMenu.Right - temp.Width * 3 - margin), margin + temp.Height * 3), "menuFont_12");
            HelpButtons.Add("SpaceBar", temp);

            foreach (KeyValuePair<string, Button> butt in HelpButtons)
            {
                butt.Value.disabled = true;  
            }

            CloseButton = new Button(content, "buttonSquare_grey.png", "", 40, 40, new Vector2((HelpMenu.Right) - 40, 0), "menuFont_20");
            CloseButton.AttachSprite("iconCross_blue.png", 0);

            font = content.Load<SpriteFont>("Fonts/menuFont_20");
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
            HelpTolietPaper.Update(gameTime);
            HelpSpikeBall.Update(gameTime);
            if (DemoBullet != null) DemoBullet.Update(gameTime);

            if (CloseButton.isPressed)
            {
                StateManager.Instance.RemoveScreen();
            }

            //since i have to use the parent draw method, i muse intilise positions in update.
            if (Phase1)
            {
                HelpPlayer.PosX = HelpMoveRec.X + (HelpMoveRec.Width / 2) - (HelpPlayer.sourceRectangle.Width / 2);
                HelpPlayer.PosY = HelpMoveRec.Y + (HelpMoveRec.Height / 2) - (HelpPlayer.sourceRectangle.Height / 2);

                HelpCovid.PosX = HelpAvoidRec.Left + HelpAvoidRec.Width / 4 - HelpCovid.sourceRectangle.Width / 2;
                HelpCovid.PosY = HelpAvoidRec.Top + (HelpAvoidRec.Height / 2) - (HelpCovid.sourceRectangle.Height / 2);

                HelpSpikeBall.PosX = HelpAvoidRec.Right - HelpAvoidRec.Width / 4 - HelpSpikeBall.sourceRectangle.Width / 2;
                HelpSpikeBall.PosY = HelpAvoidRec.Top + (HelpAvoidRec.Height / 2) - (HelpSpikeBall.sourceRectangle.Height / 2);

                HelpAmmo.PosX = HelpSuppliesRec.Left + HelpSuppliesRec.Width / 4 - HelpAmmo.sourceRectangle.Width / 2;
                HelpAmmo.PosY = HelpSuppliesRec.Top + (HelpSuppliesRec.Height / 2) - (HelpAmmo.sourceRectangle.Height / 2);

                HelpTolietPaper.PosX = HelpSuppliesRec.Right - HelpSuppliesRec.Width / 4 - HelpTolietPaper.sourceRectangle.Width / 2;
                HelpTolietPaper.PosY = HelpSuppliesRec.Top + (HelpSuppliesRec.Height / 2) - (HelpTolietPaper.sourceRectangle.Height / 2);

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
            else if (waitTimer >= waitSec * 6 && waitTimer <= (waitSec * 6) + 1)
            {
                HelpButtons["DownArrow"].SetToUnpressed();
                HelpButtons["SpaceBar"].SetToPressed();
                DemoBullet = HelpPlayer.Fire();
                DemoBullet.PosX = HelpPlayer.PosX + HelpPlayer.sourceRectangle.Width;
                DemoBullet.PosY = HelpPlayer.PosY + HelpPlayer.sourceRectangle.Height - 12;
            }
            else if (waitTimer >= waitSec * 6 && waitTimer <= waitSec * 7)
            {
                if (DemoBullet != null)
                {
                    DemoBullet.PosX += 2;
                    if (DemoBullet.PosX + DemoBullet.sourceRectangle.Width >= HelpMoveRec.Right)
                    {
                        DemoBullet.PosX = -100;
                        DemoBullet = null;
                    }
                }
            }
            else if (waitTimer >= waitSec *7 && waitTimer <= waitSec * 8)
            {
                HelpButtons["SpaceBar"].SetToUnpressed();
                HelpButtons["RightArrow"].SetToPressed();
                HelpPlayer.PosX++;
            }
            else if (waitTimer >= waitSec * 8+1)
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
            
            spriteBatch.Draw(renderTarget, _graphicsDevice.ScissorRectangle, Color.White);
            //keep previous screen's layer
            //StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch,HelpMenu, Color.Black);
            RC_Framework.LineBatch.drawLetterbox(spriteBatch, HelpMenu, 3, Color.White);

            spriteBatch.Draw(background, HelpMoveRec, BackgroundRec, Color.White);
            spriteBatch.Draw(background, HelpAvoidRec, BackgroundRec, Color.White);
            spriteBatch.DrawString(font, "AVOID!!!", new Vector2(HelpAvoidRec.Right + margin, HelpAvoidRec.Center.Y - 20), Color.White);

            spriteBatch.Draw(background, HelpSuppliesRec, BackgroundRec, Color.White);
            spriteBatch.DrawString(font, "Collect", new Vector2(HelpSuppliesRec.Right + margin, HelpSuppliesRec.Center.Y - 20), Color.White);

            foreach (KeyValuePair<string, Button> butt in HelpButtons)
            {
                butt.Value.Draw(spriteBatch);
            }

            CloseButton.Draw(spriteBatch);
            HelpPlayer.PlayerDraw(spriteBatch);
            HelpCovid.EnemyDraw(spriteBatch);
            HelpAmmo.AmmoDraw(spriteBatch);
            HelpTolietPaper.TolietPaperDraw(spriteBatch);
            HelpSpikeBall.SpikeBallDraw(spriteBatch);
            if (DemoBullet != null)
            {
                DemoBullet.Draw(spriteBatch, SpriteEffects.None);
            }
        }

        //http://rbwhitaker.wikidot.com/render-to-texture
        protected void DrawSceneToTexture(RenderTarget2D renderTarget, SpriteBatch spriteBatch)
        {
            //outputs all the draw methods to one image to save time on repating the draw command of the previous state
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.LinearClamp, DepthStencilState.Default,
                RasterizerState.CullNone);
            // Set the render target
            _graphicsDevice.SetRenderTarget(renderTarget);

            _graphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            // Draw the scene
            _graphicsDevice.Clear(Color.CornflowerBlue);
            StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);

            spriteBatch.End();
            // Drop the render target
            _graphicsDevice.SetRenderTarget(null);
        }
    }
}

