using System;

public class Shape
{
    private static int R;
    private int x;
    private int y;

    static Shape()
    {
        R = 35;
    }

    public Shape(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public virtual void Draw()
    {
        Console.WriteLine($"X: {x}, Y: {y}, R: {R}");
    }
}

class Circle : Shape
{
    public Circle(int x, int y) : base(x, y)
    {}

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine("Circle");
    }
}

class Square : Shape
{
    public Square(int x, int y) : base(x, y)
    {}

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine("Suare");
    }
}

class Triangle: Shape
{
    public Triangle(int x, int y) : base(x, y)
    {}

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine("Triangle");
    }
}

namespace Homework_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Shape[] arr = new Shape[3];
            arr[0] = new Circle(5, 10);
            arr[1] = new Square(5, 10);
            arr[2] = new Triangle(5, 10);
            foreach (Shape obj in arr)
            {
                obj.Draw();
            }
        }
    }
}

//TODO: Список вопросов: Можно ли создать абстрактному классу виртуальный метод? 
// И вообще, можно ли абстрактному классу создать метод с телом?
// Можно ли запечатать метод внутри обычного класса?