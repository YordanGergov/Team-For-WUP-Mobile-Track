namespace HappyHTTPServer.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TokenServerViewModel: BaseViewModel
    {
        private double scaleWidth;

        private double scaleHeight;

        private double top;

        private double left;

        public double ScaleWidth
        {
            get
            {
                return this.scaleWidth;
            }
            set
            {
                this.scaleWidth = value;
                this.OnPropertyChanged("ScaleWidth");
            }
        }

        public double ScaleHeight
        {
            get
            {
                return this.scaleHeight;
            }
            set
            {
                this.scaleHeight = value;
                this.OnPropertyChanged("ScaleHeight");
            }
        }

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
