namespace HappyHTTPServer.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Extensions;
    using Objects;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using HappyHTTPServer.Common;

    public class FieldViewModel : BaseViewModel
    {
        private double width;

        private double height;

        private int countObjectsInWidth;

        private int countObjectsInHeight;

        private double[][] fieldCoordinates;

        private ObservableCollection<GameObjectViewModel> enemies;
        private ObservableCollection<GameObjectViewModel> friendlyObjects;

        public FieldViewModel(double height, double width)
        {
            this.Height = height;
            this.Width = width;
            this.CountObjectsInHeight = (int)this.Height / 25 - 1;
            this.CountObjectsInWidth = (int)this.Width / 25 - 1;
            this.fieldCoordinates = GenerateCoordinatesField(this.Width, this.Height);
        }

        public IEnumerable<GameObjectViewModel> Enemies
        {
            get
            {
                if(this.enemies == null)
                {
                    this.enemies = new ObservableCollection<GameObjectViewModel>();
                }
                return this.enemies;
            }
            set
            {
                if (this.enemies == null)
                {
                    this.enemies = new ObservableCollection<GameObjectViewModel>();
                }
                this.enemies.Clear();
                this.enemies.ForEach(this.enemies.Add);
            }
        }

        public IEnumerable<GameObjectViewModel> FriendlyObjects
        {
            get
            {
                if (this.friendlyObjects == null)
                {
                    this.friendlyObjects = new ObservableCollection<GameObjectViewModel>();
                }
                return this.friendlyObjects;
            }
            set
            {
                if (this.friendlyObjects == null)
                {
                    this.friendlyObjects = new ObservableCollection<GameObjectViewModel>();
                }
                this.friendlyObjects.Clear();
                this.enemies.ForEach(this.enemies.Add);
            }
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

        public double[][] FieldCoordinates
        {
            get
            {
                return this.fieldCoordinates;
            }
            set
            {
                this.fieldCoordinates = value;
                this.OnPropertyChanged("FieldCoordinates");
            }
        }

        private double[][] GenerateCoordinatesField(double fieldWidth, double fieldHeight)
        {
            var countObjectsInWidth = fieldWidth / Constants.DefaultGameObjectRadius - 1;
            var countObjectsInHeight = fieldHeight / Constants.DefaultGameObjectRadius - 1;
            var objectsCount = (int)(countObjectsInWidth * 2 + countObjectsInHeight * 2);
            double[][] output = new double[objectsCount][];
            var countTop = 10;
            var countLeft = 10;
            var count = 0;
            // upper 
            for (int i = 0; i < countObjectsInWidth; i++)
            {
                output[i] = new double[] { 0, (countLeft + i * Constants.DefaultGameObjectRadius) };
                count = count + 1;
            }

            var count1 = count;
            //// left 
            for (int i = count; i < countObjectsInHeight + count; i++)
            {
                output[i] = new double[] { (countTop + i * Constants.DefaultGameObjectRadius), 0 };
                count1 = count1 + 1;
            }

            var count2 = count1;
            //// bottom 
            for (int i = count1; i < countObjectsInWidth + count1; i++)
            {
                output[i] = new double[] { (fieldHeight - Constants.DefaultGameObjectRadius), (countLeft + i * Constants.DefaultGameObjectRadius) };
                count2 = count2 + 1;
            }

            var count3 = count2;
            //// right 
            for (int i = count2; i < countObjectsInHeight + count2; i++)
            {
                output[i] = new double[] { (countTop + i * Constants.DefaultGameObjectRadius), (fieldHeight - Constants.DefaultGameObjectRadius) };
                count3 = count3 + 1;
            }
            return output;
        }

        public void AddEnemy(double top, double left, string img)
        {
            var enemy = new GameObjectViewModel(top, left, img, false);
            this.enemies.Add(enemy);
        }

        public void AddFriendlyObjectse(double top, double left, string img)
        {
            var friendlyObject = new GameObjectViewModel(top, left, img, true);
            this.friendlyObjects.Add(friendlyObject);
        }       
    }
}
