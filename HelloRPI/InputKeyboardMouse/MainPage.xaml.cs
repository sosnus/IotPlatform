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

namespace InputKeyboardMouse
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



        void Pointer_Moved(object sender, PointerRoutedEventArgs e)
        {
            // Retrieve the point associated with the current event
            Windows.UI.Input.PointerPoint currentPoint = e.GetCurrentPoint(GridMain);
            spMouse.Children.Add(new TextBlock()
            {
                Text = Convert.ToString($"Pressed {currentPoint.Position.X} at " + DateTime.Now.ToString("h:mm:ss fffffff"))
            });

            spMouse.Children.Add(new TextBlock()
            {
                Text = Convert.ToString($"Pressed {currentPoint.Position.Y} at " + DateTime.Now.ToString("h:mm:ss fffffff"))
            });
        }


        private void AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (args.EventType.ToString().Contains("Down"))
            {
                spKey.Children.Add(new TextBlock() {
                    //FontFamily = new FontFamily("Lucida Console"),
                    Text= Convert.ToString($"Pressed {args.VirtualKey} at "+ DateTime.Now.ToString("h:mm:ss fffffff") )
                });
            }
        }
    }
}
