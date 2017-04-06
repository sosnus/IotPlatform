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

        private void sSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            servo.Set(Convert.ToDouble(e.NewValue), PwmServo.PwmInputType.ServoFill);
            tblValue.Text = $"You set {servo.Fill}% fill";
        }
    }
}

