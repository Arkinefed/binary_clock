using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace binary_clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region moving mouse

        bool isWindowMoving = false;
        Point initialPosition;
        Cursor cursor;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == Mouse.LeftButton)
            {
                isWindowMoving = true;
                initialPosition = e.GetPosition(this);
                cursor = Cursor;
                Cursor = Cursors.Hand;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isWindowMoving)
            {
                isWindowMoving = false;
                Cursor = cursor;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isWindowMoving)
            {
                Vector position = e.GetPosition(this) - initialPosition;
                Left += position.X;
                Top += position.Y;
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isWindowMoving)
            {
                isWindowMoving = false;
            }
        }

        #endregion

        // close window
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // style when mouse is over exit button
        private void buttonExit_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Foreground = Brushes.Black;
        }

        // style when mouse is not over exit button
        private void buttonExit_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Foreground = Brushes.White;
        }
    }
}
