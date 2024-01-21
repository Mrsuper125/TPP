using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Polygons;

public partial class GraphingWindow : Window
{
    public GraphingWindow(/*List<int> xAxis, List<int> yAxis*/)
    {
        InitializeComponent();
        GraphingControl control = this.Find<GraphingControl>("GraphingControl");
        control.Draw(new List<double[]>
        {
            new []{100d, 100d},
            new []{75d, 75d},
            new []{50d, 50d},
            new []{25d, 25d},
            new []{0d, 0d},
        });
    }
}