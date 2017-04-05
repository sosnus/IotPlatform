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
using SosnusIotLib;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PwmServoTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PwmServo servo = new PwmServo();

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }

        private void InitGPIO()
        {
            servo.SetupServo(22);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double tempVar = Convert.ToDouble(sSlider.Value);
            while (tempVar > 100.0) tempVar /= 10.0;
            servo.Set(tempVar, PwmServo.PwmInputType.ServoFill);
            tblValue.Text = $"You set {tempVar}% fill on pwm";
        }

        private void sSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            double tempVar = Convert.ToDouble(e.NewValue);
            while (tempVar > 100.0) tempVar /= 10.0;
            servo.Set(tempVar, PwmServo.PwmInputType.ServoFill);
            tblValue.Text = $"You set {tempVar}% fill on pwm";
            //servo.Fill = Convert.ToDouble(e.NewValue / 100);
            //tblValue.Text = $"You set {e.NewValue / 100} fill on pwm";
        }
    }
}

