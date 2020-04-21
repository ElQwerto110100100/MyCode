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
        public List<Room> copies = new List<Room>();


        public Texture2D Sprite;
        public int OrginalPosX;
        public int OrginalPosY;
        public int Width;
        public int Height;
        public bool PlaceDown;

        public Room(int posX, int posY, int width, int height)
        {
            OrginalPosX = posX;
            OrginalPosY = posY;
            Width = width;
            Height = height;
            Rec = new Rectangle(OrginalPosX, OrginalPosY, Width, Height);
            copies.Add(this);
        }

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

        public void BaseUpdate()
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
                        ResetSize();
                        PlaceDown = false;
                    }
                }

                else if (!Grid.Instance.IsInSlot(this))
                {
                    Rec.X = OrginalPosX;
                    Rec.Y = OrginalPosY;
                    OrignalRect();
                } 
                SetRect(Grid.Instance.PlaceInSlot(this));
        }

        public void ResetSize()
        {
            Rec.X = MouseClass.Instance.GetState().Position.X - (Rec.Width / 2);
            Rec.Y = MouseClass.Instance.GetState().Position.Y - (Rec.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Room room in copies)
            {
                spriteBatch.Draw(room.Sprite, room.Rec, Color.White);
            }
        }
    }
}
