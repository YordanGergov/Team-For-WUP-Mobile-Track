﻿namespace HappyHTTPServer.ViewModels.Objects
{

    public class SecurityUpgradesViewModel : BaseViewModel
    {
        public const int Value = 3;
        public const double Frequency = 0.4;

        private double top;

        private double left;


        public double Top
        {
            get
            {
                return this.top;
            }
            set
            {
                this.top = value;
                this.OnPropertyChanged("Top");
            }
        }

        public double Left
        {
            get
            {
                return this.left;
            }
            set
            {
                this.left = value;
                this.OnPropertyChanged("Left");
            }
        }

    }
}