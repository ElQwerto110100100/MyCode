using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src
{
    static class Loader
    {
        static private List<int> vaos = new List<int>();
        static private List<int> vbos = new List<int>();
        static private List<int> texs = new List<int>();

        static int VertexArrayObject;
        static public RawModel LoadToVao(float[] positions, uint[] indices, float[] texCoords = null)
        {
            VertexArrayObject = CreateVAO();//first make a VAO
            CreateVBO(0, positions, 3);//bind VBO to VAO
            if (texCoords != null) CreateVBO(1, texCoords, 2);//bind tex VBO to VAO
            BindIndicesBuffer(indices);//bind EBO to VAO
            //finish off
            return new RawModel(VertexArrayObject, indices.Length);
        }

        public static void LoadTexture(int TexHandle)
        {
            texs.Add(TexHandle);
        }

        private static int CreateVBO(int attrNumber, float[] data, int dataSize)
        {
            int vboId = GL.GenBuffer();
            vbos.Add(vboId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attrNumber, dataSize, VertexAttribPointerType.Float, false, 0, 0);
            return vboId;
        }

        private static int CreateVAO()
        {
            int vaoId = GL.GenVertexArray();
            vaos.Add(vaoId);
            GL.BindVertexArray(vaoId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vaoId);
            return vaoId;
        }

        private static void BindIndicesBuffer(uint[] indices)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public static void CleanUp()
        {
            vbos.ForEach(vboId => GL.DeleteBuffer(vboId));
            vaos.ForEach(vaoId => GL.DeleteVertexArray(vaoId));
            texs.ForEach(texId => GL.DeleteTexture(texId));
        }
    }
}
