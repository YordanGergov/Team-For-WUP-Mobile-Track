namespace HappyHTTPServer.ViewModels.Objects
{
    public class BadRequestsViewModel : BaseViewModel
    {
        public const int Value = 2;
        public const double Frequency = 0.7;

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
