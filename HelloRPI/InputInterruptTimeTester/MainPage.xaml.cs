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
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace InputInterruptTimeTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private GpioPin tactSwitch;
        private GpioPin led; //class with led pin

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }

        private void InitGPIO() //there we will write all procedures connected with pins initializations
        {
            var gpioSwitch = GpioController.GetDefault();
            tactSwitch = gpioSwitch.OpenPin(22); //initialization
            tactSwitch.SetDriveMode(GpioPinDriveMode.InputPullUp); //Set directory (input/output)
            tactSwitch.ValueChanged += MySwitch_ValueChanged;

            var gpioLed = GpioController.GetDefault();
            led = gpioLed.OpenPin(5); //initialization
            led.Write(GpioPinValue.High); //set LOW state (GND, 0V) on led
            led.SetDriveMode(GpioPinDriveMode.Output); //Set directory (input/output)
        }

        private void MySwitch_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            var t = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    led.Write(GpioPinValue.Low); //Set low state on led
                }
                else
                {
                    led.Write(GpioPinValue.High); //Set low state on led
                }
            });
        }





        //        cnt++;

        //   tblInfo.Text = $"LOW_Cnt={cnt}";
        //tblInfo.Text = $"Cnt={cnt}";
        //var watch = System.Diagnostics.Stopwatch.StartNew();
        //watch.Stop();
        //var elapsedTicks = watch.ElapsedTicks;
        //var elapsedMs = watch.ElapsedMilliseconds;
        //    tblInfo.Text = $"HIGH_Cnt={cnt}  \n Ticks={elapsedTicks} \n Ms={elapsedMs}";

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (mySwitch.Read() == GpioPinValue.Low)
        //    {
        //        tblInfo.Text = $"LOW state on pin {numer_pinu}";
        //    }
        //    else
        //    {
        //        tblInfo.Text = $"HIGH state on pin {numer_pinu}";
        //    }
        //}
    }
}