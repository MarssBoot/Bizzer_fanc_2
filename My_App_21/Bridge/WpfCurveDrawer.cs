using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace My_App_21.Drawer
{
    public class WpfCurveDrawer : ICurveDrawer
    {
        private Canvas canvas;

        public WpfCurveDrawer(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void DrawBezierCurve(Point startPoint, Point controlPoint1, Point controlPoint2, Point endPoint)
        {
            // Создаем фигуру пути
            PathFigure pathFigure = new PathFigure { StartPoint = startPoint };

            // Создаем сегмент кривой Безье
            BezierSegment bezierSegment = new BezierSegment(controlPoint1, controlPoint2, endPoint, true);

            // Добавляем сегмент в фигуру
            pathFigure.Segments.Add(bezierSegment);

            // Создаем геометрию пути
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            // Создаем путь для отображения
            Path path = new Path
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 2,
                Data = pathGeometry
            };

            // Добавляем путь на холст
            canvas.Children.Add(path);
        }
        public void DrawLine(Point startPoint, Point endPoint)
        {
            Line line = new Line
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            canvas.Children.Add(line);
        }
    }
}
