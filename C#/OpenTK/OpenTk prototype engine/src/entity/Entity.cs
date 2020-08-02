using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src.entity
{
    class Entity
    {
        public Vector3 Position { get; set; }
        public RawModel Model { get; set; }
        public float Scale { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }
        public Matrix4 EnityMatrix { get; set; }
        public Shader Shader { get; set; }
        public Entity(RawModel model, Shader shader, Vector3 position = new Vector3(),  float rotX = 1, float rotY = 1, float rotZ = 1, float scale = 1)
        {
            Position = position;
            Model = model;
            Scale = scale;
            RotX = rotX;
            RotY = rotY;
            RotZ = rotZ;
            EnityMatrix = Maths.CreateTransmatrix(new Vector3(Position.X, Position.Y, Position.Z),rotX, rotY, rotZ, scale);
            Shader = shader;
        }

        public void Move(int NewPosX = 0, int NewPosY = 0, int NewPosZ = 0, Vector3? NewPositionVec = null, float NewRotX = 1, float NewRotY = 1, float newRotZ = 1, float NewScale = 1) { 
            if (NewPositionVec != null)
            {
                Maths.CreateTransmatrix(Position, RotX + NewRotX, RotY + NewRotX, RotZ + NewRotX, Scale + NewScale);
            }
            else if (NewPosX != 0 | NewPosY != 0 | NewPosZ != 0)
            {
                Maths.CreateTransmatrix(new Vector3(Position.X + NewPosX, Position.Y + NewPosY, Position.Z + NewPosZ));
            }
        }
    }
}
