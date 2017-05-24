using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using SosnusIotLib.Io; 
using System.Threading;
//using System.Timers;

namespace LedDisplay7segment
{
    public sealed partial class MainPage : Page
    {

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

    }

    public sealed partial class MainPage : Page
    {

        //System.Timers.Timer aTimer = new System.Timers.Timer();


        private OutputBasic[] _segments = new OutputBasic[8];
        private OutputBasic[] _modules = new OutputBasic[4];
        int numb = 0;
        int activeDigit = 3;
        double timeSpanDisplay = 1 / 50 * 4;

        private Timer timer;

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();
        }

        private void InitGPIO() //there we will write all procedures connected with pins initializations
        {
            for (int i = 0; i < 8; i++) _segments[i] = new OutputBasic();

            for (int i = 0; i < 4; i++) _modules[i] = new OutputBasic();

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
                _modules[i].State = GpioPinValue.High;

            timer = new Timer(timerCallback, null, (int)TimeSpan.FromSeconds(timeSpanDisplay).TotalMilliseconds, Timeout.Infinite);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private /*async*/ void timerCallback(object state)
        {
            _modules[activeDigit].State = GpioPinValue.High;
            if (activeDigit < 3) activeDigit++;
            else activeDigit = 0;
            SetSegments(activeDigit * 2);
            _modules[activeDigit].State = GpioPinValue.Low;
            timer = new Timer(timerCallback, null, (int)TimeSpan.FromSeconds(timeSpanDisplay).TotalMilliseconds, Timeout.Infinite);
        }

        void SetSegments(int _digitToDisplay)
        {
            for (int i = 0; i < 8; i++)
                _segments[i].State = (GpioPinValue)numery[_digitToDisplay, i];
        }
    }
}
