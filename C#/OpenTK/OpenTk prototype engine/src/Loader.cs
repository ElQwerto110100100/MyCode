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

        int VertexArrayObject;
        public RawModle LoadToVao(float[] positions, uint[] indices)
        {
            VertexArrayObject = CreateVAO();//first make a VAO
            CreateVBO(positions);//bind VBO to VAO
            BindIndicesBuffer(indices);//bind EBO to VAO

            //finish off
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            return new RawModle(VertexArrayObject, indices.Length);
        }

        private int CreateVBO(float[] data)
        {
            int vboId = GL.GenBuffer();
            vbos.Add(vboId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
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
            
        }
    }
}
