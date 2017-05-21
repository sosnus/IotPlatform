using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;

using SosnusIotLib.Io; //.OutputBasic;
using System.Threading;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LedDisplay7segment
{



    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private GpioPin led[5]; //class with led pin
        //private GpioPin[] led = new GpioPin[5];
        private OutputBasic[] _segments = new OutputBasic[8];
        private OutputBasic[] _modules = new OutputBasic[8];
        int numb = 0;
        int activeDigit = 0;

        private Timer timer;

        int[,] numery = new int[12, 8] { 
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
                { 1,1,1,1,1,1,1,0 }  // .
            };

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();
            InitTimer();
        }


        void InitTimer()
        {
            this.InitializeComponent();
            timer = new Timer(timerCallback, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, Timeout.Infinite);

        }

        private /*async*/ void timerCallback(object state)
        {
                _modules[activeDigit].State = GpioPinValue.Low;
            if (activeDigit != 4) activeDigit++;
            else activeDigit = 0;
                _modules[activeDigit].State = GpioPinValue.High;
            SetSegments(activeDigit * 2);
        }


        enum Seg //: int
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

        //const int dis7_DigitTemplates[0] = new int { 2,2,2};

            void SetSegments(int _digitToDisplay)
        {
            for (int i = 0; i < 8; i++)
            {
                _segments[i].State = (GpioPinValue)numery[_digitToDisplay, i];
            }
        }


        private void InitGPIO() //there we will write all procedures connected with pins initializations
        {
            for (int i = 0; i < 8; i++)
                _segments[i] = new OutputBasic();

            for (int i = 0; i < 4; i++)
                _modules[i] = new OutputBasic();

            _segments[(int)Seg.a].Setup(20, GpioPinDriveMode.Output);
            _segments[(int)Seg.b].Setup(16, GpioPinDriveMode.Output);
            _segments[(int)Seg.c].Setup(21, GpioPinDriveMode.Output);
            _segments[(int)Seg.d].Setup(05, GpioPinDriveMode.Output);
            _segments[(int)Seg.e].Setup(06, GpioPinDriveMode.Output);
            _segments[(int)Seg.f].Setup(13, GpioPinDriveMode.Output);
            _segments[(int)Seg.g].Setup(19, GpioPinDriveMode.Output);
            _segments[(int)Seg.h].Setup(26, GpioPinDriveMode.Output);

            _modules[0].Setup(04, GpioPinDriveMode.Output);
            _modules[1].Setup(17, GpioPinDriveMode.Output);
            _modules[2].Setup(27, GpioPinDriveMode.Output);
            _modules[3].Setup(22, GpioPinDriveMode.Output);

            for (int i = 0; i < 4; i++)
            {
                _modules[i].State = GpioPinValue.High;

            }

            //foreach (var item in _modules)
            //    item.State = GpioPinValue.High;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (numb == 12) numb = 0;
            //for (int i = 0; i < _segments.Length; i++) _segments[i].State = (GpioPinValue)numery[numb, i];
            //numb++;

            _modules[activeDigit].State = GpioPinValue.Low;
            if (activeDigit != 4) activeDigit++;
            else activeDigit = 0;
            _modules[activeDigit].State = GpioPinValue.High;
            SetSegments(activeDigit * 2);
        }
    }
}
