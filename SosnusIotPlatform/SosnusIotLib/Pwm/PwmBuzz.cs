using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosnusIotLib.Pwm
{
    class PwmBuzz : PwmBasic
    {
        int note = 480; // in milliseconds

        public struct parameters
        {
            public int frequency;
            public int fill;
            public int duration;

            public parameters(int _frequency, int _fill, int _duration)
            {
                this.frequency = _frequency;
                this.fill = _fill;
                this.duration = _duration;
            }
        };

        List<parameters> list = new List<parameters>();

        void func()
        {
            list.Add(new parameters(3,6,7));
        }
    }
}
