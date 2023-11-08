using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;

namespace Polygons
{
    public partial class DrawingControl : UserControl
    {      //TODO: получше закомментить код

        private List<Shape> vertices;
        private Shape? current = null;
        
        private double _previousX;
        private double _previousY;

        private bool _holding;
        // координаты нашей пока единственной вершины.
        public DrawingControl() : base()
        {
            Console.WriteLine("Instantiated");

            vertices = new List<Shape>();
        }

        public void PointerPressed(double x, double y)
        {
            foreach (Shape vertex in vertices)
            {
                if (vertex.IsInside(x, y))
                {
                    Console.WriteLine("Inside");
                    _holding = true;
                    _previousX = x;
                    _previousY = y;
                    current = vertex;
                    InvalidateVisual();
                    return;
                }
            }

            switch
                (Globals.VertexShape) //Проверяем тип вершины из глобалсов. Нашли совпадающий - записываем его, что-то не так - Exception по мордасам, ибо нефиг непотребство пихать.
            {
                case VertexShape.Circle:
                    vertices.Add(new CircleVertex(x, y));
                    break;
                case VertexShape.Square:
                    vertices.Add(new SquareVertex(x, y));
                    break;
                case VertexShape.Triangle:
                    vertices.Add(new TriangleVertex(x, y));
                    break;
                default:
                    throw new Exception("Wrong vertex shape");
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
                current.X = x;
                current.Y = y;
                if (Globals.VertexShape == VertexShape.Triangle)
                {
                    TriangleVertex vertex = (TriangleVertex)current;        //TODO: починить телепортацию фигуры при перетягивании
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
                current = null;
            }
            InvalidateVisual();
        }

        public override void Render(DrawingContext drawingContext)
        {
            foreach (Shape vertex in vertices)
            {
                vertex.Draw(drawingContext);
            }
        }
    }
}