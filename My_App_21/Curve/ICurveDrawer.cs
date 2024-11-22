using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace My_App_21.Drawer
{
    public interface ICurveDrawer
    {
        void DrawBezierCurve(Point startPoint, Point controlPoint1, Point controlPoint2, Point endPoint);
        void DrawLine(Point startPoint, Point endPoint);
    }
}
