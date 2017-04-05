using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SosnusIotLib
{
    public class PwmServo : PwmBasic
    {
        //frequency and width are in PwmBasic


        private double fillMin; // = 0.3; // typically 0.3
        private double fillMax; // = 0.3; // typically 2.3
        private double fillDelta;     // set in constructor

        double angleMax; // = 120; //typically 120 or 150


        public PwmServo() { }

        public async void SetupServo(int _pinNumber)
        {
            fillMin = 0.3; // typically 0.3
            fillMax = 2.3; // typically 2.3
            fillDelta = fillMax - fillMin;

            Frequency = 50; //most of servos have 50Hz, 20ms

            angleMax = 120; //typically 120 or 150

            await SetupBasic(_pinNumber, Frequency);
        }



        public enum PwmInputType
        {
            ServoAngle,
            ServoFill,
            PwmFill ///Ahtung!
        }

            private double fillTemp;

        public void Set(double variable, PwmInputType type)
        {
            fillTemp = 0; //na wszelki wypadek
            switch (type)
            {
                case PwmInputType.ServoAngle:
                    {
                        fillTemp = (variable * fillDelta)/angleMax ;
                        fillTemp += fillMin;
                        fillTemp *= Frequency;
                     //   fillTemp *= 100;
                        // fillTemp = fillTemp / (1 / Frequency);
                    }
                    break;
                case PwmInputType.ServoFill:
                    {
                        fillTemp = (variable * fillDelta) / 100;
                        fillTemp += fillMin;
                        fillTemp *= Frequency;
                       // fillTemp *= 100;
                    }
                    break;
                case PwmInputType.PwmFill:
                    {
                        fillTemp = variable;
                    }
                    break;
            }
            //btween 0.3 to 2.3ms if not used PwmFill
            Set(fillTemp);
            
        }


    }
}