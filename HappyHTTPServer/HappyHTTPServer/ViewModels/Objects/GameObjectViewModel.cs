
namespace HappyHTTPServer.ViewModels.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using HappyHTTPServer.Common;
    using Windows.Foundation;
    public abstract class GameObjectViewModel : BaseViewModel
    {
        private double top;
        private double left;
        private double size;

        //private double width;
        //private bool friendly;

        //public GameObjectViewModel(double top, double left, string imageSource, bool friendly)
        //    : this(top, left, Constants.DefaultGameObjectRadius, imageSource, friendly)
        //{
        //}

        public GameObjectViewModel(double left, double top, double size)
        {
            this.Top = top;
            this.Left = left;
            this.Size = size;
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

        public double Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                this.OnPropertyChanged("Size");
            }
        }

        public virtual bool IsOver(GameObjectViewModel other)
        {
            var min = this.Left;
            var max = this.Left + this.Size;

            var left1 = other.Left;
            var left2 = other.Left + other.Size;

            bool isOverHorizontally = (min <= left1 && left1 <= max) ||
                (min <= left2 && left2 <= max);

            if (!isOverHorizontally)
            {
                return false;
            }

            min = this.Top;
            max = this.Top + this.Size;
            var top1 = other.Top;
            var top2 = other.Top + other.Size;
            bool isOverVertically = (min <= top1 && top1 <= max) ||
                (min <= top2 && top2 <= max);

            return isOverVertically;
        }
    }
}
