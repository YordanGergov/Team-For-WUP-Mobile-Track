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
        private double GameOverCount = 1800;

        public PlayPage()
        {
            this.InitializeComponent();
            this.DataContext = new FieldViewModel();
            this.mediaPlayer.Play();
            GenerateContacts();

            //this.DataContext = new FieldViewModel(200, 200);
            //var viewModel = this.DataContext as FieldViewModel/*(this.GridContainer.RowDefinitions[1].ActualHeight 200,200 this.GridContainer.ActualWidth)*/;

            //// enemy
            //var timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(1000 * Constants.BadRequestFrequency);
            //var objectsCount = viewModel.CountObjectsInHeight * 2 + viewModel.CountObjectsInWidth * 2;
            //var randomCoordinate = Generator.GetRandomNumber(0, objectsCount);

            /// 
            //timer.Tick += (snd, arg) =>
            //{
            //    viewModel.AddEnemy(viewModel.FieldCoordinates[randomCoordinate][0], viewModel.FieldCoordinates[randomCoordinate][1], "imgstring");
            //};
            //timer.Start();

            // friendly
           


            this.accelerometer = Accelerometer.GetDefault();
            this.accelerometer.ReportInterval = 50;
            this.accelerometer.ReadingChanged += new TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs>(ReadingChanged);

            var objectsCount = this.ViewModel.CountObjectsInHeight * 2 + this.ViewModel.CountObjectsInWidth * 2;
            var randomCoordinate = Generator.GetRandomNumber(0, objectsCount);
            var spawnTimer = new DispatcherTimer();
            spawnTimer.Interval = TimeSpan.FromMilliseconds(1000 * Constants.SecurityUpgradesFrequency);
            objectsCount = this.ViewModel.CountObjectsInHeight * 2 + this.ViewModel.CountObjectsInWidth * 2;
            randomCoordinate = Generator.GetRandomNumber(0, objectsCount);

            spawnTimer.Tick += (snd, arg) =>
            {
                var x = Generator.GetRandomNumber(40, (int)this.HappyHttpCanvas.ActualWidth - 40);
                var y = Generator.GetRandomNumber(40, (int)this.HappyHttpCanvas.ActualHeight - 40);
                this.ViewModel.AddFriendlyObjects(x, y, "imgstring");
            };
            spawnTimer.Start();

            var physicsTimer = new DispatcherTimer();
            physicsTimer.Interval = TimeSpan.FromMilliseconds(100);
            physicsTimer.Tick += (snd, arg) =>
            {
                var collideObjs = new List<GameObjectViewModel>();

                foreach (var item in this.ViewModel.FriendlyObjects)
                {
                    var something = this.ViewModel.Player.IsOver(item);
                    if (something)
                    {
                        collideObjs.Add(item);
                    }
                }

                var count = collideObjs.Count;

                this.ViewModel.Score += 10 * count / this.ViewModel.Player.Scale;
                collideObjs.ForEach(this.ViewModel.Remove);

                this.GameOverCount--;

                if(this.GameOverCount <= 0)
                {
                    physicsTimer.Stop();
                    this.Frame.Navigate(typeof(FinalPage), this.ViewModel.Score);
                }
            };
            physicsTimer.Start();

        }

        public FieldViewModel ViewModel
        {
            get
            {
                return this.DataContext as FieldViewModel;
            }
        }


        async private void ReadingChanged(object Accelerometer, AccelerometerReadingChangedEventArgs e)
        {

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AccelerometerReading reading = e.Reading;
                var top = this.ViewModel.Player.Top;
                var left = this.ViewModel.Player.Left;

                var x = reading.AccelerationX;
                var y = reading.AccelerationY;
                var z = reading.AccelerationZ;

                var pitchAngle = Math.Atan2(-x, Math.Sqrt(y * y + z * z)) * (180 / Math.PI); // formula calculating the angle on x
                var rollAngle = Math.Atan2(y, z) * (180 / Math.PI); // formula calculating the angle on y

                var speedY = 0.02;
                var speedX = 0.05;
                var newTop = this.ViewModel.Player.Top;
                var newLeft = this.ViewModel.Player.Left;

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

                // the following is to stop our object not to go outside of the canvas
                if (newTop < this.HappyHttpCanvas.ActualHeight - 25 && newTop > 0)
                {
                    this.ViewModel.Player.Top = newTop;
                }

                if (newLeft < this.HappyHttpCanvas.ActualWidth - 25 && newLeft > 0)
                {
                    this.ViewModel.Player.Left = newLeft;
                }
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

                    var newContact = new Helpers.Contact(Generator.GenerateRandomName(3, 10), Generator.GenerateRandomTelephoneNumber().ToString(), string.Format("iamhappy{0}@happy.com", i));

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

        private async void OnButtonClickEnd(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FinalPage));
        }

        private void Canvas_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {           
            var change = 0.1 * (this.ViewModel.Player.Size * e.Delta.Scale);

            if (change < 3 && change > -3 )
            {
                this.ViewModel.Player.Scale *= e.Delta.Scale;
                this.ViewModel.Player.Size *=change;
            }
            change = 0;
        }

        private void Canvas_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}