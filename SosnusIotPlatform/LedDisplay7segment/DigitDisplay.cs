using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using SosnusIotLib.Io;
using System.Threading;
using System.Threading.Tasks;

namespace LedDisplay7segment
{
    public partial class DigitDisplay
    {
        OutputBasic[] _segments;
        OutputBasic[] _modules;

        int[] digitsToDisplay;

        private Timer timer;

        int modulesQuantity;
        int activeDigit;
        int refreshFrequencyInMilliseconds;

        DigitDisplay(int modules)
        {
            modulesQuantity = modules;
            activeDigit = modules - 1;

            OutputBasic[] _segments = new OutputBasic[8];
            OutputBasic[] _modules = new OutputBasic[modulesQuantity];

            int[] digitsToDisplay = new int[modulesQuantity];
        }

        bool display(int number)
        {
            return true;
        }

        void Setup(int[] pinForModules, int[] pinForLeds, int refreshFrequencyInHz)
        {
            for (int i = 0; i < 8; i++)
            {
                _segments[i] = new OutputBasic();
                _segments[i].Setup(pinForLeds[i], GpioPinDriveMode.Output);
            }
            for (int i = 0; i < modulesQuantity; i++)
            {
                _modules[i] = new OutputBasic();
                _modules[i].Setup(pinForModules[i], GpioPinDriveMode.Output);
                _modules[i].State = GpioPinValue.High;
            }

            timer = new Timer(timerCallback, null, refreshFrequencyInMilliseconds, Timeout.Infinite);
        }

        private /*async*/ void timerCallback(object state)
        {
            _modules[activeDigit].State = GpioPinValue.High;
            if (activeDigit < 3) activeDigit++;
            else activeDigit = 0;
            SetSegments(digitsToDisplay[activeDigit]);
            _modules[activeDigit].State = GpioPinValue.Low;
            timer = new Timer(timerCallback, null, refreshFrequencyInMilliseconds, Timeout.Infinite);
        }

        void SetSegments(int _digitToDisplay)
        {
            for (int i = 0; i < 8; i++)
                _segments[i].State = (GpioPinValue)numery[_digitToDisplay, i];
        }
    }

    public partial class DigitDisplay
    {

        int[,] numery = new int[13, 8] { 
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
                { 0,1,0,0,0,1,0,1 } // 6
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
            DErr
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
