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

namespace EEZYbot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PwmServo podstawa = new PwmServo();
        PwmServo ramie1 = new PwmServo();
        PwmServo ramie2 = new PwmServo();

        public MainPage()
        {
            this.InitializeComponent();
            HardwareInit();
        }

        private void HardwareInit()
        {
            podstawa.SetupServo(18);
            ramie1.SetupServo(23);
            ramie2.SetupServo(24);
        }

        private void sSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            podstawa.Set(Convert.ToDouble(e.NewValue), PwmServo.ServoPwmInputType.ServoFill);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            podstawa.State ^= true;
            ramie1.State ^= true;
            ramie2.State ^= true;
        }

        private void sSlider_ValueChanged2(object sender, RangeBaseValueChangedEventArgs e)
        {
            ramie2.Set(Convert.ToDouble(e.NewValue), PwmServo.ServoPwmInputType.ServoFill);

        }
        private void sSlider_ValueChanged3(object sender, RangeBaseValueChangedEventArgs e)
        {
            ramie1.Set(Convert.ToDouble(e.NewValue), PwmServo.ServoPwmInputType.ServoFill);

        }
    }
}
