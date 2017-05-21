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

using SosnusIotLib.MiscLib;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LedDisplaySimpleUsing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DigitDisplay wyswietlacz; 
        public MainPage()
        {
            this.InitializeComponent();
            wyswietlacz = new DigitDisplay(4);
            int[] modulesArg = new int[] {04, 17, 27, 22};
            int[] segmenstArg = new int[] {20, 16, 21, 05, 06, 13, 19, 26};
            wyswietlacz.Setup(modulesArg, segmenstArg, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int liczbaTemp;
            if(int.TryParse(tbDigitsEnter.Text,out liczbaTemp))
            wyswietlacz.Set(liczbaTemp);
            wyswietlacz.refreshFrequencyInMilliseconds = liczbaTemp;
        }
    }
}
