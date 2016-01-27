using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ImageHandlers;


namespace DesktopPaintCOOL
{
    public partial class MainWindow
    {
        #region Fields
        private readonly Brush _background = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
        #endregion


        #region  Constructors & Destructor
        public MainWindow()
        {
            InitializeComponent();
            var curPath = Path.Combine(Environment.CurrentDirectory, "crayon.cur");
            var cursor = new Cursor(curPath);
            blkTop.Background = _background;
            icvMain.Background = _background;
            icvMain.Cursor = cursor;
        }
        #endregion


        #region Override
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftCtrl:
                case Key.RightCtrl:
                    icvMain.Background = null;
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
                    icvMain.Background = _background;
                    break;
            }
            base.OnKeyUp(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            var mouse = e.GetPosition(this);
            if (mouse.Y < 10)
            {
                tbrMain.Visibility = Visibility.Visible;
            }
            OnMouseMove(e);
        }
        #endregion


        #region Event Handlers
        private void CmdClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CmdOpen_OnClick(object sender, RoutedEventArgs e)
        {
            ImageFileHander.LoadImage(icvMain);
        }

        private void CmdSave_OnClick(object sender, RoutedEventArgs e)
        {
            ImageFileHander.SaveToImage(icvMain);
        }

        private void TbrMain_OnMouseLeave(object sender, MouseEventArgs e)
        {
            tbrMain.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}


//TODO: buttons Close, Minimize, Maximize, Save, Open, Save As
//TODO: Drawing patterns: circle, rectangle, triangle, stars, line, polygon
//TODO: eraser
//TODO: save strokes