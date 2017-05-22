using System;
using Windows.Devices.Gpio;
using SosnusIotLib.Io;
using System.Threading;

namespace SosnusIotLib.MiscLib
{
    public partial class DigitDisplay
    {
        OutputBasic[] _segments = new OutputBasic[8];
        OutputBasic[] _modules = new OutputBasic[4];

        int[] digitsToDisplay = new int[4];

        private Timer timer;

        int modulesQuantity;
        int activeDigit;
        ErrorMode errorMode = ErrorMode.E;
        public double refreshFrequencyInMilliseconds;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="modules">Declare how many display modules You have \n (example - 8888- 4 modules, 88- 2 modules)</param>
        public DigitDisplay(int modules)
        {
            modulesQuantity = modules;
            activeDigit = modules - 1;
        }

        /// <summary>
        /// Set number for 7 segment display
        /// </summary>
        /// <param name="number">Number to display</param>
        /// <returns>If it isn't posible display whole number, return false</returns>
        public bool Set(int number)
        {
            if(number<(Math.Pow(10,modulesQuantity))&&number>=0)
            {
                for (int i = 0; i < digitsToDisplay.Length; i++)
                {
                    digitsToDisplay[i] = (number % 10);
                    number /= 10;
                }
                return true;
            }
            else if(number>((-1)*Math.Pow(10, modulesQuantity-1)) && number <= 0)
            {
                number *= (-1);
                for (int i = 0; i < digitsToDisplay.Length-1; i++)
                {
                    digitsToDisplay[i] = (number % 10);
                    number /= 10;
                }
                digitsToDisplay[digitsToDisplay.Length - 1] = (int)Digit.Dminus;
                return true;
            }
            else
            {
                for (int i = 0; i < digitsToDisplay.Length; i++)
                {
                    digitsToDisplay[i] = (int)errorMode;
                }
                return false;
            }
        }

        /// <summary>
        /// Setup most important things
        /// </summary>
        /// <param name="pinForModules">Enter array with pins for modules</param>
        /// <param name="pinForSegments">Enter array with pins for segments</param>
        /// <param name="refreshFrequencyInHz">Enter refresh frequency (it will be multiply by modules count</param>
        public void Setup(int[] pinForModules, int[] pinForSegments, int refreshFrequencyInHz)
        {
            refreshFrequencyInMilliseconds = refreshFrequencyInHz * pinForModules.Length;
            refreshFrequencyInMilliseconds /= 1000;
            refreshFrequencyInMilliseconds = 1 / refreshFrequencyInMilliseconds;

            for (int i = 0; i < modulesQuantity; i++)
            {
                _modules[i] = new OutputBasic();
                _modules[i].Setup(pinForModules[i], GpioPinDriveMode.Output);
                _modules[i].State = GpioPinValue.High;
            }
            for (int i = 0; i < 8; i++)
            {
                _segments[i] = new OutputBasic();
                _segments[i].Setup(pinForSegments[i], GpioPinDriveMode.Output);
            }
            
            timer = new Timer(timerCallback, null, (int)refreshFrequencyInMilliseconds, Timeout.Infinite);
        }

        private /*async*/ void timerCallback(object state)
        {
            _modules[activeDigit].State = GpioPinValue.High;
            if (activeDigit < 3) activeDigit++;
            else activeDigit = 0;
            SetSegments(digitsToDisplay[activeDigit]);
            _modules[activeDigit].State = GpioPinValue.Low;
            
            //timer = new Timer(timerCallback, null, (int)refreshFrequencyInMilliseconds, Timeout.Infinite);
            timer = new Timer(timerCallback, null, 0, Timeout.Infinite);
        }

        private void SetSegments(int _digitToDisplay)
        {
            for (int i = 0; i < 8; i++)
                _segments[i].State = (GpioPinValue)numery[_digitToDisplay, i];
        }
    }

    public partial class DigitDisplay
    {

        int[,] numery = new int[14, 8] { 
                //a,b,c,d,e,f,g,h
                { 0,0,0,0,0,0,1,1 }, // 0
                { 1,0,0,1,1,1,1,1 }, // 1
                { 0,0,1,0,0,1,0,1 }, // 2
                { 0,0,0,0,1,1,0,1 }, // 3
                { 1,0,0,1,1,0,0,1 }, // 4
                { 0,1,0,0,1,0,0,1 }, // 5
                { 0,1,0,0,0,0,0,1 }, // 6
                { 0,0,0,1,1,1,1,1 }, // 7
                { 0,0,0,0,0,0,0,1 }, // 8
                { 0,0,0,0,1,0,0,1 }, // 9
                { 1,1,1,1,1,1,0,1 }, // -
                { 1,1,1,1,1,1,1,0 },  // .
                { 0,1,1,0,0,0,0,0 }, // E
                { 1,1,1,1,1,1,1,1 } //blank
            };

        enum Digit
        {
            D0,
            D1,
            D2,
            D3,
            D4,
            D5,
            D6,
            D7,
            D8,
            D9,
            Dminus,
            Dp,
            DErr,
            Dblank
        }

        enum ErrorMode
        {
            nine = 9,
            line = 10,
            dot = 11,
            E = 12
        }

        enum Seg
        {
            a,
            b,
            c,
            d,
            e,
            f,
            g,
            h
        }

    }
}
