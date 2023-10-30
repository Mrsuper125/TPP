using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;
using System;

namespace Polygons
{
    public partial class DrawingControl : UserControl
    {
        private Vertex _vertex;
        // координаты нашей пока единственной вершины.
        public DrawingControl() : base()
        {
            Console.WriteLine("Instantiated");
            _vertex = new Vertex(0, 0);
        }

        public void PointerPressed(double x, double y)
        {
            _vertex.X = x;
            _vertex.Y = y;
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
            Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Globals.FillColor);
            
            drawingContext.DrawEllipse(brush, pen, new Point(_vertex.X, _vertex.Y), Globals.VertexRadius, Globals.VertexRadius);
        }
    }
}