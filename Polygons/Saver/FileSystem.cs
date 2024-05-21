using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace Polygons;

public partial class Saver
{
    [Obsolete]
    private void
        Save(object saveTarget,
            string? fileName = null) //This function picks object to save and saves it, reading file name from field
    {
        if (fileName == null)
        {
            fileName = _fileName;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(
            fileName,
            FileMode.Create,
            FileAccess.Write);
        bf.Serialize(fs, saveTarget);
        bf.Serialize(fs, Globals.VertexRadius);
        bf.Serialize(fs, Globals.VertexShape);
        bf.Serialize(fs, Globals.BrushColor.Color.R);
        bf.Serialize(fs, Globals.BrushColor.Color.G);
        bf.Serialize(fs, Globals.BrushColor.Color.B);
        fs.Close();
        saved = true;
    }

    [Obsolete("Obsolete")]
    private void Load(string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        var vertices = (List<Shape>)(bf.Deserialize(fs));
        Globals.VertexRadius = (double)(bf.Deserialize(fs));
        Globals.VertexShape = (VertexShape)(bf.Deserialize(fs));
        byte red = (byte)bf.Deserialize(fs);
        byte green = (byte)bf.Deserialize(fs);
        byte blue = (byte)bf.Deserialize(fs);
        Globals.BrushColor = new ImmutableSolidColorBrush(new Color(100, red, green, blue));
        fs.Close();
        RequestDataFilling(vertices);
        saved = true;
    }
}