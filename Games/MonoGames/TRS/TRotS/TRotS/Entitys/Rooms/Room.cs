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
            OrginalPosX = posX;
            OrginalPosY = posY;
            Width = width;
            Height = height;
            Rec = new Rectangle(OrginalPosX, OrginalPosY, Width, Height);
        }

        public Texture2D Sprite { get; set; }
        public int OrginalPosX { get; set; }
        public int OrginalPosY { get; set; }
        public int TempPosX { get; set; }
        public int TempPosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool PlaceDown { get; set; }

        public void OrignalRect()
        {
            Rec = new Rectangle(OrginalPosX, OrginalPosY, Width, Height);
        }
        //if it is placed in a slot
        public void SetRect(Rectangle newRec)
        {
            Rec = newRec;
            PlaceDown = true;
        }

        public void MouseMove()
        {
            if (MouseClass.Instance.GetState().LeftButton != ButtonState.Pressed)
            {
                draging = false;
            }
            if ((MouseClass.Instance.GetRect().Intersects(Rec) | draging) && 
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
            }

            else if (!Grid.Instance.IsInSlot(this))
            {
                Rec.X = OrginalPosX;
                Rec.Y = OrginalPosY;
            }
            SetRect(Grid.Instance.PlaceInSlot(this));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Rec, Color.White);
        }
    }
}
