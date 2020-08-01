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
    class Loader
    {
        private List<int> vaos = new List<int>();
        private List<int> vbos = new List<int>();
        private List<int> texs = new List<int>();

        int VertexArrayObject;
        public RawModel LoadToVao(float[] positions, uint[] indices, float[] texCoords = null)
        {
            VertexArrayObject = CreateVAO();//first make a VAO
            CreateVBO(0, positions, 3);//bind VBO to VAO
            if (texCoords != null) CreateVBO(1, texCoords, 2);//bind tex VBO to VAO
            BindIndicesBuffer(indices);//bind EBO to VAO
            //finish off
            return new RawModel(VertexArrayObject, indices.Length);
        }

        public void LoadTexture(int TexHandle)
        {
            texs.Add(TexHandle);
        }

        private int CreateVBO(int attrNumber, float[] data, int dataSize)
        {
            int vboId = GL.GenBuffer();
            vbos.Add(vboId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attrNumber, dataSize, VertexAttribPointerType.Float, false, 0, 0);
            return vboId;
        }

        private int CreateVAO()
        {
            int vaoId = GL.GenVertexArray();
            vaos.Add(vaoId);
            GL.BindVertexArray(vaoId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vaoId);
            return vaoId;
        }

        private void BindIndicesBuffer(uint[] indices)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public void CleanUp()
        {
            vbos.ForEach(vboId => GL.DeleteBuffer(vboId));
            vaos.ForEach(vaoId => GL.DeleteVertexArray(vaoId));
            texs.ForEach(texId => GL.DeleteTexture(texId));
        }
    }
}
