using System;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
//If You have error in near line, maybe You forgot about NuGet package?
using Microsoft.IoT.DeviceCore.Pwm;
using Microsoft.IoT.Devices.Pwm;

namespace SosnusIotLib
{
    public class PwmBasic
    {
        private PwmPin _pwmPin;
        private PwmController _pwmController;

        private double frequency = 100; //init value
        public double Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                //value must be between 
                frequency = value;
            }
        }

        private double width = 100; //init value
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                //value must be between 
                width = value;
                _pwmPin.SetActiveDutyCyclePercentage(width); //between <0-1>

            }
        }

        /// <summary>
        /// This constructor is void, You must configure this class by this.SetupBasic()
        /// </summary>
        public PwmBasic() { }

        public async void SetupBasic(int _pinNumber, double _frequency)
        {
            var gpioController = GpioController.GetDefault();
            var pwmManager = new PwmProviderManager();
            pwmManager.Providers.Add(new SoftPwm());

            var pwmControllers = await pwmManager.GetControllersAsync();

            _pwmController = pwmControllers[0];
            frequency = _frequency; //TODO: get; set;
            _pwmController.SetDesiredFrequency(frequency);

            _pwmPin = _pwmController.OpenPin(_pinNumber);
            _pwmPin.Start();
        }
    }
}
