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
    Circle(int x, int y) : base(x, y)
    {}

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine("Circle");
    }
}

class Square : Shape
{
    Square(int x, int y) : base(x, y)
    {}

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine("Suare");
    }
}

class Triangle: Shape
{
    Triangle(int x, int y) : base(x, y)
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
            Console.WriteLine("Hi");
        }
    }
}

//TODO: Список вопросов: Можно ли создать абстрактному классу виртуальный метод? 
// И вообще, можно ли абстрактному классу создать метод с телом?
// Можно ли запечатать метод внутри обычного класса?