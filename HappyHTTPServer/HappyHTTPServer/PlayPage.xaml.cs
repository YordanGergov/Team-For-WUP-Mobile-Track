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

    public sealed partial class PlayPage : Page
    {
        private Accelerometer accelerometer;
        private TokenServerViewModel happyServerVM;


        public PlayPage()
        {
            this.InitializeComponent();
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
                var top = Canvas.GetTop(Test);
                var left = Canvas.GetLeft(Test);                
                var x = reading.AccelerationX;
                var y = reading.AccelerationY;
                var z = reading.AccelerationZ;
                var pitchAngle = Math.Atan2(-x, Math.Sqrt(y * y + z * z)) * (180 / Math.PI);
                var rollAngle = Math.Atan2(y, z) * (180 / Math.PI);
                double newTop = top;
                double newLeft = left;
                var speed = 0.01;

                if (rollAngle < -100)
                {
                    newTop += speed * (rollAngle - 90);
                }
                else if (rollAngle > -80)
                {
                    newTop -= speed * (rollAngle - 90);
                }

                if (pitchAngle > 20)
                {
                    newLeft += speed * (pitchAngle - 90);
                }
                else if (pitchAngle < -20)
                {
                    newLeft -= speed * (pitchAngle - 90);
                }
                Canvas.SetLeft(Test, newLeft);
                Canvas.SetTop(Test, newTop);

                this.DataContext = this.happyServerVM;
                
            });
        }
    }
}
