//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Windows.Devices.Gpio;


namespace SosnusIotLib.Io
{
    public class OutputBasic
    {

        //private const int numer_pinu = 2; //number of pin where we plug led
        GpioPin gpioPin; //class with led pin

        OutputBasic(int pinNumber, GpioPinDriveMode _mode)
        {
            GpioController gpio = GpioController.GetDefault();
            gpioPin = gpio.OpenPin(pinNumber); //initialization
            gpioPin.Write(GpioPinValue.Low); //set LOW state (GND, 0V) on led
            gpioPin.SetDriveMode(_mode); // GpioPinDriveMode.Output); //Set directory (input/output)
        }

        private GpioPinValue state;

        //State { get; Set; }

        public GpioPinValue State
        {
            get { return state; }
        set
        {
                gpioPin.Write(value);
                state = value;
        }
    }

        void StateToggle()
        {
            if(gpioPin.Read() == GpioPinValue.High)
                gpioPin.Write(GpioPinValue.Low);
            else
                gpioPin.Write(GpioPinValue.High);
        }


    //void Set(GpioPinValue state)
    //    {
    //        gpioPin.Write(state);
    //    }

    }
}
