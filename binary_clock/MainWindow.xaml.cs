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
using System.Timers;

namespace binary_clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer;
        Brush inactiveSegmentColor = new SolidColorBrush(Color.FromRgb(224, 224, 224));
        Brush activeSegmentColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        public MainWindow()
        {
            InitializeComponent();

            // timer updating UI
            timer = new Timer(1);
            timer.Elapsed += UpdateUI;
            timer.Start();
        }

        // update UI method
        private void UpdateUI(object s, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // data
                DateTime time = DateTime.Now;
                int[] digits = new int[6];
                List<List<Rectangle>> segments = new List<List<Rectangle>>();

                segments.Add(new List<Rectangle>() { h03, h02 });
                segments.Add(new List<Rectangle>() { h13, h12, h11, h10 });
                segments.Add(new List<Rectangle>() { m03, m02, m01 });
                segments.Add(new List<Rectangle>() { m13, m12, m11, m10 });
                segments.Add(new List<Rectangle>() { s03, s02, s01 });
                segments.Add(new List<Rectangle>() { s13, s12, s11, s10 });

                digits[0] = time.Hour / 10;
                digits[1] = time.Hour % 10;
                digits[2] = time.Minute / 10;
                digits[3] = time.Minute % 10;
                digits[4] = time.Second / 10;
                digits[5] = time.Second % 10;

                // reset UI
                foreach (List<Rectangle> lr in segments)
                {
                    foreach (Rectangle r in lr)
                    {
                        r.Fill = inactiveSegmentColor;
                    }
                }

                // update UI
                for (int i = 0; i < 6; i++)
                {
                    int value = digits[i];
                    int index = 0;

                    while (value > 0)
                    {
                        if (value % 2 == 1)
                        {
                            segments[i][index].Fill = activeSegmentColor;
                        }

                        value /= 2;
                        index++;
                    }
                }
            });
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
            timer.Stop();
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
