using Windows.Devices.Gpio;


namespace SosnusIotLib.Pwm
{
    public class PwmBasic : PwmCore
    {
        public PwmBasic() { }

        public async void SetupBasic(int _pinNumber, double _frequency)
        {
            await  SetupPwmCore(_pinNumber, _frequency); // Frequency); I cannot use Frequency before SetupBasic method
        }

        public void Set(double variable)
        {
            Fill = variable;
        }

    }
}
