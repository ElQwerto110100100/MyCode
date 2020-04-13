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
        public BasicRoom(Texture2D sprite, int posX, int posY, int width, int height) : base(sprite, posX, posY, width, height)
        {

        }
    }
}
