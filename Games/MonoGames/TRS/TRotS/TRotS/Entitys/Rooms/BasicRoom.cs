using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using TRotS.GamesStates;

namespace TRotS.Entitys.Rooms
{
    class BasicRoom : Room
    {
        public BasicRoom(int posX, int posY, int width, int height) : base(posX, posY, width, height)
        {
            Sprite = MouseClass.Instance._Content.Load<Texture2D>("Rooms/Test_Room");
        }
    }
}
