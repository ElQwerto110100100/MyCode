using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS.GamesStates
{
    class Room
    {
        public Rectangle Rec;
        public bool draging = false;

        public Room(Texture2D sprite, int posX, int posY, int width, int height)
        {
            Sprite = sprite;
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            Rec = new Rectangle(PosX, PosY, Width, Height);
        }

        public Texture2D Sprite { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool PlaceDown { get; set; } 

        public void OrignalRect()
        {
            Rec = new Rectangle(PosX, PosY, Width, Height);
        }
        //if it is placed in a slot
        public void SetRect(Rectangle newRec)
        {
            Rec = newRec;
            PlaceDown = true;
        }

        public Rectangle GetRect()
        {
            return Rec;
        }

        public Texture2D GetTex()
        {
            return Sprite;
        }

        public void MouseMove()
        {
            if (MouseClass.Instance.GetState().LeftButton != ButtonState.Pressed)
            {
                draging = false;
            }
            if ((MouseClass.Instance.GetRect().Intersects(Rec) | 
                draging) && 
                MouseClass.Instance.GetState().LeftButton == ButtonState.Pressed)
            {
                draging = true;
                //if it was allready place down return to orignal size and centre on curosr
                if (PlaceDown == true)
                {
                    OrignalRect();
                    Rec.X = MouseClass.Instance.GetState().Position.X - (Rec.Width / 2);
                    Rec.Y = MouseClass.Instance.GetState().Position.Y - (Rec.Height / 2);

                    PlaceDown = false;
                }
                int diffrentX = MouseClass.Instance.GetState().Position.X - MouseClass.Instance.GetPrevState().Position.X;
                int diffrentY = MouseClass.Instance.GetState().Position.Y - MouseClass.Instance.GetPrevState().Position.Y;

                PosX += diffrentX;
                PosY += diffrentY;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Rec, Color.White);
        }
    }
}
