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
        public Loader loader;
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            loader = new Loader();
            triangle = loader.LoadToVao(vertices, indices, texCoords);
            TextureLibary.CreateTexture(@"res\img1.png", "image1");
            TextureLibary.CreateTexture(@"res\img2.png", "image2");
            Util.DefaultShader.SetInt("textureSampler", 0);
            Util.DefaultShader.SetInt("textureSampler2", 1);

            base.OnLoad(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            //Get the state of the keyboard this frame
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
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
