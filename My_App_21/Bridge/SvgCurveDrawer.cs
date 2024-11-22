using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace My_App_21.Drawer
{
    public class SvgCurveDrawer : ICurveDrawer
    {
        private StreamWriter writer;
        private bool headerWritten = false;
        private double width;
        private double height;

        public SvgCurveDrawer(StreamWriter writer, double width, double height)
        {
            this.writer = writer;
            this.width = width;
            this.height = height;
        }

        public void DrawBezierCurve(Point startPoint, Point controlPoint1, Point controlPoint2, Point endPoint)
        {
            if (!headerWritten)
            {
                WriteHeader();
                headerWritten = true;
            }

            // Форматируем данные кривой для SVG
            string pathData = string.Format(CultureInfo.InvariantCulture,
                "M {0},{1} C {2},{3} {4},{5} {6},{7}",
                startPoint.X, startPoint.Y,
                controlPoint1.X, controlPoint1.Y,
                controlPoint2.X, controlPoint2.Y,
                endPoint.X, endPoint.Y);

            // Записываем кривую в файл
            writer.WriteLine($"<path d=\"{pathData}\" stroke=\"blue\" stroke-width=\"2\" fill=\"none\" />");
        }
        public void DrawLine(Point startPoint, Point endPoint)
        {
            if (!headerWritten)
            {
                WriteHeader();
                headerWritten = true;
            }

            string lineData = string.Format(CultureInfo.InvariantCulture,
                "<line x1=\"{0}\" y1=\"{1}\" x2=\"{2}\" y2=\"{3}\" stroke=\"red\" stroke-width=\"2\" />",
                startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);

            writer.WriteLine(lineData);
        }

        private void WriteHeader()
        {
            // Записываем заголовок SVG
            writer.WriteLine($"<svg width=\"{width}\" height=\"{height}\" xmlns=\"http://www.w3.org/2000/svg\">");
        }

        public void WriteFooter()
        {
            // Записываем закрывающий тег SVG
            writer.WriteLine("</svg>");
        }
    }
}
