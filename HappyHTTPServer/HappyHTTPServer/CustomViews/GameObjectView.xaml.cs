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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HappyHTTPServer.CustomViews
{
    public sealed partial class GameObjectView : UserControl
    {
        public GameObjectView()
        {
            this.InitializeComponent();
        }

        public double Diameter
        {
            get { return (double)GetValue(DiaProperty); }
            set { SetValue(DiaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiaProperty =
            DependencyProperty.Register("Dia", typeof(double), typeof(GameObjectView), new PropertyMetadata(25.0));


        public bool Friendly
        {
            get { return (bool)GetValue(FriendlyProperty); }
            set { SetValue(FriendlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Friendly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FriendlyProperty =
            DependencyProperty.Register("Friendly", typeof(bool), typeof(GameObjectView), new PropertyMetadata(true));




    }
}
