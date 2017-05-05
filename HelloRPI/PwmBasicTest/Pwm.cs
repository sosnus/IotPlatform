using System;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
//If You have error in near line, maybe You forgot about NuGet package?
using Microsoft.IoT.DeviceCore.Pwm;
using Microsoft.IoT.Devices.Pwm;

namespace PwmBasicTest
{
    class Pwm
    {
        private PwmPin _pwmPin;
        private PwmController _pwmController;

        double width = 0;
        double frequency = 100; //init value

        public Pwm() { }

        public async void Setup(int _pinNumber, double _frequency)
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

        public void SetPosition(double _width)
        {
            width = _width; //TODO: get; set;
            _pwmPin.SetActiveDutyCyclePercentage(width); //between <0-1>
        }

    }
}
