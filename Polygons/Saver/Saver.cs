using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia.Controls;

namespace Polygons;

public class Saver
{
    private string? _fileName;
    private readonly Window _parentWindow;

    public Saver(Window window)
    {
        _fileName = null;
        _parentWindow = window;
    }
    
    [Obsolete]
    private void
        Save(object saveTarget) //This function picks object to save and saves it, reading file name from field
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(
            _fileName + ".bin",
            FileMode.Create,
            FileAccess.Write);
        bf.Serialize(fs, saveTarget);
        fs.Close();
    }

    public async void PickName() //This function utilizes the file picker dialogue and returns the filename. It is async to make it non-blocking. The file still won't be saved until a name is picked
    {
        Console.WriteLine("Picking name");
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Save file to some location";
        saveFileDialog.InitialFileName = _fileName + ".bin";
        saveFileDialog.DefaultExtension = "bin";
        saveFileDialog.Filters.Add(new FileDialogFilter { Name = "Standart Binary files", Extensions = { "bin" } });

        string? result = await saveFileDialog.ShowAsync(_parentWindow);         //Описание проблемы: требуется объяснить async/await. .Result тут не работает, если же переделывать весь метод в ассинхронный, мы не знаем конца
        Console.WriteLine("Here");
        
        if (result != null)
        {
            _fileName = result;
            Console.WriteLine(result);
        }
    }

    public void SaveWithoutQuestion(object saveTarget)
    {
        if (_fileName != null)
        {
            Save(saveTarget);
        }
        else
        {
            PickName().;
        }
    }
}