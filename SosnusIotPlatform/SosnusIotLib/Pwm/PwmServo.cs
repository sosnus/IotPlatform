using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosnusIotLib.Pwm
{
    public class PwmServo : PwmCore
    {
        //frequency and width are in PwmBasic


        private const double fillMin=0.3; // typically 0.3ms //mean 1,5% of fill
        private const double fillMax=2.3; // typically 2.3ms //mean 11,5% of fill
        private const double fillDelta =fillMax - fillMin;    // set in constructor

        private const int pwmFrequency = 50; //typically 50Hz

        const double angleMax=120; //typically 120 or 150

        public PwmServo() { }

        public async void SetupServo(int _pinNumber)
        {
            //fillMin = 0.3; // typically 0.3
            //fillMax = 2.3; // typically 2.3
            //fillDelta = fillMax - fillMin;
            //angleMax = 120; //typically 120 or 150
            // Frequency = 50; //most of servos have 50Hz, 20ms
            // Frequency); I cannot use Frequency before SetupBasic method

            await SetupPwmCore(_pinNumber, pwmFrequency); 

        }

        public enum ServoPwmInputType
        {
            ServoAngle,
            ServoFill,
            PwmFill //Ahtung!
        }


        public void Set(double variable, ServoPwmInputType type)
        {
            double fillTemp = 0;

            switch (type)
            {
                case ServoPwmInputType.ServoAngle:
                    { //now Var can have between <0-120(150?)> but this isn't tested
                        fillTemp = (variable * fillDelta)/angleMax ;
                        fillTemp += fillMin;
                        fillTemp = (fillTemp * angleMax) / FrequencyToMiliseconds(Frequency); //change to Percent of fill (between 1.5% to 11.5%)
                    }
                    break;
                case ServoPwmInputType.ServoFill:
                    {
                        fillTemp = (variable * fillDelta) / 100; //fillTemp - how many ms i need add to fillMin?
                        fillTemp += fillMin; //fillTemp - was in ms, now add fillMin [ms]
                        fillTemp = (fillTemp*100)/ FrequencyToMiliseconds(Frequency); //change to Percent of fill (between 1.5% to 11.5%)
                    }
                    break;
                case ServoPwmInputType.PwmFill:
                    {
                        fillTemp = variable; // simple % (between 1.5% to 11.5%)
                    }
                    break;
            }
            //btween 0.3 to 2.3ms if not used PwmFill
            Fill = fillTemp; 
            // * 100; between 1.5 to 11.5 (mean 5)
            //Set(fillTemp);
            
        }


    }
}