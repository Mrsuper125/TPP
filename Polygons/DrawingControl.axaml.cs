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
            
            if (vertices.Count >= 3)
            {
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (!vertices[i].IsConnected)
                    {
                        vertices.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            InvalidateVisual();
        }

        public void ZarvAlgorithm(DrawingContext drawingContext)
        {
            foreach (Shape vertex in vertices)
            {
                vertex.IsConnected = false;
            }
            if (vertices.Count >= 3)
            {
                for (int i = 0; i < vertices.Count - 1; i++)
                {
                    for (int j = i+1; j < vertices.Count; j++)
                    {
                        Shape first = vertices[i];
                        Shape second = vertices[j];
                        double k = (second.Y - first.Y) / (second.X - first.X);
                        double b = first.Y - first.X * k;
                        int above = 0;
                        int below = 0;
                        for (int l = 0; l < vertices.Count; l++)
                        {
                            if ((l != i) && (l != j))
                            {
                                Shape checking = vertices[l];
                                double tempY = k * checking.X + b;
                                if (tempY > checking.Y)
                                {
                                    below++;
                                }
                                if (tempY < checking.Y)
                                {
                                    above++;
                                }
                            }
                        }
                        if (above == 0 || below == 0)
                        {
                            Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
                            drawingContext.DrawLine(pen, new Point(first.X, first.Y), new Point(second.X, second.Y));
                            first.IsConnected = true;
                            second.IsConnected = true;
                        }
                    }
                }
            }
        }
        
        public double VectorsCos(Shape first, Shape second, Shape third)
        {
            double firstVectorX = first.X - second.X;
            double firstVectorY = first.Y - second.Y;
            double secondVectorX = third.X - second.X;
            double secondVectorY = third.Y - second.Y;
            double firstVectorLength = Math.Sqrt(firstVectorX * firstVectorX + firstVectorY * firstVectorY);
            double secondVectorLength = Math.Sqrt(secondVectorX * secondVectorX + secondVectorY * secondVectorY);
            return (firstVectorX * secondVectorX + firstVectorY * secondVectorY) /
                   (firstVectorLength * secondVectorLength);
        }

        public double Distance(Shape first, Shape second)
        {
            double firstVectorX = first.X - second.X;
            double firstVectorY = first.Y - second.Y;
            double firstVectorLength = Math.Sqrt(firstVectorX * firstVectorX + firstVectorY * firstVectorY);
            return firstVectorLength;
        }

        public void JarvisAlgorithm(DrawingContext drawingContext)
        {
            foreach (Shape vertex in vertices)
            {
                vertex.IsConnected = false;
            }

            if (vertices.Count >= 3)
            {
                Shape lowest = vertices[0];
                for (int i = 0; i < vertices.Count; i++)
                {
                    if ((vertices[i].Y > lowest.Y))
                    {
                        lowest = vertices[i];
                    }
                    else if((vertices[i].Y == lowest.Y) && (vertices[i].X < lowest.X))
                    {
                        lowest = vertices[i];
                    }
                }

                lowest.IsConnected = true;
                
                Shape current = lowest;
                
                List<Shape> ShapeVertices = new List<Shape>();
                
                ShapeVertices.Add(current);

                Shape previous = new CircleVertex(lowest.X - 100, lowest.Y);

                Shape next = new CircleVertex(lowest.X - 5, lowest.Y);
                
                while (current != lowest)
                {
                    double MinCos = 1;
                    next = null;
                    for (int i = 0; i < vertices.Count; i++)
                    {
                        if (next != current && next != previous)
                        {
                            if (VectorsCos(previous, current, vertices[i]) < MinCos)
                            {
                                next = vertices[i];
                                MinCos = VectorsCos(previous, current, vertices[i]);
                            }
                            else if (VectorsCos(previous, current, vertices[i]) == MinCos && Distance(current, vertices[i]) < Distance(current, next))
                            {
                                next = vertices[i];
                            }
                        }
                    }
                    
                    Pen pen = new Pen(Brushes.Crimson, 1, lineCap: PenLineCap.Square);
                    Brush brush = new SolidColorBrush(Globals.FillColor);
                    
                    // drawingContext.DrawLine(pen, new Point(previous.X, previous.Y), new Point(current.X, current.Y));
                    // drawingContext.DrawLine(pen, new Point(current.X, current.Y), new Point(next.X, next.Y));
                    //
                    // drawingContext.DrawEllipse(brush, pen, new Point(current.X, current.Y), Globals.VertexRadius, Globals.VertexRadius);
                    // drawingContext.DrawEllipse(brush, pen, new Point(current.X, current.Y), Globals.VertexRadius, Globals.VertexRadius);
                    
                    previous = current;
                    current = next;
                    
                    ShapeVertices.Add(current);
                }
                
                for (int i = 0; i < ShapeVertices.Count; i++)
                {
                    Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
                    drawingContext.DrawLine(pen, new Point(ShapeVertices[i].X, ShapeVertices[i].Y), new Point(ShapeVertices[i+1].X, ShapeVertices[i+1].Y));
                    ShapeVertices[i].IsConnected = true;
                    ShapeVertices[i+1].IsConnected = true;
                }
            }
        }

        public override void Render(DrawingContext drawingContext)
        {
            foreach (Shape vertex in vertices)
            {
                vertex.Draw(drawingContext);
            }
            
            JarvisAlgorithm(drawingContext);
        }
    }
}