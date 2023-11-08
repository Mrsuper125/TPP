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
        private List<Shape> heldVeritces;
        
        private double _previousX;      //В эти переменные записываются предыдущие координаты курсора для вычисления дельты
        private double _previousY;

        private bool _holding;
        // координаты нашей пока единственной вершины.
        public DrawingControl() : base()
        {
            Console.WriteLine("Instantiated");

            vertices = new List<Shape>();
            heldVeritces = new List<Shape>();       //Списки для всех вершин и для удерживаемых в данный момент вершин
        }

        public void PointerPressed(double x, double y)
        {
            foreach (Shape vertex in vertices)      //Проверяем все вершины на предмет клика внутри них
            {
                if (vertex.IsInside(x, y))
                {
                    Console.WriteLine("Inside");
                    _holding = true;
                    _previousX = x;         //Если хотя бы одна вершина захвачена, начинаем фиксировать координаты и добавляем в список двигаемых першин
                    _previousY = y;
                    heldVeritces.Add(vertex);       //Добавляем ссылку на объект вершины в список удерживаемых
                }
            }

            if (heldVeritces.Count == 0)        //Если ни одну вершину не захватили, создаём новую
            {
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
            if (_holding)           //Если удерживаем хоть одну вершину, высчитываем движение
            {
                Console.WriteLine("Drag");
                foreach (Shape vertex in heldVeritces)      //Проходимся по списку ужерживаемых вершин. Классы ссылочные, поэтому объекты изменятся везде, где используются
                {
                    vertex.X += x - _previousX;
                    vertex.Y += y - _previousY;
                }
                _previousX = x;
                _previousY = y;
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
                heldVeritces = new List<Shape>();
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