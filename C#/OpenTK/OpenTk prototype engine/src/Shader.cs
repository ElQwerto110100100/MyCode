using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src
{
    public class Shader
    {
        public int Handle;
        int VertexShader;
        int FragmentShader;

        string basePath = @"C:\Users\joshy\Desktop\Github\MyCode\C#\OpenTK\OpenTk prototype engine\src\shaders\";
        public Shader(string vertexPath, string fragmentPath)
        {
            loadShaders(vertexPath, fragmentPath);
            CompileShaders();

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);
            GL.LinkProgram(Handle);
            GL.ValidateProgram(Handle);
        }
        public void CompileShaders()
        {
            GL.CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);
        }
        private void loadShaders(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource;

            using (StreamReader reader = new StreamReader(basePath + vertexPath, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }

            string FragmentShaderSource;

            using (StreamReader reader = new StreamReader(basePath + fragmentPath, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);
        }
        public void CleanUp() 
        {
            Stop();
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
            GL.DeleteProgram(Handle);
        }
        public void Use()
        {
            GL.UseProgram(Handle);
        }
        public void Stop()
        {
            GL.UseProgram(0);
        }
        //public int GetAttribLocation(string attribName)
        //{
        //    return GL.GetAttribLocation(Handle, attribName);
        //}

        //protected void BindAttributes(int attribute, string variableName)
        //{
        //    GL.BindAttribLocation(Handle, attribute, variableName);
        //}

        //private bool disposedValue = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        GL.DeleteProgram(Handle);

        //        disposedValue = true;
        //    }
        //}
    }
}
