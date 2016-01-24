using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace DesktopPaintCOOL
{
    public partial class MainWindow
    {
        #region Fields
        private readonly Brush _background = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
        #endregion


        #region  Constructors & Destructors
        public MainWindow()
        {
            InitializeComponent();
            var curPath = Path.Combine(Environment.CurrentDirectory, "crayon.cur");
            var cursor = new Cursor(curPath);
            inkCanvasMain.Background = _background;
            inkCanvasMain.Cursor = cursor;
        }
        #endregion


        #region Override
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftCtrl:
                case Key.RightCtrl:
                    inkCanvasMain.Background = null;
                    break;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftCtrl:
                case Key.RightCtrl:
                    inkCanvasMain.Background = _background;
                    break;
            }
            base.OnKeyUp(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            var mouse = e.GetPosition(this);
            if (mouse.Y < 10)
            {
                rectangle.Visibility = Visibility.Visible;
            }
            base.OnMouseMove(e);
        }
        #endregion


        private void Rectangle_OnMouseLeave(object sender, MouseEventArgs e)
        {
            rectangle.Visibility = Visibility.Collapsed;
        }
    }
}