using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HappyHTTPServer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FinalPage : Page
    {
        List<Helpers.Contact> contactAsList = new List<Helpers.Contact>();
        public FinalPage()
        {
            this.InitializeComponent();
            //LoadContacts();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Score.Text = e.Parameter.ToString();
        }

        //private async void LoadContacts()
        //{
        //    var contactsStore = await ContactManager.RequestStoreAsync(Windows.ApplicationModel.Contacts.ContactStoreAccessType.AllContactsReadOnly);
        //    var allContacts = await contactsStore.FindContactsAsync();
        //    var contactAsList1 = allContacts.ToList();


        //    foreach (var item in contactAsList1)
        //    {
        //        var contactToAdd = new Helpers.Contact(item.Name, item.Phones.FirstOrDefault().Number, item.Emails.FirstOrDefault().Address);
        //        contactAsList.Add(contactToAdd);
        //    }

        //    var what = contactAsList.FirstOrDefault();
        //    this.FriendsWhoHelped.DataContext = contactAsList;
        //}
    }
}
