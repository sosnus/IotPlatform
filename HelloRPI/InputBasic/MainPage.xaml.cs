using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace InputBasic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int numer_pinu = 20; //number of pin where we plug switch
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
