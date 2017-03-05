using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Blink
{
    public sealed partial class MainPage : Page
    {
        private const int numer_pinu = 2; //number of pin where we plug led
        private GpioPin led; //class with led pin

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }

        private void InitGPIO() //there we will write all procedures connected with pins initializations
        {
            var gpio = GpioController.GetDefault();
            led = gpio.OpenPin(numer_pinu); //initialization
            led.Write(GpioPinValue.Low); //set LOW state (GND, 0V) on led
            led.SetDriveMode(GpioPinDriveMode.Output); //Set directory (input/output)
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GpioPinValue stateTemp = led.Read(); //check state on led
            if (stateTemp == GpioPinValue.High) led.Write(GpioPinValue.Low);
            else led.Write(GpioPinValue.High);
        }

        private void Button_ClickOn(object sender, RoutedEventArgs e)
        {
            led.Write(GpioPinValue.Low); //Set low state on led
        }
        private void Button_ClickOff(object sender, RoutedEventArgs e)
        {
            led.Write(GpioPinValue.High); //Set high state on led
        }
    }
}