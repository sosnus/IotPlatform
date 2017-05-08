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


            public parameters(int frequency, int fill, int duration)
            {
                this.frequency = frequency;
                this.fill = fill;
                this.duration = duration;
            }
    };

        List<parameters> list = new List<parameters>();

        void func()
        {
            list.Add(new parameters(3,6,7));
        }



        //int[] parameters = new int[3];
        //public int[] parameters = new parameters[3];
        //List<int>[] list = new List<int>[3];
            //,3,3));
                //new int[]={ 3,3,3});
                //parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });
     //   private List<int[]> list = new List<new >();
        //enum param
        //{
        //    frequency,
        //    fill,
        //    duration
        //}
    }
}
