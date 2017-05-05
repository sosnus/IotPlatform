using System;
using Windows.Devices.Gpio;
using Windows.Devices.Pwm;
//If You have error in near line, maybe You forgot about NuGet package?
using Microsoft.IoT.DeviceCore.Pwm;
using Microsoft.IoT.Devices.Pwm;
using System.Threading.Tasks;

namespace SosnusIotLib.Pwm
{
    public class PwmCore
    {
        public PwmCore() { }

        private PwmPin _pwmPin;
        private PwmController _pwmController;

        private double frequency = 100;
        private bool state = true;
        private double fill = 0; //range between <0-100>

        /// <summary>
        /// Simple method to convert frequency to perioid (T=1/f) and convert seconds to miliseconds
        /// </summary>
        /// <param name="_frequency">frequency (in Hz)</param>
        /// <returns>miliseconds for one cycle</returns>
        public double FrequencyToMiliseconds(double _frequency)
        {
            return 1000.0 / _frequency; //return miliseconds, for f=50, return 20 (ms)
        }

        /// <summary>
        /// This constructor is void, You must configure this class by this.SetupBasic()
        /// </summary>

        /// <summary>
        /// Initialize of pwm (it must be used after constructor PwmBasic() )
        /// </summary>
        /// <param name="__pinNumber">Pin of RPi where this pwm must be work</param>
        /// <param name="__frequency">Frequency of pwm (can change this param later)</param>
        /// <returns></returns>
        public async Task SetupPwmCore(int __pinNumber, double __frequency)
        {
            var gpioController = GpioController.GetDefault();
            var pwmManager = new PwmProviderManager();
            pwmManager.Providers.Add(new SoftPwm());

            var pwmControllers = await pwmManager.GetControllersAsync();

            _pwmController = pwmControllers[0];
            Frequency = __frequency;
            _pwmController.SetDesiredFrequency(frequency);

            _pwmPin = _pwmController.OpenPin(__pinNumber);
            State = true;
        }

        /// <summary>
        /// Get or Set frequency of pwm (in Hz)
        /// </summary>
        public double Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                //eventually check: if(value>=40 && value<=1000)
                _pwmController.SetDesiredFrequency(value);
                frequency = value;
            }
        }

        /// <summary>
        /// Get or Set fill of pwm
        /// Range between 0.0 to 100.0 (mean 0% to 100%)
        /// </summary>
        public double Fill
        {
            get
            {
                return fill * 100.0; //get <0.0-100.0>
            }
            set
            {
                //if (value > 100) throw Exception; //TODO
                fill = (value / 100);
                _pwmPin.SetActiveDutyCyclePercentage(fill); //set <0-1>
            }
        }

        /// <summary>
        /// Get state of pwm (is working or not)
        /// Set state of pwm (for enable/disable pwm
        /// </summary>
        public bool State
        {
            get
            {
                return state;
            }
            set
            {
                if(value)// == true)
                {
                    _pwmPin.Start();
                    state = true;
                }
                else
                {
                    _pwmPin.Stop();
                    state = false;
                }
            }
        } //end of State
    }
}
