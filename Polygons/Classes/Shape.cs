using Avalonia.Media;
using Polygons;

public abstract class Shape
{
    public static double VertexRadius;
    protected double x;
    protected double y;

    public bool IsHeld;
    public bool IsConnected;

    static Shape()
    {
        VertexRadius = Globals.VertexRadius;
    }

    public Shape(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public double X
    {
        get
        {
            return x;
        }
        set                                         // А зачем? Привет Анна Олеговна, прямой доступ к полям теперь вызывает у меня моральную боль.
        {
            x = value;
        }
    }
    
    public double Y
    {
        get
        {
            return y;;
        }
        set
        {
            y = value;
        }
    }

    public abstract bool IsInside(double x, double y);

    public abstract void Draw(DrawingContext drawingContext);
}