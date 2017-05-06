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
            wheel.SetupServo360(18);
        }



        private void sSlider_ValueChangedLed(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            led.Fill = (Convert.ToDouble(e.NewValue));
            tblValueLed.Text = $"You set {led.Fill}% fill";
        }

        private void btnPwmEnable_ClickLed(object sender, RoutedEventArgs e)
        {
            led.State = !led.State;
            btnPwmEnableLed.Content = Convert.ToString($"pwm is {led.State}");
        }


        private void sSlider_ValueChangedServo(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            servo.Set(Convert.ToDouble(e.NewValue), PwmServo.ServoPwmInputType.ServoFill);
            tblValueServo.Text = $"You set {servo.Fill}% fill";
        }

        private void btnPwmEnable_ClickServo(object sender, RoutedEventArgs e)
        {
            servo.State = !servo.State;
            btnPwmEnableServo.Content = Convert.ToString($"pwm is {servo.State}");
        }
    }
}
