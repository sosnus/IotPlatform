using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pwm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Pwm mojLed = new Pwm();
        Pwm mojLed2 = new Pwm();

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }

        private void InitGPIO()
        {
            mojLed.Setup(5,100); //led with pwm on pin 5 with frequency=100
            mojLed2.Setup(13,100); //led with pwm on pin 5 with frequency=100
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //!! This method is simple, so it isn't protected from bad input data
            mojLed.SetPosition(Convert.ToDouble(tbWidth.Text));
            tblValue.Text = $"You set {tbWidth.Text} fill on pwm";
        }

        private void sSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            mojLed2.SetPosition(Convert.ToDouble(e.NewValue / 100));
            tblValue.Text = $"You set {e.NewValue/100} fill on pwm";
        }
    }

    
}
