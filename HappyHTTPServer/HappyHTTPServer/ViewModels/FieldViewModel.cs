namespace HappyHTTPServer.ViewModels
{
    using Extensions;
    using Objects;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FieldViewModel : BaseViewModel
    {
        private double width;

        private double height;

        private int countObjectsInWidth;

        private int countObjectsInHeight;

        private ObservableCollection<GameObjectViewModel> enemies;
        private ObservableCollection<GameObjectViewModel> gameObjects;
        private ObservableCollection<GameObjectViewModel> fireballs;

        public IEnumerable<GameObjectViewModel> Enemies
        {
            get
            {
                return this.enemies;
            }
            set
            {
                if (this.enemies == null)
                {
                    this.enemies = new ObservableCollection<GameObjectViewModel>();
                }
                this.enemies.Clear();
                this.enemies.AddRange(value);
            }
        }

        public IEnumerable<GameObjectViewModel> GameObjects
        {
            get
            {
                return this.gameObjects;
            }
            set
            {
                if (this.gameObjects == null)
                {
                    this.gameObjects = new ObservableCollection<GameObjectViewModel>();
                }
                this.gameObjects.Clear();
                this.gameObjects.AddRange(value);
            }
        }

        public ObservableCollection<GameObjectViewModel> Fireballs
        {
            get
            {
                return this.fireballs;
            }
            set
            {
                if (this.fireballs == null)
                {
                    this.fireballs = new ObservableCollection<GameObjectViewModel>();
                }
                this.fireballs.Clear();
                this.fireballs.AddRange(value);
            }
        }

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
