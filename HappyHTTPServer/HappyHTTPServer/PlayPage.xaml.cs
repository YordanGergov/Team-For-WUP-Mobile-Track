namespace HappyHTTPServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using ViewModels;
    using Windows.Devices.Sensors;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;
    using Windows.UI.Xaml.Shapes;
    public sealed partial class PlayPage : Page
    {
        private Accelerometer accelerometer;
        private TokenServerViewModel happyServerVM;

        public PlayPage()
        {
            this.InitializeComponent();
            //GenerateFieldObjects();

            //var timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(5000); //искам да му казвам за всяко през колко време
            //var viewModel = this.DataContext as FieldViewModel;
            //var rng = new Random();
            //double x, y;
            //double r = 30;
            //трябват ми няколко от тези и то с определени координати и то да се редуват, че да не стане много на веднъж
            //timer.Tick += (snd, arg) =>
            //{
            //    x = 100 + rng.NextDouble() * 200;
            //    viewModel.AddShot(x, x, r);
            //};

            //трябва ми метода за колизия да гу мушна, но първо обектите
            //сори за БГ-то - много ме мързи - утре тези коментари ще ги няма

            this.happyServerVM = new TokenServerViewModel();
            this.DataContext = this.happyServerVM;
            this.accelerometer = Accelerometer.GetDefault();
            this.accelerometer.ReportInterval = 50;
            this.accelerometer.ReadingChanged += new TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs>(ReadingChanged);
        }

        async private void ReadingChanged(object Accelerometer, AccelerometerReadingChangedEventArgs e)
        {
           
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AccelerometerReading reading = e.Reading;
                var top = Canvas.GetTop(HappyServer);
                var left = Canvas.GetLeft(HappyServer);
                var x = reading.AccelerationX;
                var y = reading.AccelerationY;
                var z = reading.AccelerationZ;
                var pitchAngle = Math.Atan2(-x, Math.Sqrt(y * y + z * z)) * (180 / Math.PI); // formula calculating the angle on x
                var rollAngle = Math.Atan2(y, z) * (180 / Math.PI); // formula calculating the angle on y
                double newTop = top;
                double newLeft = left;
                var speedY = 0.02;
                var speedX = 0.05;

                if (rollAngle < -100)
                {
                    newTop += speedY * (rollAngle - 90);
                }
                else if (rollAngle > -80)
                {
                    newTop -= speedY * (rollAngle - 90);
                }

                if (pitchAngle > 20)
                {
                    newLeft += speedX * (pitchAngle - 90);
                }
                else if (pitchAngle < -20)
                {
                    newLeft -= speedX * (pitchAngle - 90);
                }

                this.happyServerVM.TopSize = newTop + this.HappyServer.Height;
                this.happyServerVM.LeftSize = newLeft + this.HappyServer.Width;

                //// the following is to stop our object not to go outside of the canvas
                if (this.happyServerVM.TopSize > this.HappyServer.Height && this.happyServerVM.TopSize < this.Field.ActualHeight)
                {
                    Canvas.SetTop(HappyServer, newTop);
                }

                if (this.happyServerVM.LeftSize > this.HappyServer.Width && this.happyServerVM.LeftSize < this.Field.ActualWidth)
                {
                    Canvas.SetLeft(HappyServer, newLeft);
                }

                this.DataContext = this.happyServerVM;

            });
        }

        //private void GenerateFieldObjects()
        //{
        //    Ellipse ellipse = new Ellipse();
        //    ellipse.Width = 25;
        //    ellipse.Height = 25;
        //    var countTop = 10;
        //    var countLeft = 10;

        //    //// upper ellipses
        //    for (int i = 0; i < field.CountObjectsInWidth; i++)
        //    {
        //        this.Field.Children.Add(ellipse);
        //        Canvas.SetTop(ellipse, 0);
        //        Canvas.SetLeft(ellipse, countLeft + i * 25);
        //        fieldEllipses.Add(ellipse);
        //    }

        //    //// left ellipses
        //    for (int i = 0; i < field.CountObjectsInWidth; i++)
        //    {
        //        this.Field.Children.Add(ellipse);
        //        Canvas.SetTop(ellipse, countTop + i * 25);
        //        Canvas.SetLeft(ellipse, 0);
        //        fieldEllipses.Add(ellipse);
        //    }

        //    //// bottom ellipses
        //    for (int i = 0; i < field.CountObjectsInWidth; i++)
        //    {
        //        this.Field.Children.Add(ellipse);
        //        Canvas.SetTop(ellipse, field.Height - 25);
        //        Canvas.SetLeft(ellipse, countLeft + i * 25);
        //        fieldEllipses.Add(ellipse);
        //    }

        //    //// right ellipses
        //    for (int i = 0; i < field.CountObjectsInWidth; i++)
        //    {
        //        this.Field.Children.Add(ellipse);
        //        Canvas.SetTop(ellipse, countTop + i * 25);
        //        Canvas.SetLeft(ellipse, field.Width - 25);
        //        fieldEllipses.Add(ellipse);
        //    }
        //}
    }
}