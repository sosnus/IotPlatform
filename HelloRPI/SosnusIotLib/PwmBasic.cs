using System;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
//If You have error in near line, maybe You forgot about NuGet package?
using Microsoft.IoT.DeviceCore.Pwm;
using Microsoft.IoT.Devices.Pwm;
using System.Threading.Tasks;

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

        private double fill = 0; //init value between <0-100>
        //between 0.0-100.0
        public double Fill
        {
            get
            {
                return fill*100.0; //between 0.0-100.0
            }
            set
            {
                //value must be between 
                fill = (value/100);
                _pwmPin.SetActiveDutyCyclePercentage(fill/100.0); //between <0-1>

            }
        }

        /// <summary>
        /// This constructor is void, You must configure this class by this.SetupBasic()
        /// </summary>
        public PwmBasic() { }

        public async 
        Task SetupBasic(int _pinNumber, double _frequency)
        {
            var gpioController = GpioController.GetDefault();
            var pwmManager = new PwmProviderManager();
            pwmManager.Providers.Add(new SoftPwm());

            var pwmControllers = await pwmManager.GetControllersAsync();

            _pwmController = pwmControllers[0];
            Frequency = _frequency;
            _pwmController.SetDesiredFrequency(frequency);

            _pwmPin = _pwmController.OpenPin(_pinNumber);
            _pwmPin.Start();
        }

        /// <summary>
        /// Set fill of PWM
        /// </summary>
        /// <param name="_fill">must be between 0.0 to 100.0</param>
        public void Set(double _fill)
        {
            Fill = _fill;
        }



        public void Stop()
        {
            _pwmPin.Stop();
        }

        public void Start()
        {
            _pwmPin.Start();
        }
    }
}
