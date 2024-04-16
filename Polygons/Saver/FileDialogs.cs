using System.Threading.Tasks;
using Avalonia.Controls;

namespace Polygons;

public partial class Saver
{
    private async Task<string?> PickFile() //This function returns an existing file
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Open existing save file";
        openFileDialog.AllowMultiple = false;
        string[]? result = await openFileDialog.ShowAsync(_parentWindow);

        if (result != null && result.Length >= 1)
        {
            _fileName = result[0];
            return result?[0];
        }
        else
        {
            return null;
        }
    }

    private async Task<string?>
        PickName() //This function utilizes the file picker dialogue and returns the filename. It is async to make it non-blocking. The file still won't be saved until a name is picked
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Save file to some location";
        saveFileDialog.InitialFileName = _fileName + ".bin";
        saveFileDialog.DefaultExtension = "bin";
        saveFileDialog.Filters.Add(new FileDialogFilter { Name = "Standart Binary files", Extensions = { "bin" } });

        string?
            result = await saveFileDialog
                .ShowAsync(_parentWindow); //Описание проблемы: требуется объяснить async/await. .Result тут не работает, если же переделывать весь метод в ассинхронный, мы не знаем конца

        if (result != null)
        {
            _fileName = result;
        }

        return result;
    }
}