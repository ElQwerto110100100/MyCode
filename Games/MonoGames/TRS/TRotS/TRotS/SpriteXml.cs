using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS
{
    class SpriteXml
    {
        public string Name { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        //helpful function for debugging
        public void printAll()
        {
            try
            {
                Console.WriteLine(Name + ' ' + PosX + ' ' + PosY + ' ' + Width + ' ' + Height);
            } catch
            {
                //the first item will be invalid as it trys to read the first line from the xml witch has no useful infomation
            }
        }

        public Rectangle GetRect()
        {
            return new Rectangle(PosX, PosY, Width, Height);
        }
        public Vector2 GetVec()
        {
            return new Vector2(PosX, PosY);
        }
    }
}
