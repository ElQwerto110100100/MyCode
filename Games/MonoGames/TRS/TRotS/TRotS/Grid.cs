using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TRotS.GamesStates
{
    class Grid
    {
        private List<Rectangle> slots;
        private static Grid _instance;

        public static Grid Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Grid();
                }
                return _instance;
            }
        }

        public void MakeGrid(int numOfSlotsX, int numOfSlotsY, int screenWidth, int screenHeight)
        {
            int xPos = 0;
            int yPos = 0;
            int width = screenWidth / numOfSlotsX;
            int height = screenHeight / numOfSlotsY;
            slots = new List<Rectangle>();

            for (int i = 0; i < numOfSlotsX; i++)
            {
                xPos = (screenWidth / numOfSlotsX) * i;
                yPos = 0;
                slots.Add(new Rectangle(xPos, yPos, width, height));
                for (int j = 0; j < numOfSlotsY; j++)
                {
                    yPos = (screenHeight / numOfSlotsY) * j;
                    slots.Add(new Rectangle(xPos, yPos, width, height));
                }
            }
        }

        public List<Rectangle> GetGrid()
        {
            return slots;
        }

        public void DrawGrid(SpriteBatch spriteBatch)
        {

            foreach (Rectangle slot in slots)
            {
                RC_Framework.LineBatch.drawLineRectangle(spriteBatch, slot, Color.White);
            }
        }

        public Rectangle PlaceInSlot(Rectangle item)
        {
            foreach(Rectangle slot in slots)
            {
                if (item.Intersects(slot) && MouseClass.Instance.GetState().LeftButton != ButtonState.Pressed)
                {
                    //change the items position to match that slot
                    return slot;
                }
            }
            return item;
        }
    }

}
