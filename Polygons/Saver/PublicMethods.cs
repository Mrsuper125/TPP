using MsBox.Avalonia.Enums;

namespace Polygons;

public partial class Saver
{
    public async void New(object saveTarget)
    {
        if (saved)
        {
            RequestDataErasure();
        }

        else
        {
            var dialogResult =
                await Confirm("Save previous data? \"Yes\" for save, \"No\" for erase, \"Cancel\" to cancel operation");

            if (dialogResult == ButtonResult.Yes)
            {
                SaveWithQuestion(saveTarget);
                RequestDataErasure();
            }
            else if (dialogResult == ButtonResult.No)
            {
                RequestDataErasure();
            }
        }
    }

    public async void Open(object saveTarget)
    {
        
        if (saved)
        {
            string? fileName = await PickFile();
            if (fileName == null)
            {
                await Notify("No proper file name was picked");
                return;
            }
            Load(fileName);
        }
        else
        {
            var dialogResult =
                await Confirm("Save previous data? \"Yes\" for save, \"No\" for erase, \"Cancel\" to cancel operation");
            
            if (dialogResult == ButtonResult.Yes)
            {
                SaveWithQuestion(saveTarget);
                string? fileName = await PickFile();
                if (fileName == null)
                {
                    await Notify("No proper file name was picked");
                    return;
                }
                Load(fileName);
            }
            else if (dialogResult == ButtonResult.No)
            {
                string? fileName = await PickFile();
                if (fileName == null)
                {
                    await Notify("No proper file name was picked");
                    return;
                }
                Load(fileName);
            }
        }
    }
    
    public async void SaveWithoutQuestion(object saveTarget)
    {
        if (_fileName != null)
        {
            Save(saveTarget);
        }
        else
        {
            string? newName = await PickName();
            if (newName != null)
            {
                Save(saveTarget, newName);
            }
        }
    }

    public async void SaveWithQuestion(object saveTarget)
    {
        string? newName = await PickName();
        if (newName != null)
        {
            Save(saveTarget, newName);
        }
    }

    public async void Exit(object saveTarget)
    {
        if (saved)
        {
            RequestDataErasure();
            _parentWindow.Close();
        }

        else
        {
            var dialogResult =
                await Confirm("Save data? \"Yes\" for save, \"No\" to erase, \"Cancel\" to cancel operation");

            if (dialogResult == ButtonResult.Yes)
            {
                SaveWithQuestion(saveTarget);
                _parentWindow.Close();
            }
            else if (dialogResult == ButtonResult.No)
            {
                _parentWindow.Close();
            }
        }
    }
}