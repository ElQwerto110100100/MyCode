using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src
{

    static class Util
    {
        // when we need to use other various shaders. 
        //class will need to be made for each unqie shader
        static public Shader DefaultShader { get; set; }
        static public Loader Loader { get; set; }
        static Util()
        {
            Loader = new Loader();
            DefaultShader = new Shader("shader.vert", "shader.frag");
        }

        static public void FinishUp() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            Loader.CleanUp();
            DefaultShader.CleanUp();
        }
    }
}
