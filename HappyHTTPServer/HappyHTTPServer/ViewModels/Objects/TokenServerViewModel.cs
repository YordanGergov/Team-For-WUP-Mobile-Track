namespace HappyHTTPServer.ViewModels
{
    using Objects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TokenServerViewModel: GameObjectViewModel
    {
        private double size;
        private double scale;

        public TokenServerViewModel( double left, double top, double size)
            :base(left,top,size)
        {
            this.Scale = 1;
        }

        public double Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
                this.OnPropertyChanged("Scale");
            }
        }

        public override bool IsOver(GameObjectViewModel other)
        {
            return base.IsOver(other);
        }
    }
}
