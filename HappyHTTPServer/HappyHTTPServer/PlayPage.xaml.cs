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

    public sealed partial class PlayPage : Page
    {
        private Accelerometer accelerometer;
        private TokenServerViewModel happyServerVM;

        public PlayPage()
        {
            this.InitializeComponent();
            this.mediaPlayer.Play();

            // BadRequests

            //var viewModel = this.DataContext as FieldViewModel;

            //var timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(10000 * Constants.BadRequestFrequency);
            //var objectsCount = viewModel.CountObjectsInHeight * 2 + viewModel.CountObjectsInWidth * 2;
            //var randomCoordinate = Generator.GetRandomNumber(0, objectsCount);

            //timer.Tick += (snd, arg) =>
            //{
            //    viewModel.AddBadRequest(viewModel.FieldCoordinates[randomCoordinate][0], viewModel.FieldCoordinates[randomCoordinate][1], "imgstring");
            //};

            //// securityUpgrade
            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(10000 * Constants.SecurityUpgradesFrequency);
            //objectsCount = viewModel.CountObjectsInHeight * 2 + viewModel.CountObjectsInWidth * 2;
            //randomCoordinate = Generator.GetRandomNumber(0, objectsCount);

            //timer.Tick += (snd, arg) =>
            //{
            //    viewModel.AddSecurityUpgrade(viewModel.FieldCoordinates[randomCoordinate][0], viewModel.FieldCoordinates[randomCoordinate][1], "imgstring");
            //};

            //// friendHttpRequests
            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(10000 * Constants.HttpFriendRequestsFrequency);
            //objectsCount = viewModel.CountObjectsInHeight * 2 + viewModel.CountObjectsInWidth * 2;
            //randomCoordinate = Generator.GetRandomNumber(0, objectsCount);

            //timer.Tick += (snd, arg) =>
            //{
            //    viewModel.AddFriendHttpRequest(viewModel.FieldCoordinates[randomCoordinate][0], viewModel.FieldCoordinates[randomCoordinate][1], "imgstring");
            //};

            ////трябва ми метода за колизия да гу мушна, но първо обектите
            ////сори за БГ-то - много ме мързи - утре тези коментари ще ги няма

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

                    var newContact = new Helpers.Contact(string.Format("whatever{0}", i), string.Format("whatever{0}", i), string.Format("whatever{0}@whatever.com", i));

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
    }
}