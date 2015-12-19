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

        private double topSize;

        private double leftSize;

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

        public double TopSize
        {
            get
            {
                return this.topSize;
            }
            set
            {
                this.topSize = value;
                this.OnPropertyChanged("TopSize");
            }
        }

        public double LeftSize
        {
            get
            {
                return this.leftSize;
            }
            set
            {
                this.leftSize = value;
                this.OnPropertyChanged("LeftSize");
            }
        }
    }
}
