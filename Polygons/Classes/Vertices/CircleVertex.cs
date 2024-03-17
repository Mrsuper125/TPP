using System;
using Avalonia;
using Avalonia.Media;

namespace Polygons;

[Serializable]
class CircleVertex : Shape
{
    public CircleVertex(double x, double y) : base(x, y)
    {
    }

    public override bool IsInside(double x, double y)
    {
        if (Math.Pow(x - this.x, 2) + Math.Pow(y - this.y, 2) <
            VertexRadius * VertexRadius) //Переменные родительского класса достаются напрямую, минуя свойства. В этом особых плюсов, как и минусов, нет, мне просто так кажется логичнее - "свои" же. Ну это правится быстро, если что.
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Draw(DrawingContext drawingContext)
    {
        Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
        Brush brush = new SolidColorBrush(Globals.FillColor);
        drawingContext.DrawEllipse(brush, pen, new Point(this.x, this.y), VertexRadius, VertexRadius);
    }
}