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
using Windows.UI;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.System;
using System.Diagnostics;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace InputKeyboardMouseBasic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += AcceleratorKeyActivated;
            GridMain.PointerMoved += new PointerEventHandler(Pointer_Moved);
        }

        private void AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (args.EventType.ToString().Contains("Down"))
            {
                spKeyboard.Children.Insert(0, (new TextBlock()
                {
                    Text = Convert.ToString($"Pressed {args.VirtualKey} at " + DateTime.Now.ToString("h:mm:ss fffffff"))
                }));
            }
        }

        void Pointer_Moved(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint currentPoint = e.GetCurrentPoint(GridMain);
            spMouse.Children.Insert(0, (new TextBlock()
            {
                Text = Convert.ToString($"Cursor on X={Math.Round(currentPoint.Position.X, 0)} Y={Math.Round(currentPoint.Position.Y, 0)} at " + DateTime.Now.ToString("h:mm:ss fffffff"))
            }));
        }

    }
}
