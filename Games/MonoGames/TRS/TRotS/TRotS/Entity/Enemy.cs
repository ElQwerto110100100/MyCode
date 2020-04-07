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
    class Enemy : Sprite
    {
        private GraphicsDevice graphicsDevice;
        private Texture2D charSheet;
        public List<Enemy> enemies = new List<Enemy>();
        public int NumOfEnimes { get; set; } = 0;
        private Random rand = new Random();
        private int movementSpeed;

        public Enemy(GraphicsDevice graphicsDevice, Texture2D charSheet) : base(graphicsDevice, charSheet)
        {
            this.graphicsDevice = graphicsDevice;
            this.charSheet = charSheet;

            AddAnimation("movement", 78, 78, 4, 0);
            SetAnimation("movement");
            

            for (int i = NumOfEnimes; i <= NumOfEnimes; i++)
            {
                enemies.Add(new Enemy(graphicsDevice, charSheet));
            }
        }

        public void EnemyUpdate(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void EnemyDraw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
