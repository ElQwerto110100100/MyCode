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

        static public string basePath = @"C:\Users\joshy\Desktop\Github\MyCode\C#\OpenTK\OpenTk prototype engine\src\";
        static Util()
        {
            DefaultShader = new Shader(@"shaders\shader.vert", @"shaders\shader.frag");
        }

        static public void FinishUp()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            Loader.CleanUp();
            DefaultShader.CleanUp();
        }
    }
    static public class Maths
    {
        public static Matrix4 CreateTransmatrix(Vector3 translation, float rx = 1, float ry = 1, float rz = 1, float scale = 1)
        {
            Matrix4 matRotationX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rx));
            Matrix4 matRotationY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(ry));
            Matrix4 matRotationZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rz));
            Matrix4 matScale = Matrix4.CreateScale(scale);
            Matrix4 matTranslation = Matrix4.CreateTranslation(translation);
            Matrix4 trans = new Matrix4();
            trans += matTranslation;
            return trans;
        }

    }
}
