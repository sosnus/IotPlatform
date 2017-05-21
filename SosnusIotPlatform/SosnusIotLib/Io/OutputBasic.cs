using Windows.Devices.Gpio;

namespace SosnusIotLib.Io
{
    public class OutputBasic
    {
        GpioPin _gpioPin;

        public OutputBasic() { }

        public void Setup(int pinNumber, GpioPinDriveMode _mode)
        {
            GpioController gpio = GpioController.GetDefault();
            _gpioPin = gpio.OpenPin(pinNumber); //initialization
            _gpioPin.Write(GpioPinValue.Low); //set LOW state (GND, 0V) on led
            _gpioPin.SetDriveMode(_mode); // GpioPinDriveMode.Output); //Set directory (input/output)
        }

        private GpioPinValue state;

        public GpioPinValue State
        {
            get
            {
                return state;
            }
            set
            {
                _gpioPin.Write(value);
                state = value;
            }
        }



        void StateToggle()
        {
            if (_gpioPin.Read() == GpioPinValue.High)
                _gpioPin.Write(GpioPinValue.Low);
            else
                _gpioPin.Write(GpioPinValue.High);
        }

        //void Set(GpioPinValue state)
        //    {
        //        gpioPin.Write(state);
        //    }

    }
}
