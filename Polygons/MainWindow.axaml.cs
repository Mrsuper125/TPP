using Avalonia.Controls;
using System;
using Avalonia.Input;

namespace Polygons;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void Win_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        Console.WriteLine("CLICK"); 
        // отладочная печать – просто проверить, что метод вызвался при клике

        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        // нам нужен объект (переменная), ссылающийся на наш контрол.
        // для этого воспользуемся методом Find и найдем его по имени myCC

        cc.PointerPressed (e.GetPosition(cc).X, e.GetPosition(cc).Y);
        // при помощи GetPosition приводим координаты курсора 
        // в систему координат нашего контрола для рисования
        // передав его (cc) в качестве параметра

        // А затем просто вызываем созданный выше метод PointerPressed, 
        // относящийся уже не к окну, а к контролу – там мы сможем рисовать
    }

    private void Win_PointerMoved(object? sender, PointerEventArgs e)       //Как и предыдущий, ловит событие (здесь - перемещение курсора) и прокидывает координаты в DrawingControl
    {
        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        cc.PointerMoved(e.GetPosition(cc).X, e.GetPosition(cc).Y);
    }
    
    private void Win_PointerReleased(object? sender, PointerReleasedEventArgs e)    //Та же аналогия
    {
        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        cc.PointerReleased(e.GetPosition(cc).X, e.GetPosition(cc).Y);
    }
}