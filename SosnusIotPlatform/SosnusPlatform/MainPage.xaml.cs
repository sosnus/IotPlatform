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

namespace SosnusPlatform
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PwmServo360 wheelLeft = new PwmServo360();
        PwmServo360 wheelRight = new PwmServo360();



        public MainPage()
        {
            this.InitializeComponent();
            InitHardware();
            GridMain.PointerWheelChanged += new PointerEventHandler(Pointer_Wheel_Changed);

        }

        void Pointer_Wheel_Changed(object sender, PointerRoutedEventArgs e)
        {
            ScrollCnt += pointerProperties.MouseWheelDelta / 120;
            scrollTbl.Text = $" ScrollCnt = {ScrollCnt}";
        }

        private void InitHardware()
        {
            wheelLeft.SetupServo360(23);
            wheelLeft.SetupServo360(24);
            //throw new NotImplementedException();
        }
    }
}
