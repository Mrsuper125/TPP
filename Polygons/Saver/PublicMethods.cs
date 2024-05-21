using MsBox.Avalonia.Enums;

namespace Polygons;

public partial class Saver
{
    public async void New(object saveTarget)
    {
        if (saved) //Если сохранено, чистим молча
        {
            RequestDataErasure();
            _fileName = null;
            saved = false;
        }

        else
        {
            var dialogResult =
                await Confirm("Save previous data? \"Yes\" for save, \"No\" for erase, \"Cancel\" to cancel operation");

            if (dialogResult == ButtonResult.Yes) //Если надо сохранить, сохраняем
            {
                SaveWithoutQuestion(saveTarget);
                RequestDataErasure();
                _fileName = null;
                saved = false;
            }
            else if (dialogResult == ButtonResult.No)
            {
                RequestDataErasure();
                _fileName = null;
                saved = false;
            }
        }
    }

    public async void Open(object saveTarget)
    {
        if (saved) //Если сохранено - загружаем новый
        {
            string? fileName = await PickFile();
            if (fileName == null)
            {
                await Notify("No proper file name was picked");
                return;
            }

            Load(fileName);
            _fileName = fileName;
        }
        else
        {
            var dialogResult =
                await Confirm("Save previous data? \"Yes\" for save, \"No\" for erase, \"Cancel\" to cancel operation");

            if (dialogResult == ButtonResult.Yes) //Сохраняем и загружаем
            {
                SaveWithoutQuestion(saveTarget);
                string? fileName = await PickFile();
                if (fileName == null)
                {
                    await Notify("No proper file name was picked");
                    return;
                }

                Load(fileName);
                _fileName = fileName; //Подгружаем имя открытого файла
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
                _fileName = fileName; //Подгружаем имя открытого файла
            }
        }
    }

    public async void SaveWithoutQuestion(object saveTarget)
    {
        if (_fileName != null) //Только если имя файла уже было выбрано, сохраняем
        {
            Save(saveTarget);
        }
        else
        {
            string? newName = await PickName(); //Иначе выбираем новое имя
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
            _parentWindow.Close();
        }

        else
        {
            var dialogResult =
                await Confirm("Save data? \"Yes\" for save, \"No\" to erase, \"Cancel\" to cancel operation");

            if (dialogResult == ButtonResult.Yes)
            {
                SaveWithoutQuestion(saveTarget);
                _parentWindow.Close();
            }
            else if (dialogResult == ButtonResult.No)
            {
                _parentWindow.Close();
            }
        }
    }
}