using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Polygons.Abstract_Algorithms;

namespace Polygons;

public partial class GraphingWindow : Window
{
    public GraphingWindow(AbstractAlgorithms alg, int minimum, int maximum, int step)
    {
        InitializeComponent();
        GraphingControl control = this.Find<GraphingControl>("GraphingControl");
        List<double[]> points = new List<double[]>();
        for (int i = minimum; i <= maximum; i+=step)
        {
            Console.WriteLine(i);
            double milliseconds = Abstract_Algorithms.Algorithms.MeasureTime(alg, i).Milliseconds;
            points.Add(new double[]{i, milliseconds});
        }
        control.Draw(points);
    }
}