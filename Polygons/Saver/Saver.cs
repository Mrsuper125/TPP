using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia.Controls;

namespace Polygons;

public class Saver
{
    private string? fileName;
    private Window parentWindow;

    public Saver(Window window)
    {
        fileName = "save";
        parentWindow = window;
    }
    
    [Obsolete]
    private void
        Save(object saveTarget) //This function picks object to save and saves it, reading file name from field
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(
            fileName + ".bin",
            FileMode.Create,
            FileAccess.Write);
        bf.Serialize(fs, saveTarget);
        fs.Close();
    }

    public void PickName() //This function utilizes the file picker dialogue and returns the filename
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Save file to some location";
        saveFileDialog.InitialFileName = fileName + ".bin";
        saveFileDialog.DefaultExtension = "bin";
        saveFileDialog.Filters.Add(new FileDialogFilter { Name = "Standart Binary files", Extensions = { "bin" } });

        string? result = saveFileDialog.ShowAsync(parentWindow).Result;
        if (result != null)
        {
            fileName = result;
            Console.WriteLine(result);
        }
    }
}