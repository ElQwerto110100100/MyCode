﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace TRotS
{
    class SpriteSheet
    {
        //allows for this to be called from anywhere
        private static SpriteSheet _instance;

        public List<SpriteXml> spriteList = new List<SpriteXml>();
        public GraphicsDeviceManager graphics;
        public string spriteSheetPath;

        public static SpriteSheet Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpriteSheet();
                }
                return _instance;
            }
        }
        /*
         GraphicsDeviceManager gd, string sSPath, string xmlPath
         spriteSheetPath = sSPath;
            graphics = gd;
            ReadXml(xmlPath);
        */
        public void SetGraphicsManager(GraphicsDeviceManager gd)
        {
            graphics = gd;
        }

        public void AddSpriteSheet(string sSPath, string xmlPath)
        {
            spriteSheetPath = sSPath;
            ReadXml(xmlPath);
        }

        public Texture2D GetSpriteSheet()
        {
            return texFromFile(graphics.GraphicsDevice, spriteSheetPath);
        }

        public float GetSpritWidth(string spriteName)
        {
            SpriteXml sprite = spriteList.Find(x => x.Name == spriteName);
            return (float)sprite.Width;
        }

        public float  GetSpritHeight(string spriteName)
        {
            SpriteXml sprite = spriteList.Find(x => x.Name == spriteName);
            return (float)sprite.Height;
        }

        public Rectangle GetSpriteRec(string spriteName)
        {
            SpriteXml sprite = spriteList.Find(x => x.Name == spriteName);
            return sprite.GetRect();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Vector2 imgScale, string spriteName, Color? color = null, float rot = 0, bool centrePlace = false)
        {
            SpriteXml sprite = spriteList.Find(x => x.Name == spriteName);
            Vector2 centre;
            if (centrePlace) centre = new Vector2(sprite.Width / 2, sprite.Height / 2); else centre = new Vector2(0, 0);
            Color defColor = color ?? Color.White;
            spriteBatch.Draw(GetSpriteSheet(), pos, sprite.GetRect(), defColor, rot, centre, imgScale, SpriteEffects.None, 0.0f);
        }
        public void ReadXml(string xmlPath)
        {
            //this will read the xml
            SpriteXml currentItem;
            //https://support.microsoft.com/en-au/help/307548/how-to-read-xml-from-a-file-by-using-visual-c
            using (XmlTextReader reader = new XmlTextReader(xmlPath))
            {
                while (reader.Read())
                {
                    currentItem = new SpriteXml();
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            while (reader.MoveToNextAttribute()) // Read the attributes.
                                                                 // this will place all correct values into list object to refrenc when extracting sprite from sheet
                                switch (reader.Name)
                                {
                                    case "name":
                                        currentItem.Name = reader.Value;
                                        break;
                                    case "x":
                                        currentItem.PosX = int.Parse(reader.Value);
                                        break;
                                    case "y":
                                        currentItem.PosY = int.Parse(reader.Value);
                                        break;
                                    case "width":
                                        currentItem.Width = int.Parse(reader.Value);
                                        break;
                                    case "height":
                                        currentItem.Height = int.Parse(reader.Value);
                                        break;
                                }
                            spriteList.Add(currentItem);
                            break;
                    }
                }
            }
        }

        public static Texture2D texFromFile(GraphicsDevice gd, string fName)
        {
            return RC_Framework.Util.texFromFile(gd, fName);
        }
    }
}
