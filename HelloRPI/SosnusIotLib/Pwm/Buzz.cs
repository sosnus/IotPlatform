using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosnusIotLib.Pwm
{
    class Buzz : PwmBasic
    {
        //  PwmServo buzz = new PwmServo();
        Buzz buzz = new Buzz();
        public Buzz() { }

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
            //pwm enable
            //set frequency
            //set timer
            //add pwm.stop to queue
        }

    }
}
