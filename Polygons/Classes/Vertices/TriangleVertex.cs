using System;
using Avalonia;
using Avalonia.Media;

namespace Polygons;

[Serializable]
class TriangleVertex: Shape
{
    [NonSerialized]
    private double _topVertexX;
    [NonSerialized]
    private double _topVertexY;
    [NonSerialized]
    private double _leftVertexX;
    [NonSerialized]
    private double _leftVertexY;
    [NonSerialized]
    private double _rightVertexX;
    [NonSerialized]
    private double _rightVertexY;

    public double TopVertexX => _topVertexX;

    public double TopVertexY => _topVertexY;

    public double LeftVertexX => _leftVertexX;

    public double LeftVertexY => _leftVertexY;

    public double RightVertexX => _rightVertexX;

    public double RightVertexY => _rightVertexY;
    

    public TriangleVertex(double x, double y) : base(x, y)
    {
        InvalidateVertices();
    }

    public void InvalidateVertices()
    {
        _topVertexX = this.x;
        _topVertexY = this.y + VertexRadius;                            //TODO: Перевернуть треугольник
        _leftVertexX = this.x - Math.Cos(0.523599d) * VertexRadius;
        _leftVertexY = this.y - Math.Sin(0.523599d) * VertexRadius;
        _rightVertexX = this.x + Math.Cos(0.523599d) * VertexRadius;
        _rightVertexY = _leftVertexY;
    }

    public override bool IsInside(double x, double y)
    {
        InvalidateVertices();
        if (!((y - _leftVertexY)/(_topVertexY - _leftVertexY) < (x - _leftVertexX)/(_topVertexX - _leftVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }
        
        if (!((y - _rightVertexY)/(_topVertexY - _rightVertexY) < (x - _rightVertexX)/(_topVertexX - _rightVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }

        if (y < _rightVertexY)
        {
            return false;
        }

        return true;
    }

    public override void Draw(DrawingContext drawingContext)
    {
        InvalidateVertices();
        Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
        Brush brush = new SolidColorBrush(Globals.FillColor);
        drawingContext.DrawGeometry(brush, pen, new PolylineGeometry(new Point[4]{new Point(this.LeftVertexX, this.LeftVertexY), new Point(this.TopVertexX, this.TopVertexY), new Point(this.RightVertexX, this.RightVertexY), new Point(this.LeftVertexX, this.LeftVertexY)}, false));
    }
}