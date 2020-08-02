using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src
{
    class RawModel
    {
        public int VaoID { get; set; }
        public int IndiciesLength { get; set; }
        public RawModel(int vaoID, int indiciesLength)
        {
            VaoID = vaoID;
            IndiciesLength = indiciesLength;
        }
    }
}
