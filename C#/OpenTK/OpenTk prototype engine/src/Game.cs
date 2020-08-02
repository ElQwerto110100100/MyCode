using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src
{
    class Game : GameWindow
    {
        float[] vertices = {
            -0.5f,  0.5f, 0.0f,  // top right
            -0.5f, -0.5f, 0.0f,  // bottom right
            0.5f, -0.5f, 0.0f,  // bottom left
            0.5f,  0.5f, 0.0f   // top left
        };
        uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            3, 1, 2    // second triangle
        };
        float[] texCoords = {
            0,0,
            0,1,
            1,1,
            1,0
        };
        public RawModel triangle;

        public Matrix4 trans;
        public float xPos = 0;
        public float yPos = 0;
        private bool imageChanged = false;
        KeyboardState previousKeyboard;
        KeyboardState input;
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            triangle = Loader.LoadToVao(vertices, indices, texCoords);
            //create textures
            TextureLibary.CreateTexture(@"res\img1.png", "image1");
            TextureLibary.CreateTexture(@"res\img2.png", "image2");



            //bind them to a texture unit
            TextureLibary.TexLibary["image1"].Use(0);
            TextureLibary.TexLibary["image2"].Use(1);

            //bund the samplers to the texture units
            Util.DefaultShader.SetData("textureSampler", 0);
            Util.DefaultShader.SetData("textureSampler2", 1);
            trans = Maths.CreateTransmatrix(new Vector3(xPos, yPos, 0));
            Util.DefaultShader.SetData("transform", trans);
            //transform model
            base.OnLoad(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            //Get the state of the keyboard this frame
            previousKeyboard = input;
            input = Keyboard.GetState();
            
            
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (input.IsKeyDown(Key.Left))
            {
                xPos -= 0.05f;
                trans = Maths.CreateTransmatrix(new Vector3(xPos, yPos, 0));
                Util.DefaultShader.SetData("transform", trans);
            }
            if (input.IsKeyDown(Key.Right))
            {
                xPos += 0.05f;
                trans = Maths.CreateTransmatrix(new Vector3(xPos, yPos, 0));
                Util.DefaultShader.SetData("transform", trans);
            }
            if (input.IsKeyDown(Key.Up))
            {
                yPos += 0.05f;
                trans = Maths.CreateTransmatrix(new Vector3(xPos, yPos, 0));
                Util.DefaultShader.SetData("transform", trans);
            }
            if (input.IsKeyDown(Key.Down))
            {
                yPos -= 0.05f;
                trans = Maths.CreateTransmatrix(new Vector3(xPos, yPos, 0));
                Util.DefaultShader.SetData("transform", trans);
            }
            if (input.IsKeyDown(Key.Space) && previousKeyboard.IsKeyUp(Key.Space))
            {
                if (!imageChanged)
                {
                    Util.DefaultShader.SetData("textureSampler", 0);
                    Util.DefaultShader.SetData("textureSampler2", 0);
                    imageChanged = !imageChanged;
                } else
                {
                    Util.DefaultShader.SetData("textureSampler", 1);
                    Util.DefaultShader.SetData("textureSampler2", 1);
                    imageChanged = !imageChanged;
                }
            }
            if (input.IsKeyDown(Key.Q) && previousKeyboard.IsKeyUp(Key.Q))
            {
                Util.DefaultShader.SetData("textureSampler", 0);
                Util.DefaultShader.SetData("textureSampler2", 1);
            }
            base.OnUpdateFrame(e);
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e) 
        {
            Util.FinishUp();
            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Drawing.Draw(Util.DefaultShader, triangle);

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
