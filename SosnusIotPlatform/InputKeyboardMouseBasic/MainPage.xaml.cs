using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Core;

namespace InputKeyboardMouseBasic
{
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
