using Windows.Devices.Gpio;
using Windows.UI.Xaml.Controls;

namespace InputInterruptTimeTester
{
    public sealed partial class MainPage : Page
    {
        private GpioPin tactSwitch;
        private GpioPin led;

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();
        }

        private void InitGPIO() //there we will write all procedures connected with pins initializations
        {
            var gpioSwitch = GpioController.GetDefault();
            tactSwitch = gpioSwitch.OpenPin(22); 
            tactSwitch.SetDriveMode(GpioPinDriveMode.InputPullUp);
            tactSwitch.ValueChanged += MySwitch_ValueChanged;

            var gpioLed = GpioController.GetDefault();
            led = gpioLed.OpenPin(5);
            led.SetDriveMode(GpioPinDriveMode.Output);
        }

        //simple scenario
        private void MySwitch_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
                if (args.Edge == GpioPinEdge.FallingEdge) led.Write(GpioPinValue.Low); 
                else                                      led.Write(GpioPinValue.High);
        }

        /*
        //advanced scenario
        int stateChangeCnt = 0;
        private void MySwitch_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //remember that Stopwatch is enabled in MySwitch_ValueChanged(), not while when GPIO RPi change state
            stateChangeCnt++;
            var t = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    led.Write(GpioPinValue.Low);
                }
                else
                {
                    led.Write(GpioPinValue.High);
                }
                tblInfo.Text = $"Switch state: {tactSwitch.Read().ToString()}, Cnt={stateChangeCnt}";
                watch.Stop();
                tblInfo.Text += $"\nTicks={watch.ElapsedTicks} \nMilliseconds={watch.ElapsedMilliseconds}";

            });
        }
        */

        /*
        //simple scenario (but ready to change UI)
        private void MySwitch_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            var t = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    led.Write(GpioPinValue.Low); 
                }
                else
                {
                    led.Write(GpioPinValue.High);
                }
            });
        }
        */
    }
}