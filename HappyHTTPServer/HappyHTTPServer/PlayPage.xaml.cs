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
    using HappyHTTPServer.Common;
    using Helpers;
    using Windows.ApplicationModel.Contacts;
    using Windows.Phone.PersonalInformation;
    using ViewModels.Objects;
    public sealed partial class PlayPage : Page
    {
        private Accelerometer accelerometer;
        private TokenServerViewModel happyServerVM;

        public PlayPage()
        {
            this.InitializeComponent();
            this.mediaPlayer.Play();

            this.DataContext = new FieldViewModel(200, 200);
            var viewModel = this.DataContext as FieldViewModel/*(this.GridContainer.RowDefinitions[1].ActualHeight 200,200 this.GridContainer.ActualWidth)*/;

            // enemy
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000 * Constants.BadRequestFrequency);
            var objectsCount = viewModel.CountObjectsInHeight * 2 + viewModel.CountObjectsInWidth * 2;
            var randomCoordinate = Generator.GetRandomNumber(0, objectsCount);

            timer.Tick += (snd, arg) =>
            {
                viewModel.AddEnemy(viewModel.FieldCoordinates[randomCoordinate][0], viewModel.FieldCoordinates[randomCoordinate][1], "imgstring");
            };
            timer.Start();

            // friendly
            var timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromMilliseconds(1000 * Constants.SecurityUpgradesFrequency);
            objectsCount = viewModel.CountObjectsInHeight * 2 + viewModel.CountObjectsInWidth * 2;
            randomCoordinate = Generator.GetRandomNumber(0, objectsCount);

            timer2.Tick += (snd, arg) =>
            {
                viewModel.AddFriendlyObjects(viewModel.FieldCoordinates[randomCoordinate][0], viewModel.FieldCoordinates[randomCoordinate][1], "imgstring");
            };
            timer2.Start();

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
                if (this.happyServerVM.TopSize > this.HappyServer.Height /*&& this.happyServerVM.TopSize < this.Field.ActualHeight*/)
                {
                    Canvas.SetTop(HappyServer, newTop);
                }

                if (this.happyServerVM.LeftSize > this.HappyServer.Width /*&& this.happyServerVM.LeftSize < this.Field.ActualWidth*/)
                {
                    Canvas.SetLeft(HappyServer, newLeft);
                }

                this.DataContext = this.happyServerVM;

            });
        }

        public async void GenerateContacts()
        {
            var contactsStore = await ContactManager.RequestStoreAsync(Windows.ApplicationModel.Contacts.ContactStoreAccessType.AllContactsReadOnly);
            var allContacts = await contactsStore.FindContactsAsync();
            var contactAsList = allContacts.ToList();

            if (contactAsList.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    var contactStore = await Windows.Phone.PersonalInformation.ContactStore.CreateOrOpenAsync(
                                                                                            ContactStoreSystemAccessMode.ReadWrite,
                                                                                            ContactStoreApplicationAccessMode.ReadOnly);
                    var contact = new StoredContact(contactStore);
                    var contactDetails = await contact.GetPropertiesAsync();

                    var newContact = new Helpers.Contact(Generator.GenerateRandomName(3, 10), Generator.GetRandomNumber(8, 8).ToString(), string.Format("iamhappy{0}@happy.com", i));

                    if (!string.IsNullOrEmpty(newContact.Name))
                    {
                        contactDetails.Add(KnownContactProperties.GivenName, newContact.Name);
                    }
                    if (!string.IsNullOrEmpty(newContact.PhoneNumber))
                    {
                        contactDetails.Add(KnownContactProperties.WorkTelephone, newContact.PhoneNumber);
                    }
                    if (!string.IsNullOrEmpty(newContact.PhoneNumber))
                    {
                        contactDetails.Add(KnownContactProperties.Email, newContact.Email);
                    }

                    await contact.SaveAsync();
                }

                contactsStore = await ContactManager.RequestStoreAsync(Windows.ApplicationModel.Contacts.ContactStoreAccessType.AllContactsReadOnly);
                allContacts = await contactsStore.FindContactsAsync();
                contactAsList = allContacts.ToList();
            }
        }

        private void HappyServer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
                this.musicClick.Play();          
        }

        private void HappyServer_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

            //validation and casting
            var element = sender as UIElement;

            if (element == null)
            {
                return;
            }

            // create transfrom
            element.RenderTransform = new ScaleTransform();
            element.RenderTransformOrigin = new Point(0.5, 0.5);
            var transform = element.RenderTransform as ScaleTransform;

            var sizeChange = e.Delta.Scale;

            transform.ScaleX += sizeChange;
            transform.ScaleY += sizeChange;

            var delta = e.Delta;

            var x = delta.Translation.X;
            var y = delta.Translation.Y;

            var viewModeld = this.DataContext as GameObjectViewModel;
            if (viewModeld.Left + x < 0)
            { return; }
            viewModeld.Top += y;
            viewModeld.Left += x;

        }
    }
}