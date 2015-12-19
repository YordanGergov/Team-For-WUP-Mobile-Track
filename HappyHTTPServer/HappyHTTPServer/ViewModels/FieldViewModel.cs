namespace HappyHTTPServer.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FieldViewModel : BaseViewModel
    {
        private double width;

        private double height;

        private int countObjectsInWidth;

        private int countObjectsInHeight;

        public FieldViewModel()
        {
            this.CountObjectsInHeight = (int)this.Height / 25 - 1;
            this.CountObjectsInWidth = (int)this.Width / 25 - 1;
        }

        public double Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                this.OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
                this.OnPropertyChanged("Height");
            }
        }

        public int CountObjectsInWidth
        {
            get
            {
                return this.countObjectsInWidth;
            }
            set
            {
                this.countObjectsInWidth = value;
                this.OnPropertyChanged("CountObjectsInWidth");
            }
        }

        public int CountObjectsInHeight
        {
            get
            {
                return this.countObjectsInHeight;
            }
            set
            {
                this.countObjectsInHeight = value;
                this.OnPropertyChanged("CountObjectsInHeight");
            }
        }
    }
}
