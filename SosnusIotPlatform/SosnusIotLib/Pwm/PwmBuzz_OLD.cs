using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosnusIotLib.Pwm
{
    class PwmBuzz_OLD : PwmCore
    {
        private List<BuzzerEffectProperties> list = new List<BuzzerEffectProperties>();

        //Deklaracja listy tasków dla tego obiektu-pwmBuzz
        //struct or class? class becouse in struct I must define ALL parametrs
        public class BuzzerEffectProperties
        {
            BuzzerEffectProperties(double _frequency, double _signalDuration)
            {
                frequency = _frequency;
                signalDuration = _signalDuration;
                fill = 50;
                //fill? or Fill
            }
            public double fill;
            public double frequency;
            public double signalDuration;
        }

        public void addEffect(BuzzerEffectProperties tempBuzzerEffectProperties)
        {
            list.Add(tempBuzzerEffectProperties);
            BuzzerWorking = true;
        }


        /// <summary>
        /// get firs element from List<BuzzerEffectProperties> and set pwm properties on device
        /// </summary>
        public async void SetEffect()
        {
            if(list.Count !=0)
            {

                //var a = list.First().duty;
                //this.
                Frequency = list.First().frequency;
                Fill = list.First().fill;
                await Task.Delay(TimeSpan.FromMilliseconds(list.First().signalDuration));
                list.Remove(list.First());
                BuzzerWorking = true;
            }
        }

        private bool buzzerWorking = false;

        public bool BuzzerWorking
        {
            get
            {
                return buzzerWorking;
            }
            set
            {
                if(value) //if setter is true
                {
                    if(list.Count!=0) //when object in list is waiting
                    {
                        this.State = true; //startPwmPin
                        SetEffect();
                        buzzerWorking = true;
                    }
                    else //if list is empty set work on false
                    {
                        this.State = false; //stopPwmPin
                        buzzerWorking = false;
                    }
                }
                else
                {
                    Debug.WriteLine("It isn't possible!");
                    Debug.WriteLine("Problem with BuzzerWorking{get;set;}!");
                    //throw;
                    //if (list.Count != 0) ; //when object in list is waiting

                }

                //ustaw na false ale sprawdź jeszcze raz kolejkę
            }
        }



        public PwmBuzz_OLD() { }

        //public async void SetupBuzz(int _pinNumber)
        //{
        //    await SetupBasic(_pinNumber, 100); //this is temp frequency
        //    State = false;
        //}


        ///// <summary>
        ///// For generate one tone
        ///// </summary>
        ///// <param name="time">time of this signal (in ms)</param>
        ///// <param name="frequency">frequency of this signal (in Hz)</param>
        //public void Tone(double time, double frequency)
        //{
        //    //add to queue this functions:
        //    //pwm enable (working setter true)
        //    //set frequency
        //    //set timer
        //    //wait (or sleep?)
        //    //pwm disable (working setter false)
        //}

    }
}
