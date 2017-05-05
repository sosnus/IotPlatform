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

namespace PwmBuzzerSimpleTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        PwmBasic Buzzer = new PwmBasic();

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO(); //Initialize all input and output pins
        }



        private async void InitGPIO()
        {
            await Buzzer.SetupBasic(27, 100);

        }



        private void SetParam_Button_Click(object sender, RoutedEventArgs e)
        {
            if(Buzzer.Frequency !=70)
            {
                Buzzer.Frequency = 70;

            }
            else
            {
                Buzzer.Frequency = 1000;
            }
            //Buzzer.Frequency = sFrequency.Value*1000;
            tblFrequency.Text = $"{Buzzer.Frequency.ToString()} Hz";
        }

        private void EnablePwm_Button_Click(object sender, RoutedEventArgs e)
        {
            Buzzer.State = !Buzzer.State;
            tblPwmState.Text = Convert.ToString($"Pwm is {Buzzer.State}");
        }
    }
}
