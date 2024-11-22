using My_App_21.Drawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace My_App_21.Curve
{
    public class BezierCurve : Curve
    {
        private Point startPoint;
        private Point controlPoint1;
        private Point controlPoint2;
        private Point endPoint;

        public BezierCurve(Point startPoint, Point controlPoint1, Point controlPoint2, Point endPoint, ICurveDrawer drawer)
            : base(drawer)
        {
            this.startPoint = startPoint;
            this.controlPoint1 = controlPoint1;
            this.controlPoint2 = controlPoint2;
            this.endPoint = endPoint;
        }

        public override void Draw()
        {
            drawer.DrawBezierCurve(startPoint, controlPoint1, controlPoint2, endPoint);
        }
    }
}
