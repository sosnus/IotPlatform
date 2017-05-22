using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SosnusIotLib.MiscLib;

namespace LedDisplaySimpleUsing
{
    public sealed partial class MainPage : Page
    {
        DigitDisplay wyswietlacz; 

        public MainPage()
        {
            this.InitializeComponent();
            wyswietlacz = new DigitDisplay(4);
            int[] modulesArg = new int[] {04, 17, 27, 22}; //pins GPIO from RPI for modules
            int[] segmenstArg = new int[] {20, 16, 21, 05, 06, 13, 19, 26}; //pins GPIO from RPI for segments
            wyswietlacz.Setup(modulesArg, segmenstArg, 50);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int liczbaTemp;
            if(int.TryParse(tbDigitsEnter.Text,out liczbaTemp)) //bad convert protection
                wyswietlacz.Set(liczbaTemp);
        }
    }
}