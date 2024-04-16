using System.Threading.Tasks;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Polygons;

public partial class Saver
{
    public void ActionCommitted()
    {
        saved = false;
    }

    public async Task<ButtonResult> Confirm(string prompt)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Confirm your actions", prompt, ButtonEnum.YesNoCancel);
        var result = await box.ShowWindowDialogAsync(_parentWindow);
        return result;
    }

    public async Task<ButtonResult> Notify(string prompt)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Error", prompt, ButtonEnum.Ok);
        var result = await box.ShowWindowDialogAsync(_parentWindow);
        return result;
    }
}