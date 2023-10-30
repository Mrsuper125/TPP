using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;

namespace Polygons
{
    public partial class DrawingControl : UserControl
    {
        private double vertexX, vertexY;
        // координаты нашей пока единственной вершины.
        // вместо этого лучше просто завести вершину, т.е. 
        // объект типа Circle, Square и Triangle

        public void PointerPressed(double x, double y)
        {
            vertexX = x;
            vertexY = y;
            // по сути, этот метод теперь заменяет нам 
            // обработку события PointerPressed
            // в нашем DrawingControl (объекте для рисования).
            // в качестве параметров сюда приходят x и y – 
            // координаты курсора (точки клика)

            // здесь будут те манипуляции с фигурой 
            // (точнее с координатами точек)
            // которые вам будут нужны – например, 
            // проверка IsInside и печать "да/нет"

            InvalidateVisual();
            // после того, как в фигуре что-то поменялось –
            // требуем перерисовать весь контрол. 
            // Этот метод просто вызывает Render, описанный ниже
        }

        public override void Render(DrawingContext drawingContext)
        {
            // объекты "ручка" и "кисть" нужны нам для рисования
            Pen pen = new Pen(Brushes.Green, 1, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Colors.Black);

            // рисуем фигуру по координатам vertexX, vertexY –
            // у меня просто рисуется окружность, 
            // у вас вместо этого в будущем будет вызываться 
            // метод Draw соответствующей фигуры,
            // а drawingcontext будет передаваться ему 
            // в качестве параметра –
            // без него мы просто не можем рисовать
            drawingContext.DrawEllipse(brush, pen, new Point(vertexX, vertexY), 10, 10);
        }
    }
}