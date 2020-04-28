using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS
{
    //https://stackoverflow.com/questions/20676185/xna-monogame-getting-the-frames-per-second
    class FrameRate
    {
            double currentFrametimes;
            double weight;
            int numerator;

            public double framerate
            {
                get
                {
                    return (numerator / currentFrametimes);
                }
            }

            public FrameRate(int oldFrameWeight)
            {
                numerator = oldFrameWeight;
                weight = (double)oldFrameWeight / ((double)oldFrameWeight - 1d);
            }

            public void Update(double timeSinceLastFrame)
            {
                currentFrametimes = currentFrametimes / weight;
                currentFrametimes += timeSinceLastFrame;
            }
        
    }
}
