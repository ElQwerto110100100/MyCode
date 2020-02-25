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
    class SpriteSheetExtract
    {
        public List<SpriteXml> spriteList = new List<SpriteXml>();
        public List<SpriteImage> spriteImageList = new List<SpriteImage>();
        public SpriteSheetExtract(GraphicsDeviceManager gd, string spriteSheetPath, string xmlPath)
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

        public void getSprite(string spriteName)
        {

        }

        public static Texture2D texFromFile(GraphicsDevice gd, string fName)
        {
            // note needs :using System.IO;
            Stream fs = new FileStream(fName, FileMode.Open);
            Texture2D rc = Texture2D.FromStream(gd, fs);
            fs.Close();
            return rc;
        }
    }
}
