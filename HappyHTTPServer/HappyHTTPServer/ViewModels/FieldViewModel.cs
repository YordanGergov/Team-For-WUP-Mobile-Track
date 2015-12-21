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

        private ObservableCollection<GameObjectViewModel> badRequests;
        private ObservableCollection<GameObjectViewModel> securityUpgrades;
        private ObservableCollection<GameObjectViewModel> friendHttpRequests;

        public FieldViewModel()
        {
            this.CountObjectsInHeight = (int)this.Height / 25 - 1;
            this.CountObjectsInWidth = (int)this.Width / 25 - 1;
        }

        public IEnumerable<GameObjectViewModel> BadRequests
        {
            get
            {
                return this.badRequests;
            }
            set
            {
                if (this.badRequests == null)
                {
                    this.badRequests = new ObservableCollection<GameObjectViewModel>();
                }
                this.badRequests.Clear();
                this.badRequests.AddRange(value);
            }
        }

        public IEnumerable<GameObjectViewModel> SecurityUpgrades
        {
            get
            {
                return this.securityUpgrades;
            }
            set
            {
                if (this.securityUpgrades == null)
                {
                    this.securityUpgrades = new ObservableCollection<GameObjectViewModel>();
                }
                this.securityUpgrades.Clear();
                this.securityUpgrades.AddRange(value);
            }
        }

        public ObservableCollection<GameObjectViewModel> FriendHttpRequests
        {
            get
            {
                return this.friendHttpRequests;
            }
            set
            {
                if (this.friendHttpRequests == null)
                {
                    this.friendHttpRequests = new ObservableCollection<GameObjectViewModel>();
                }
                this.friendHttpRequests.Clear();
                this.friendHttpRequests.AddRange(value);
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
                this.fieldCoordinates = GenerateCoordinatesField(this.Width, this.Height);
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

            //// left 
            for (int i = count; i < countObjectsInHeight + count; i++)
            {
                output[i] = new double[] { (countTop + i * Constants.DefaultGameObjectRadius), 0 };
                count = count + 1;
            }

            //// bottom 
            for (int i = count; i < countObjectsInWidth + count; i++)
            {
                output[i] = new double[] { (fieldHeight - Constants.DefaultGameObjectRadius), (countLeft + i * Constants.DefaultGameObjectRadius) };
                count = count + 1;
            }

            //// right 
            for (int i = count; i < countObjectsInHeight + count; i++)
            {
                output[i] = new double[] { (countTop + i * Constants.DefaultGameObjectRadius), (fieldHeight - Constants.DefaultGameObjectRadius) };
                count = count + 1;
            }
            return output;
        }

        public void AddBadRequest(double top, double left, string img)
        {
            var badRequest = new GameObjectViewModel(top, left, img);
            this.badRequests.Add(badRequest);
        }

        public void AddSecurityUpgrade(double top, double left, string img)
        {
            var securityUpgrade = new GameObjectViewModel(top, left, img);
            this.securityUpgrades.Add(securityUpgrade);
        }

        public void AddFriendHttpRequest(double top, double left, string img)
        {
            var friendHttpRequest = new GameObjectViewModel(top, left, img);
            this.friendHttpRequests.Add(friendHttpRequest);
        }

        
    }
}
