using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src
{
    static class Drawing 
    {
        static public void Draw(Shader shader, RawModel Modle)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Use();
            
            GL.BindVertexArray(Modle.VaoID);
            GL.EnableVertexAttribArray(0);         
            GL.EnableVertexAttribArray(1);

            GL.DrawElements(PrimitiveType.Triangles,  6, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            
            GL.BindVertexArray(0);

            shader.Stop();

        }
    }
}
