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

using SosnusIotLib.Pwm;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PwmServoAndLedLibTest
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PwmBasic led = new PwmBasic();
        PwmServo servo = new PwmServo();
        PwmServo360 wheel = new PwmServo360();

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }

        private void InitGPIO()
        {
            led.SetupBasic(6, 100);
            servo.SetupServo(22);
            wheel.SetupServo360(23);
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            string name = (sender as Slider).Name;

            if (name == "sSliderLed")
            {
                led.Set(Convert.ToDouble(e.NewValue));
                tblValueLed.Text = $"You set {led.Fill}% fill";
            }
            else if (name == "sSliderServo")
            {
                servo.Set(Convert.ToDouble(e.NewValue), PwmServo.ServoPwmInputType.ServoFill);
                tblValueServo.Text = $"You set {servo.Fill}% fill";

            }
            else //((sender as Slider).Name == "sSliderServo360")
            {
                wheel.SetServo360(Convert.ToDouble(e.NewValue)*2-100);
                tblValueServo360.Text = $"You set {wheel.Fill}% fill";
            }
        }

        private void btnPwmState(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;

            if (name == "btnPwmLed")
            {
                led.State ^= true;
                btnPwmLed.Content = Convert.ToString($"pwm is {led.State}");
            }
            else if (name == "btnPwmServo")
            {
                servo.State ^= true;
                btnPwmServo.Content = Convert.ToString($"pwm is {servo.State}");
            }
            else //(name == "btnPwmServo360")
            {
                wheel.State ^= true;
                btnPwmServo360.Content = Convert.ToString($"pwm is {wheel.State}");
            }
        }

    }
}