using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS.GamesStates.States.Overlays
{
    class RoomSelector : GameState
    {
        List<Room> LevelsRooms = new List<Room>();

        Rectangle ToolboxRec;
        int ToolboxWidth { get; set; }
        int ToolboxHeight { get; set; }
        int ToolboxX { get; set; }
        int ToolboxY { get; set; }

        public RoomSelector(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        // Initialize the game settings here      
        public override void Initialize()
        {
            ToolboxWidth = 200;
            ToolboxHeight = _graphicsDevice.Viewport.Height;
            ToolboxX = _graphicsDevice.Viewport.Width - ToolboxWidth;
            ToolboxY = 0;
            ToolboxRec = new Rectangle(ToolboxX, ToolboxY, ToolboxWidth, ToolboxHeight);
        }

        // Load all content here
        public override void LoadContent(ContentManager content)
        {
            LevelsRooms.Add(new Room(content.Load<Texture2D>("Rooms/Test_Room"), ToolboxX + 10, 10, 50, 50));
        }

        // Unload any content here
        public override void UnloadContent()
        {
        }

        // Updates the game
        public override void Update(GameTime gameTime)
        {
            foreach (Room room in LevelsRooms)
            {
                room.MouseMove();
                room.SetRect(Grid.Instance.PlaceInSlot(room.GetRect()));
            }

            StateManager.Instance._screens.Skip(1).First().Update(gameTime);
        }

        // Draws the game
        public override void Draw(SpriteBatch spriteBatch)
        {
            StateManager.Instance._screens.Skip(1).First().Draw(spriteBatch);
            RC_Framework.LineBatch.drawFillRectangle(spriteBatch, ToolboxRec, Color.Coral);
            foreach (Room room in LevelsRooms)
            {
                room.Draw(spriteBatch);
                RC_Framework.LineBatch.drawLineRectangle(spriteBatch, room.GetRect(), Color.Black);
            }
        }
    }
}
