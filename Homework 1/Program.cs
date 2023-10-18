using System;

public abstract class Shape
{
    protected static int R;
    protected int x;
    protected int y;

    static Shape()
    {
        R = 2;
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

    public abstract bool IsInside(int x, int y);
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

    public override bool IsInside(int x, int y)
    {
        if (Math.Pow(x-this.x, 2) + Math.Pow(y - this.y, 2) < R*R)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

class Square : Shape
{
    public Square(int x, int y) : base(x, y)
    {}

    public override void Draw()
    {
        base.Draw();
        Console.WriteLine("Square");
    }

    public override bool IsInside(int x, int y)
    {
        double halfWidht = R / Math.Sqrt(2);
        if (x < this.x + halfWidht && x > this.x - halfWidht && y < this.y + halfWidht && y > this.y - halfWidht)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public override bool IsInside(int x, int y)
    {
        double topVertexX = this.x;
        double topVertexY = this.y + R;
        double leftVertexX = this.x - Math.Cos(0.523599d) * R;  //30 градусов - половина угла треугольника, гипотенуза которого = радиус описанной
        double leftVertexY = this.y - Math.Sin(0.523599d) * R;  //Радианы вместо градусов. Спрашивается, нафига? Это шарп, ничего не попишешь.
        double rightVertexX = this.x + Math.Cos(0.523599d) * R;  //30 градусов - половина угла треугольника, гипотенуза которого = радиус описанной
        double rightVertexY = leftVertexY;

        if (!((y - leftVertexY)/(topVertexY - leftVertexY) < (x - leftVertexX)/(topVertexX - leftVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }
        
        if (!((y - rightVertexY)/(topVertexY - rightVertexY) < (x - rightVertexX)/(topVertexX - rightVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }

        if (y < rightVertexY)
        {
            return false;
        }

        return true;
    }
}

namespace Homework_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            Shape[] arr = new Shape[3];
            arr[0] = new Circle(5, 10);
            arr[1] = new Square(5, 10);
            arr[2] = new Triangle(5, 10);
            foreach (Shape obj in arr)
            {
                obj.Draw();
            }
            */

            Circle cir = new Circle(3, 4);
            Console.WriteLine(cir.IsInside(3, 6));
            Square sq = new Square(4, 4);
            Console.WriteLine(sq.IsInside(3, 5));
            Triangle tr = new Triangle(3, 5);
            Console.WriteLine(tr.IsInside(3, 5));
        }
    }
}