namespace SosnusIotLib.Pwm
{
    class PwmServo360 : PwmCore
    {
        public PwmServo360() { }

        private const double fillMin = 0.8; 
        private const double fillMax = 2.2; 
        private const double fillNeutral = 1.5; 
        private const double fillDelta = fillMax - fillMin; 
        private const int pwmFrequency = 50; //typically 50Hz

        public async void SetupServo360(int _pinNumber)
        {
            await SetupPwmCore(_pinNumber, pwmFrequency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wheelFill">between {-100;100 }</param>
        public void SetServo360(double wheelFill)
        {
            //between {-100;100 } to between {4;11,5 }
            Fill = percentPowerToFill(wheelFill);
        }

        private double percentPowerToFill(double percent)
        {
            // -100% mean 0.8ms mean 4%
            //    0% mean 1.5ms
            //  100% mean 2.3ms mean 11,5%
            //TODO: check algoritm
            double fillTemp = 0;
            percent += 100;
            percent /= 2;
            fillTemp = (percent * fillDelta) / 100; //fillTemp - how many ms i need add to fillMin?
            fillTemp += percent; //fillTemp - was in ms, now add fillMin [ms]
            fillTemp = (percent * 100) / 20; //change to Percent of fill (between 1.5% to 11.5%)

            return fillTemp;
        }

        public void WheelStop()
        {
            SetServo360(0);
        }

        public void WheelStopForce()
        {
            SetServo360(0);
            State = false;
        }
    }
}
