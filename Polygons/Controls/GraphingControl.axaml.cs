using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Polygons;

public partial class GraphingControl : UserControl
{
    private List<int> xAxis;
    private List<int> yAxis;

    private Brush brush;
    private Pen pen;

    public GraphingControl()
    {
        brush = new SolidColorBrush(Globals.FillColor);
        pen = new Pen(Globals.BrushColor);
        xAxis = new List<int>();
        yAxis = new List<int>();
    }

    public void Draw(List<int> xAxis, List<int> yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public override void Render(DrawingContext context)
    {
        for (int i = 0; i < xAxis.Count; i++)
        {
            context.DrawEllipse(brush, pen, new Point(xAxis[i], yAxis[i]), 10d, 10d);
        }
    }
}