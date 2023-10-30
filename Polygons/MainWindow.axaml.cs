using Avalonia.Controls;
using System;

namespace Polygons;

public partial class MainWindow : Window
{
    
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

    
    public MainWindow()
    {
        InitializeComponent();
    }
}