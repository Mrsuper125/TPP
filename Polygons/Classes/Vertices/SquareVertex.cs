using System;
using Avalonia;
using Avalonia.Media;

namespace Polygons;

class SquareVertex : Shape
{
    public double halfWidht;

    public SquareVertex(double x, double y) : base(x, y)
    {
        
    }

    public override bool IsInside(double x, double y)
    {
        halfWidht = VertexRadius / Math.Sqrt(2);
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
        drawingContext.DrawRectangle(brush, pen, new Rect(this.x - halfWidht, this.y - halfWidht, this.halfWidht * 2, this.halfWidht * 2));
    }
}