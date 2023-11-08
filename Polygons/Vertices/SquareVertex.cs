using System;
using Avalonia;
using Avalonia.Media;

namespace Polygons;

class SquareVertex : Shape
{
    public readonly double halfWidht;

    public SquareVertex(double x, double y) : base(x, y)
    {
        halfWidht = VertexRadius / Math.Sqrt(2);
    }

    public override bool IsInside(double x, double y)
    {
        if (x < this.x + halfWidht && x > this.x - halfWidht && y < this.y + halfWidht && y > this.y - halfWidht)
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
        drawingContext.DrawRectangle(brush, pen, new Rect(this.x, this.y, this.halfWidht, this.halfWidht));
    }
}