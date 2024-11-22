using My_App_21.Drawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_App_21.Curve
{
    public class CompositeCurve : Curve
    {
        private List<Curve> curves = new List<Curve>();

        public CompositeCurve(ICurveDrawer drawer)
            : base(drawer)
        {
        }

        public void Add(Curve curve)
        {
            curves.Add(curve);
        }

        public void Remove(Curve curve)
        {
            curves.Remove(curve);
        }

        public override void Draw()
        {
            foreach (var curve in curves)
            {
                curve.Draw();
            }
        }

        public new ICurveDrawer Drawer
        {
            get { return drawer; }
            set
            {
                drawer = value;
                foreach (var curve in curves)
                {
                    curve.Drawer = value;
                }
            }
        }
    }
}
