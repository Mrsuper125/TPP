using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Polygons.Windows;

public class ColorSelectedEventArgs : EventArgs
{
    public readonly byte Red;
    public readonly byte Green;
    public readonly byte Blue;

    public ColorSelectedEventArgs(byte Red, byte Green, byte Blue)
    {
        this.Red = Red;
        this.Green = Green;
        this.Blue = Blue;
    }
}

public delegate void ColorEventHandler(object? sender, ColorSelectedEventArgs e);

public partial class ColorSelectWindow : Window
{
    public event ColorEventHandler colorChanged;
    private bool initialized;
    private Slider redSlider;
    private Slider greenSlider;
    private Slider blueSlider;
    
    public ColorSelectWindow(Color initial)
    {
        InitializeComponent();
        redSlider = this.Find<Slider>("RedSlider");
        redSlider.Value = initial.R;
        greenSlider = this.Find<Slider>("GreenSlider");
        greenSlider.Value = initial.G;
        blueSlider = this.Find<Slider>("BlueSlider");
        blueSlider.Value = initial.B;
    }   
    
    public void RegisterInitialization()
    {
        initialized = true;
    }
    
    public void RegisterClose()
    {
        initialized = false;
    }
    
    private void RedSlider_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (initialized) colorChanged(this, new ColorSelectedEventArgs((byte)e.NewValue, (byte)greenSlider.Value, (byte)blueSlider.Value));
    }

    private void GreenSlider_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (initialized) colorChanged(this, new ColorSelectedEventArgs((byte)redSlider.Value, (byte)e.NewValue,  (byte)blueSlider.Value));
    }
    
    private void BlueSlider_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (initialized) colorChanged(this, new ColorSelectedEventArgs((byte)redSlider.Value, (byte)greenSlider.Value, (byte)e.NewValue));
    }
}