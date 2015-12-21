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
using AngleSharp;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HappyHTTPServer
{
    public sealed partial class HTTPEncyclopedia : Page
    {
        public HTTPEncyclopedia()
        {
            this.InitializeComponent();
        }


        private async void RunButtonClick(object sender, RoutedEventArgs e)
        {
            ProggressRing.IsActive = true;

            var config = Configuration.Default.WithDefaultLoader();

            var address = "https://en.wikipedia.org/wiki/List_of_HTTP_status_codes";

            var document = await BrowsingContext.New(config).OpenAsync(address);

            var cellSelector = "dl dt";
            
            var cells = document.QuerySelectorAll(cellSelector);

            var titles = cells.Select(m => m.TextContent);

            this.ResultTextBlock.Text = string.Join(Environment.NewLine, titles);

            ProggressRing.IsActive = false;
        }
    }
}
