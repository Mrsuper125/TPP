using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Polygons;

public partial class GraphingControl : UserControl
{
    List<double[]> points;

    private Brush brush;
    private Pen pen;
    private Brush textBrush;

    private double maxX;
    private double maxY;
    
    public GraphingControl()
    {
        brush = new SolidColorBrush(Globals.FillColor);
        pen = new Pen(Globals.BrushColor);
        textBrush = new SolidColorBrush(Globals.TextColor);
    }

    public void Draw(List<double[]> points)
    {
        this.points = points;
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i][0] > maxX) maxX = points[i][0];
            if (points[i][1] > maxY) maxY = points[i][1];
        }
    }

    public void DrawPoint(DrawingContext context, double x, double y)
    {
        double displayedX = 100 + 1200 * (x / maxX);
        double displayedY = 900 - 700 * (y / maxY);
        Console.WriteLine(x);
        Console.WriteLine(x / maxX);
        context.DrawEllipse(brush, pen, new Point(displayedX, displayedY), 5d, 5d);
        
    }

    public override void Render(DrawingContext context)
    {
        context.DrawLine(pen,  new Point(100, 900), new Point(1400, 900));
        context.DrawLine(pen,  new Point(1400, 900), new Point(1375, 925));      //X-axis
        context.DrawLine(pen,  new Point(1400, 900), new Point(1375, 875));
        context.DrawText(new FormattedText("X", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 20d, textBrush), new Point(1425, 900));
        
        context.DrawLine(pen, new Point(100, 100), new Point(100, 900));
        context.DrawLine(pen, new Point(100, 100), new Point(125, 125));        //Y-axis
        context.DrawLine(pen, new Point(100, 100), new Point(75, 125));
        context.DrawText(new FormattedText("Y", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 20d, textBrush), new Point(100, 75));
        
        context.DrawLine(pen, new Point(1300, 925), new Point(1300, 875));
        context.DrawText(new FormattedText(this.maxX.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 20d, textBrush), new Point(1300, 950));  //Measures on the graph
        
        context.DrawLine(pen, new Point(75, 200), new Point(125, 200));
        context.DrawText(new FormattedText(this.maxY.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 20d, textBrush), new Point(50, 200));
        
        for (int i = 0; i < this.points.Count; i++)
        {
            DrawPoint(context, points[i][0], points[i][1]);
        }
    }
}