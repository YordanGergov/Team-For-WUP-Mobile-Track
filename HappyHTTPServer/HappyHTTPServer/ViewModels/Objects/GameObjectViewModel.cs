
namespace HappyHTTPServer.ViewModels.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Constants;

    public abstract class GameObjectViewModel : BaseViewModel
    {
        private double top;
        private double left;

        private double width;

        public GameObjectViewModel(double top, double left, string imageSource)
            : this(top, left, Constants.DefaultGameObjectWidth, imageSource)
        {
        }

        public GameObjectViewModel(double top, double left, double width, string imageSource)
        {
            this.Top = top;
            this.Left = left;
            this.ImageSource = imageSource;
            this.Width = width;
            this.IsAlive = true;
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

        public string ImageSource { get; set; }

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
                return this.width;
            }
            set
            {
                this.width = value;
                this.OnPropertyChanged("Height");
            }
        }

        public bool IsAlive { get; set; }

        public virtual bool IsOver(GameObjectViewModel other)
        {
            var min = this.Left;
            var max = this.Left + this.Width;

            var left1 = other.Left;
            var left2 = other.Left + other.Width;

            bool isOverHorizontally = (min <= left1 && left1 <= max) ||
                (min <= left2 && left2 <= max);

            if (!isOverHorizontally)
            {
                return false;
            }

            min = this.Top;
            max = this.Top + this.Width;
            var top1 = other.Top;
            var top2 = other.Top + other.Width;
            bool isOverVertically = (min <= top1 && top1 <= max) ||
                (min <= top2 && top2 <= max);

            return isOverVertically;
        }
    }
}
