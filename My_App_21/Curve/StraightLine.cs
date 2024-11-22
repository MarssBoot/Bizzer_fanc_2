using My_App_21.Drawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace My_App_21.Curve
{
    public class StraightLine : Curve
    {
        private Point startPoint;
        private Point endPoint;

        public StraightLine(Point startPoint, Point endPoint, ICurveDrawer drawer)
            : base(drawer)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public override void Draw()
        {
            drawer.DrawLine(startPoint, endPoint);
        }
    }
}
