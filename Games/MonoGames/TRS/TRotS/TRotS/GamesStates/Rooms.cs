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
    class Rooms
    {
        public Rectangle Rec;
        public bool draging = false;

        public Rooms(Texture2D sprite, int posX, int posY, int width, int height)
        {
            Sprite = sprite;
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
        }

        public Texture2D Sprite { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle GetRect()
        {
            return Rec = new Rectangle(PosX, PosY, Width, Height);
        }

        public Texture2D GetTex()
        {
            return Sprite;
        }

        public void MoveRoom()
        {
            if ((MouseClass.Instance.GetRec().Intersects(Rec) && 
                MouseClass.Instance.GetState().LeftButton == ButtonState.Pressed) | 
                draging)
            {
                int diffrentX = MouseClass.Instance.GetState().Position.X - MouseClass.Instance.GetPrevState().Position.X;
                int diffrentY = MouseClass.Instance.GetState().Position.Y - MouseClass.Instance.GetPrevState().Position.Y;

                PosX += diffrentX;
                PosY += diffrentY;
                if (MouseClass.Instance.GetState().LeftButton == ButtonState.Pressed)
                {
                    draging = true;
                }
                else
                {
                    draging = false;
                }
            }

        }
    }
}
