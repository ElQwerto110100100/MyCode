using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk_prototype_engine.src
{
    class RawModle
    {
        public int VaoID { get; set; }
        public int IndiciesLength { get; set; }

        public RawModle(int vaoID, int indiciesLength)
        {
            VaoID = vaoID;
            IndiciesLength = indiciesLength;
        }

    }
}
