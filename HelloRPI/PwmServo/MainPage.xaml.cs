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

//using SosnusIotLib;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PwmServo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PwmBasic mojLed1 = new PwmBasic();
        PwmBasic mojLed2 = new PwmBasic();

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }

        private void InitGPIO()
        {
            mojLed1.SetupBasic(5, 100); //led with pwm on pin 5 with frequency=100
            mojLed2.SetupBasic(13, 100); //led with pwm on pin 13 with frequency=100
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //!! This method is simple, so it isn't protected from bad input data
            mojLed1.Width = Convert.ToDouble(tbWidth.Text);
            tblValue.Text = $"You set {tbWidth.Text} fill on pwm";
        }

        private void sSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            mojLed2.Width = Convert.ToDouble(e.NewValue / 100);
            tblValue.Text = $"You set {e.NewValue / 100} fill on pwm";
        }
    }
}

