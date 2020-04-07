using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarkTut1.Resources;

namespace TRotS.Entity
{
    class Player : Sprite
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly Texture2D charSheet;
        int moveSpeed = 3;
        int bulletSpeed = 6;

        public List<Sprite> bullets = new List<Sprite>();

        public Player(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            this.graphicsDevice = graphicsDevice;
            this.charSheet = charSheet;

            AddAnimation("movement",100,60,5,0);
            SetAnimation("movement");

            SetPosXY(100, 100);
        }

        public void UpdatePlayer(GameTime gametime)
        {
            Update(gametime);
            KeyboardState currentKeyState = MouseClass.Instance.GetKeyState();
            KeyboardState previousKeyState = MouseClass.Instance.GetPrevKeyState();

            if (currentKeyState.IsKeyDown(Keys.Down))
            {
                SetPosXY(0, moveSpeed);
            }
            if (currentKeyState.IsKeyDown(Keys.Up))
            {
                SetPosXY(0, -moveSpeed);
            }
            if (currentKeyState.IsKeyDown(Keys.Left))
            {
                SetPosXY(-moveSpeed, 0);
            }
            if (currentKeyState.IsKeyDown(Keys.Right))
            {
                SetPosXY(moveSpeed, 0);
            }

            if (currentKeyState.IsKeyDown(Keys.Space) && !previousKeyState.IsKeyDown(Keys.Space))
            {
                Sprite newBullet = new Sprite(graphicsDevice, RC_Framework.Util.texFromFile(graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane-bullet.png"));
                newBullet.AddAnimation("fire",28,12,1,0,0.01,false);
                newBullet.SetAnimation("fire");
                newBullet.SetPosXY(this.PosX + this.sourceRectangle.Width, this.PosY + this.sourceRectangle.Height);
                newBullet.borderOn = this.borderOn;
                bullets.Add(newBullet);
            }

            foreach (Sprite bullet in bullets) {
                bullet.Update(gametime);
                bullet.PosX += bulletSpeed;
            }
            bullets.RemoveAll(bullet => bullet.PosX > graphicsDevice.Viewport.Width);
        }

        public void PlayerDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch);

            foreach (Sprite bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}
