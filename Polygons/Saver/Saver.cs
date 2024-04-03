using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Polygons;


public class Saver
{
    private string _lastUsedName;
    private bool _wasSaved;
    private readonly Window _parentWindow;

    public bool WasSaved
    {
        get { return _wasSaved; }
    }

    public Saver(Window window)
    {
        _lastUsedName = "save";
        _parentWindow = window;
        this._wasSaved = false;
    }
    
    [Obsolete]
    private void
        SaveWithName(object saveTarget, string name) //This function picks object to save and saves it, reading file name from field
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(
            name + ".bin",
            FileMode.Create,
            FileAccess.Write);
        bf.Serialize(fs, saveTarget);
        fs.Close();
    }

    private string PickName() //This function utilizes the file picker dialogue and returns the filename
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Save file to some location";
        saveFileDialog.InitialFileName = _lastUsedName + ".bin";
        saveFileDialog.DefaultExtension = "bin";
        saveFileDialog.Filters.Add(new FileDialogFilter { Name = "Standart Binary files", Extensions = { "bin" } });

        string? result = saveFileDialog.ShowAsync(_parentWindow).Result;
        if (result != null)
        {
            _lastUsedName = result;
            return result;
        }

        return "";
    }

    [Obsolete("Obsolete")]
    public void SaveAs(object saveTarget)
    {
        string name = PickName();
        SaveWithName(saveTarget, name);
        _wasSaved = true;
    }

    [Obsolete("Obsolete")]
    public void Save(object saveTarget)
    {
        string nameToUse;
        if (_wasSaved)
        {
            nameToUse = _lastUsedName;
        }
        else
        {
            nameToUse = PickName();
        }
        SaveWithName(saveTarget, nameToUse);
        _wasSaved = true;
    }

    public void DoAction()
    {
        _wasSaved = false;
    }

    [Obsolete("Obsolete")]
    public object PickAndLoad()
    {
        var files = _parentWindow.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open file with saved data",
            AllowMultiple = false
        }).Result;

        if (files.Count >= 1)
        {
            var stream = files[0].OpenReadAsync().Result;
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }

        return null;
    }
}