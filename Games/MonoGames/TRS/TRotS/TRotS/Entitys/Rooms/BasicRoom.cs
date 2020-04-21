using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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

        public Room Update()
        {
            BaseUpdate();
            if (Grid.Instance.IsInSlot(this))
            {
                BasicRoom newRoom = new BasicRoom(base.OrginalPosX, base.OrginalPosY, base.Width, base.Height)
                {
                    Sprite = this.Sprite
                };
                return newRoom;
            }
            else
            {
                return null;
            }
        }
    }
}
