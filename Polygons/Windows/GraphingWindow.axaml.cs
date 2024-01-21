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
        control.Draw(new List<int>(){100, 50}, new List<int>(){100, 50});
    }
}