using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarkTut1.Resources;
using Microsoft.Xna.Framework.Audio;

namespace TRotS.Entity
{
    class Player : Sprite
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly Texture2D charSheet;
        int moveSpeed = 5;
        int bulletSpeed = 6;
        public int score = 0;
        public int ammunation = 20;

        private Color planeColor = Color.White;

        public List<Sprite> bullets = new List<Sprite>();
        public bool Hit = false;
        private int waitTimer = 0;
        public int health = 3;

        static List<SoundEffect> soundEffects;
        static int occurances = 0;

        Dictionary<string, List<Rectangle>> healthBar = new Dictionary<string, List<Rectangle>>();

        public Player(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            this.graphicsDevice = graphicsDevice;
            this.charSheet = charSheet;

            soundEffects = new List<SoundEffect>();
            soundEffects.Add(MouseClass.Instance._content.Load<SoundEffect>("Sounds/Effects/planeGun"));
            SoundEffect.MasterVolume = 1f;

            AddAnimation("movement",100,60,5,0);
            SetAnimation("movement");

            SetPosXY(100, graphicsDevice.Viewport.Height / 2);

            List<Rectangle> temp = new List<Rectangle>();
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barGreen_verticalTop.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barGreen_verticalMid.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barGreen_verticalMid.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barGreen_verticalMid.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barGreen_verticalBottom.png"));
            healthBar.Add("GreenBar", temp);

            temp = new List<Rectangle>();
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barYellow_verticalTop.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barYellow_verticalMid.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barYellow_verticalMid.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barYellow_verticalBottom.png"));
            healthBar.Add("YellowBar", temp);

            temp = new List<Rectangle>();
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barRed_verticalTop.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barRed_verticalMid.png"));
            temp.Add(SpriteSheet.Instance.GetSpriteRec("barRed_verticalBottom.png"));
            healthBar.Add("RedBar", temp);

        }

        public void UpdatePlayer(GameTime gametime)
        {
            Update(gametime);
            KeyboardState currentKeyState = MouseClass.Instance.GetKeyState();
            KeyboardState previousKeyState = MouseClass.Instance.GetPrevKeyState();

            if (currentKeyState.IsKeyDown(Keys.Down))
            {
                SetPosXY(0, moveSpeed, 0, 10);
            }
            if (currentKeyState.IsKeyDown(Keys.Up))
            {
                SetPosXY(0, -moveSpeed);
            }
            if (currentKeyState.IsKeyDown(Keys.Left))
            {
                //added extra padding to compensat for health bar
                SetPosXY(-moveSpeed, 0, -18);
            }
            if (currentKeyState.IsKeyDown(Keys.Right))
            {
                SetPosXY(moveSpeed, 0);
            }

            if (currentKeyState.IsKeyDown(Keys.Space) && !previousKeyState.IsKeyDown(Keys.Space))
            {
                Fire();
            }

            if (Hit)
            {
                if (waitTimer % 5 == 0)
                {
                    planeColor = Color.Red;
                } 
                else
                {
                    planeColor = Color.White;
                }
                waitTimer--;
                if (waitTimer == 0) Hit = false;
            }

            BulletUpdate(gametime);
        }

        public Sprite Fire()
        {
           if (ammunation != 0)
            {
                Sprite newBullet = new Sprite(graphicsDevice, RC_Framework.Util.texFromFile(graphicsDevice, @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\plane-bullet.png"));
                if (occurances != 3)
                {
                    occurances += 1;
                     soundEffects[0].Play();
                    occurances -= 1;
                }
                newBullet.AddAnimation("fire", 28, 12, 1, 0, 0.01, false);
                newBullet.SetAnimation("fire");
                SoundLib.Instance.PlaySound("PlaneGun", 0.2f);
                newBullet.SetPosXY(this.PosX + this.sourceRectangle.Width, this.PosY + this.sourceRectangle.Height - 12);
                bullets.Add(newBullet);
                ammunation -= 1;

                return newBullet;
            }
            return null;
        }

        public void BulletUpdate(GameTime gametime) {
            foreach (Sprite bullet in bullets)
            {
                bullet.Update(gametime);
                bullet.PosX += bulletSpeed;
            }
            bullets.RemoveAll(bullet => bullet.PosX > graphicsDevice.Viewport.Width);
        }

        public void PlaneHit()
        {
            if (Hit)
            {
                //no damage period
            }
            else
            {
                Hit = true;
                waitTimer = 60 * 1;
                if (health != 0) health--; //prevents it from going negtive
            }

        }

        public void PlayerDraw(SpriteBatch spriteBatch, SpriteEffects spriteEx = SpriteEffects.None)
        {
            Draw(spriteBatch, spriteEx, planeColor);

            if (health == 3)
            {
                DrawHealthBar(spriteBatch, healthBar["GreenBar"], spriteEx);

            }
            else if (health == 2)
            {
                DrawHealthBar(spriteBatch, healthBar["YellowBar"], spriteEx);
            }
            else if (health == 1)
            {
                DrawHealthBar(spriteBatch, healthBar["RedBar"], spriteEx);
            }
            else
            {
                //draw nothing
            }

            foreach (Sprite bullet in bullets)
            {
                bullet.Draw(spriteBatch, SpriteEffects.None);
            }
        }

        private void DrawHealthBar(SpriteBatch spriteBatch, List<Rectangle> rects, SpriteEffects spriteEx = SpriteEffects.None)
        {
            int totalLength = 0;
            for (int i = 0; i < rects.Count; i++)
            {
                spriteBatch.Draw(
                    SpriteSheet.Instance.GetSpriteSheet(), 
                    new Vector2(PosX - rects[i].Width, PosY + totalLength), 
                    null,
                    rects[i], 
                    null,
                    0,
                    null,
                    Color.White,
                    spriteEx,
                    0
                    );
                totalLength += rects[i].Height;
            }
        }
    }
}
