using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;
using System;

namespace Polygons
{
    public partial class DrawingControl : UserControl
    {
        private Shape _vertex;      //TODO: получше закомментить код

        private bool _holding;
        // координаты нашей пока единственной вершины.
        public DrawingControl() : base()
        {
            Console.WriteLine("Instantiated");
            switch (Globals.VertexShape)            //Проверяем тип вершины из глобалсов. Нашли совпадающий - записываем его, что-то не так - Exception по мордасам, ибо нефиг непотребство пихать.
            {
                case VertexShape.Circle:
                    _vertex = new CircleVertex(0, 0);       
                    break;
                case VertexShape.Square:
                    _vertex = new SquareVertex(0, 0);
                    break;
                case VertexShape.Triangle:
                    _vertex = new TriangleVertex(0, 0);
                    break;
                default:
                    throw new Exception("Wrong vertex shape");
            }
        }

        public void PointerPressed(double x, double y)
        {
            if (_vertex.IsInside(x, y))
            {
                Console.WriteLine("Inside");
                _holding = true;
            }
            else
            {
                _vertex.X = x;
                _vertex.Y = y;
                if (Globals.VertexShape == VertexShape.Triangle)
                {
                    TriangleVertex vertex = (TriangleVertex)_vertex;
                    vertex.InvalidateVertices();
                }
            }
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
         public void PointerMoved(double x, double y)
        {
            if (_holding)
            {
                Console.WriteLine("Drag");
                _vertex.X = x;
                _vertex.Y = y;
                if (Globals.VertexShape == VertexShape.Triangle)
                {
                    TriangleVertex vertex = (TriangleVertex)_vertex;        //TODO: починить телепортацию фигуры при перетягивании
                    vertex.InvalidateVertices();
                }
            }
            InvalidateVisual();
        }

        public void PointerReleased(double x, double y)
        {
            Console.WriteLine("Released");
            if (_holding)
            {
                Console.WriteLine("Set holding to false");
                _holding = false;
            }
            InvalidateVisual();
        }

        public void DrawCircle(DrawingContext drawingContext)               //Рисование круга в отдельной функции. Ничего необычного, просто круг по данным из Globals и _vertex
        {
            // объекты "ручка" и "кисть" нужны нам для рисования
            Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Globals.FillColor);
            drawingContext.DrawEllipse(brush, pen, new Point(_vertex.X, _vertex.Y), Globals.VertexRadius, Globals.VertexRadius);
        }

        public void DrawSquare(DrawingContext drawingContext)
        {
            SquareVertex vertex = (SquareVertex)_vertex;
            Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Globals.FillColor);
            drawingContext.DrawRectangle(brush, pen, new Rect(_vertex.X, _vertex.Y, vertex.halfWidht, vertex.halfWidht));
        }

        public void DrawTriangle(DrawingContext drawingContext)
        {
            TriangleVertex vertex = (TriangleVertex)_vertex;
            Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Globals.FillColor);
            drawingContext.DrawGeometry(brush, pen, new PolylineGeometry(new Point[4]{new Point(vertex.LeftVertexX, vertex.LeftVertexY), new Point(vertex.TopVertexX, vertex.TopVertexY), new Point(vertex.RightVertexX, vertex.RightVertexY), new Point(vertex.LeftVertexX, vertex.LeftVertexY)}, false));
        }

        public override void Render(DrawingContext drawingContext)
        {
            switch (Globals.VertexShape)                            //Берём из глобалсов тип фигуры и вызываем соответствующий метод рисования, который берёт данные из глобалсов и полей фигуры. Что-то не так с типом - Exception.
            {
                case VertexShape.Circle:
                    DrawCircle(drawingContext);
                    break;
                case VertexShape.Square:
                    DrawSquare(drawingContext);
                    break;
                case VertexShape.Triangle:
                    DrawTriangle(drawingContext);
                    break;
                default:
                    throw new Exception("Wrong vertex shape");
            }
        }
    }
}