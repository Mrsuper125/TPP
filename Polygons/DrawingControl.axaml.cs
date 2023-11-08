using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;
using System;

namespace Polygons
{
    public partial class DrawingControl : UserControl
    {
        private Shape _vertex;      //TODO: получше закомментить код
        private double _previousX;
        private double _previousY;

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
                _previousX = x;
                _previousY = y;
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
                _vertex.X += x-_previousX;
                _vertex.Y += y-_previousY;
                _previousX = x;
                _previousY = y;
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

        public override void Render(DrawingContext drawingContext)
        {
            _vertex.Draw(drawingContext);
        }
    }
}