using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace First_Avalonia_attempt;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    public void SendToHell_OnClick(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Сам нахуй сходи");
    }
}