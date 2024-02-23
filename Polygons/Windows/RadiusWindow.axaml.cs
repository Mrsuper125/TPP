using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;

namespace Polygons;

public class RadiusEventArgs : EventArgs
{
    public double R;

    public RadiusEventArgs(double R)
    {
        this.R = R;
    }
}

public delegate void RadiusEventHandler(object? sender, RadiusEventArgs e);

public partial class RadiusWindow : Window
{
    public event RadiusEventHandler RadiusChanged;
    private bool initialized;
    
    public RadiusWindow()
    {
        InitializeComponent();
        initialized = true;
    }


    private void RadiusSlider_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (initialized) RadiusChanged(this, new RadiusEventArgs(e.NewValue));
    }
}