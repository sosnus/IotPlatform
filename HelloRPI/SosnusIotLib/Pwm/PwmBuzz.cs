using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosnusIotLib.Pwm
{
    class PwmBuzz : PwmBasic
    {
        //Deklaracja listy tasków dla tego obiektu-pwmBuzz


        public PwmBuzz() { }

        public async void SetupBuzz(int _pinNumber)
        {
            await SetupBasic(_pinNumber, 100); //this is temp frequency
            State = false;
        }


        /// <summary>
        /// For generate one tone
        /// </summary>
        /// <param name="time">time of this signal (in ms)</param>
        /// <param name="frequency">frequency of this signal (in Hz)</param>
        public void Tone(double time, double frequency)
        {
            //add to queue this functions:
            //pwm enable
            //set frequency
            //set timer
            //wait (or sleep?)
            //pwm disable
        }

    }
}
