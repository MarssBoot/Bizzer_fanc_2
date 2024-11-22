using My_App_21.Drawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_App_21.Curve
{
    public abstract class Curve
    {
        protected ICurveDrawer drawer;

        protected Curve(ICurveDrawer drawer)
        {
            this.drawer = drawer;
        }

        public abstract void Draw();

        public ICurveDrawer Drawer
        {
            get { return drawer; }
            set { drawer = value; }
        }
    }
}
