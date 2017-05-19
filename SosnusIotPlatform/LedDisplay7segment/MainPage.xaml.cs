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
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;

using SosnusIotLib.Io; //.OutputBasic;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LedDisplay7segment
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private GpioPin led[5]; //class with led pin
        //private GpioPin[] led = new GpioPin[5];
        private OutputBasic[] led = new OutputBasic[8];


        //= {{3} };


        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();

            int[,] numery = new int[12,8] { 
                //a,b,c,d,e,f,g,h
                { 0,0,0,0,0,0,1,1 }, // 0
                { 1,0,0,1,1,1,1,1 }, // 1
                { 0,0,1,0,0,1,0,1 }, // 2
                { 0,0,0,0,1,1,0,1 }, // 3
                { 1,0,0,1,1,0,0,1 }, // 4
                { 0,1,0,0,1,0,0,1 }, // 5
                { 0,1,0,0,0,0,0,1 }, // 6
                { 0,0,0,1,1,1,0,1 }, // 7
                { 0,0,0,0,0,0,0,1 }, // 8
                { 0,0,0,0,1,0,0,1 }, // 9
                { 1,1,1,1,1,1,0,1 }, // -
                { 1,1,1,1,1,1,1,0 }  // .
            };

            for (int i = 0; i < led.Length; i++)  led[i].State = (GpioPinValue)numery[2, i];

            //int[,] dis7_DigitTemplates = new int[1, 1];
            //dis7_DigitTemplates[1] = 

        }



        //const int dis7_DigitTemplates[0] = new int { 2,2,2};

        private void InitGPIO() //there we will write all procedures connected with pins initializations
        {


            int[] dis7_segments = new int[8];
            int[] dis7_module = new int[8];
            var gpio = GpioController.GetDefault();
            led[0] = gpio.OpenPin(22); //initialization
            led[0].Write(GpioPinValue.Low); //set LOW state (GND, 0V) on led
            led[0].SetDriveMode(GpioPinDriveMode.Output); //Set directory (input/output)
        }
    }
}
