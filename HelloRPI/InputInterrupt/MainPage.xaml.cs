using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace InputInterrupt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int numer_pinu = 22; //number of pin where we plug switch
                                           //one switch pin is still connected to ground
        private GpioPin mySwitch;

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }

        private void InitGPIO() //there we will write all procedures connected with pins initializations
        {
            var gpio = GpioController.GetDefault();
            mySwitch = gpio.OpenPin(numer_pinu); //initialization
            mySwitch.SetDriveMode(GpioPinDriveMode.InputPullUp); //Set directory (input/output)
            mySwitch.ValueChanged += MySwitch_ValueChanged;
        }

        private void MySwitch_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            var t = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (args.Edge == GpioPinEdge.FallingEdge)
                {
                    tblInfo.Text = $"LOW state on pin {numer_pinu}";
                }
                else
                {
                    tblInfo.Text = $"HIGH state on pin {numer_pinu}";
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mySwitch.Read() == GpioPinValue.Low)
            {
                tblInfo.Text = $"LOW state on pin {numer_pinu}";
            }
            else
            {
                tblInfo.Text = $"HIGH state on pin {numer_pinu}";
            }
        }
    }
}