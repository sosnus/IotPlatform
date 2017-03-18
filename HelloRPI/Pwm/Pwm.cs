using System;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
using Microsoft.IoT.DeviceCore.Pwm;
using Microsoft.IoT.Devices.Pwm;

namespace Pwm
{
    class Pwm
    {
        private PwmPin _pwmPin;
        private PwmController _pwmController;

        const double frequency = 50;
        double width = 0;

        public Pwm() { }

        public async void Setup(int _pinNumber)
        {
            var gpioController = GpioController.GetDefault();
            var pwmManager = new PwmProviderManager();
            pwmManager.Providers.Add(new SoftPwm());

            var pwmControllers = await pwmManager.GetControllersAsync();

            //use the first available PWM controller an set refresh rate (Hz)
            _pwmController = pwmControllers[0]; //wut?
            _pwmController.SetDesiredFrequency(frequency);

            _pwmPin = _pwmController.OpenPin(_pinNumber);
            _pwmPin.Start();
        }

        public void SetPosition(double _position)
        {
            _pwmPin.SetActiveDutyCyclePercentage(width); //temporary between <0-1>
        }

    }
}
