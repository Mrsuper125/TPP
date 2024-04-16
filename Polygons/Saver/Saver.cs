using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Polygons.Windows;

namespace Polygons;

public delegate void EraseData();

public delegate void FillData(List<Shape> vertices);

public partial class Saver
{
    private string? _fileName;
    private readonly Window _parentWindow;

    public bool saved = true;

    public event EraseData RequestDataErasure;
    public event FillData RequestDataFilling;

    public Saver(Window window)
    {
        _fileName = null;
        _parentWindow = window;
    }
}