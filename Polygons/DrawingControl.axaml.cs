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
        
        private double _previousX;      //В эти переменные записываются предыдущие координаты курсора для вычисления дельты
        private double _previousY;

        private bool _holding;
        // координаты нашей пока единственной вершины.
        public DrawingControl() : base()
        {
            Console.WriteLine("Instantiated");

            vertices = new List<Shape>(); //Список для всех вершин
        }

        public void LeftPointerPressed(double x, double y)
        {
            foreach (Shape vertex in vertices)      //Проверяем все вершины на предмет клика внутри них
            {
                if (vertex.IsInside(x, y))
                {
                    Console.WriteLine("Inside");
                    _holding = true;
                    _previousX = x;         //Если хотя бы одна вершина захвачена, начинаем фиксировать координаты
                    _previousY = y;
                    vertex.IsHeld = true; //Записываем в поля вершины то, что мы её удерживаем
                }
            }

            if (!_holding)        //Если ни одну вершину не захватили, создаём новую
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

        public void RigthPointerPressed(double x, double y)
        {
            for (int i = vertices.Count - 1; i >= 0; i--)           //Перебор в обратном порядке, т.к. верхние фигуры рендерятся последними
            {
                Shape vertex = vertices[i];
                if (vertex.IsInside(x, y))
                {
                    vertices.RemoveAt(i);       //Попали - удаляем из списка, тогда рендер просто не нарисует её
                    Console.WriteLine("Deleted");
                    break;
                }
            }
            InvalidateVisual();
        }
        
         public void PointerMoved(double x, double y)
        {
            if (_holding)           //Если удерживаем хоть одну вершину, высчитываем движение
            {
                Console.WriteLine("Drag");
                foreach (Shape vertex in vertices)      //Проходимся по списку вершин. Если она удерживается - двигаем
                {
                    if (vertex.IsHeld)
                    {
                        vertex.X += x - _previousX;         //Высчитываем дельту через текущую и предыдущую позиции курсора
                        vertex.Y += y - _previousY;
                    }
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
                foreach (Shape vertex in vertices)
                {
                    vertex.IsHeld = false;      //Гарантированно отпускаем все вершины
                }
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