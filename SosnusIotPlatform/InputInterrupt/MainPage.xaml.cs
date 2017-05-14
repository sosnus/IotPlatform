using Windows.Devices.Gpio;
using Windows.UI.Xaml.Controls;

namespace InputInterrupt
{
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
                    tblInfo.Text = $"LOW state on pin {numer_pinu}";
                else
                    tblInfo.Text = $"HIGH state on pin {numer_pinu}";
            });
        }

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