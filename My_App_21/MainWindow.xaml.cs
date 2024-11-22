using Microsoft.Win32;
using My_App_21.Curve;
using My_App_21.Drawer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace My_App_21
{
    public partial class MainWindow : Window
    {
        private CompositeCurve compositeCurve;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateCurvesButton_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем холст
            CurveCanvas.Children.Clear();

            // Создаем отрисовщик для WPF
            ICurveDrawer drawer = new WpfCurveDrawer(CurveCanvas);

            // Создаем композит кривых
            compositeCurve = new CompositeCurve(drawer);

            // Получаем выбранный тип кривой
            string selectedCurveType = ((ComboBoxItem)CurveTypeComboBox.SelectedItem).Content.ToString();

            // Генерируем случайные точки
            Random rand = new Random();

            CurveCanvas.UpdateLayout();
            double canvasWidth = CurveCanvas.ActualWidth;
            double canvasHeight = CurveCanvas.ActualHeight;

            if (canvasWidth == 0 || canvasHeight == 0)
            {
                canvasWidth = CurveCanvas.Width;
                canvasHeight = CurveCanvas.Height;
            }

            if (canvasWidth == 0 || canvasHeight == 0)
            {
                canvasWidth = this.Width;
                canvasHeight = this.Height - 100; // Учитываем высоту кнопок
            }

            if (selectedCurveType == "Кривая Безье")
            {
                // Создаем случайные точки для кривой Безье
                Point startPoint = new Point(rand.NextDouble() * canvasWidth, rand.NextDouble() * canvasHeight);
                Point controlPoint1 = new Point(rand.NextDouble() * canvasWidth, rand.NextDouble() * canvasHeight);
                Point controlPoint2 = new Point(rand.NextDouble() * canvasWidth, rand.NextDouble() * canvasHeight);
                Point endPoint = new Point(rand.NextDouble() * canvasWidth, rand.NextDouble() * canvasHeight);

                // Создаем кривую Безье
                BezierCurve bezierCurve = new BezierCurve(startPoint, controlPoint1, controlPoint2, endPoint, drawer);

                // Добавляем кривую в композит
                compositeCurve.Add(bezierCurve);
                
            }
            else if (selectedCurveType == "Прямая линия")
            {
                // Создаем случайные точки для прямой линии
                Point startPoint = new Point(rand.NextDouble() * canvasWidth, rand.NextDouble() * canvasHeight);
                Point endPoint = new Point(rand.NextDouble() * canvasWidth, rand.NextDouble() * canvasHeight);

                // Создаем прямую линию
                StraightLine straightLine = new StraightLine(startPoint, endPoint, drawer);

                // Добавляем линию в композит
                compositeCurve.Add(straightLine);
            }

            // Отрисовываем композит кривых
            compositeCurve.Draw();
        }

        private void SaveImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (compositeCurve == null)
            {
                MessageBox.Show("Сначала сгенерируйте кривые.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Получаем путь к директории, где находится исполняемый файл
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Поднимаемся на несколько уровней вверх, чтобы попасть в корневую директорию проекта
                string projectRootPath = Directory.GetParent(baseDirectory).Parent.Parent.FullName;

                // Указываем поддиректорию Image внутри проекта
                string imageDirectoryPath = System.IO.Path.Combine(projectRootPath, "Image");

                // Проверяем, существует ли директория Image, если нет - создаем
                if (!Directory.Exists(imageDirectoryPath))
                {
                    Directory.CreateDirectory(imageDirectoryPath);
                }

                // Добавляем временную метку к имени файла для уникальности
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Сохраняем SVG
                string svgFilePath = System.IO.Path.Combine(imageDirectoryPath, $"curve_{timestamp}.svg");
                using (StreamWriter writer = new StreamWriter(svgFilePath))
                {
                    double width = CurveCanvas.ActualWidth;
                    double height = CurveCanvas.ActualHeight;

                    if (width == 0 || height == 0)
                    {
                        width = CurveCanvas.Width;
                        height = CurveCanvas.Height;
                    }

                    if (width == 0 || height == 0)
                    {
                        width = this.Width;
                        height = this.Height - 100; // Учитываем высоту кнопок
                    }

                    // Создаем отрисовщик для SVG
                    SvgCurveDrawer svgDrawer = new SvgCurveDrawer(writer, width, height);

                    // Сохраняем оригинальный отрисовщик
                    ICurveDrawer originalDrawer = compositeCurve.Drawer;

                    // Заменяем отрисовщик на SVG
                    compositeCurve.Drawer = svgDrawer;

                    // Отрисовываем кривые в SVG
                    compositeCurve.Draw();

                    // Записываем закрывающий тег SVG
                    svgDrawer.WriteFooter();

                    // Восстанавливаем оригинальный отрисовщик
                    compositeCurve.Drawer = originalDrawer;
                }

                // Сохраняем PNG
                string pngFilePath = System.IO.Path.Combine(imageDirectoryPath, $"curve_{timestamp}.png");
                SaveCanvasAsPng(CurveCanvas, pngFilePath);

                MessageBox.Show($"Изображения успешно сохранены в директорию:\n{imageDirectoryPath}", "Сохранение изображения", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файлов:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Метод для сохранения Canvas в PNG
        private void SaveCanvasAsPng(Canvas canvas, string filePath)
        {
            Size size = new Size(canvas.ActualWidth, canvas.ActualHeight);
            if (size.Width == 0 || size.Height == 0)
            {
                size = new Size(canvas.Width, canvas.Height);
            }

            if (size.Width == 0 || size.Height == 0)
            {
                size = new Size(this.Width, this.Height - 100); // Учитываем высоту кнопок
            }

            // Измеряем и располагаем Canvas
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            // Создаем RenderTargetBitmap
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)size.Width, (int)size.Height,
                96d, 96d, PixelFormats.Pbgra32);

            // Отрисовываем Canvas в RenderTargetBitmap
            renderBitmap.Render(canvas);

            // Создаем PNG-энкодер
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // Сохраняем в файл
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                pngEncoder.Save(fileStream);
            }
        }
    }
}