namespace HappyHTTPServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
    
        private async void OnButtonClickPlay(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlayPage));
        }
       
        private async void OnButtonClickTutorial(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Tutorial));
        }

        private async void OnButtonClickHttpEncyclopedia(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HTTPEncyclopedia));
        }
    }
}
