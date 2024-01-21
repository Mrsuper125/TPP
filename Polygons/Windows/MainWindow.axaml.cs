using Avalonia.Controls;
using System;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Polygons;

public partial class MainWindow : Window
{
    public bool IsClickOnUI;    //Костыльная переменная, в которую пишется true, если клик пришёл по чему угодно, кроме рисовалки. TODO: спросить Завра про идеи получше
    
    public MainWindow()
    {
        InitializeComponent();
        GraphingWindow graphingWindow = new GraphingWindow();
        graphingWindow.Show();
    }

    private void Menu_OnClick(object? sender, PointerPressedEventArgs e)
    {
        IsClickOnUI = true; //Пока что общий плейсхолдерный перехватчик UI-шных кликов
    }

    private void Menu_TriangleSelect(object? sender, PointerPressedEventArgs e)
    {
        Globals.VertexShape = VertexShape.Triangle;
    }
    
    private void Menu_CircleSelect(object? sender, PointerPressedEventArgs e)
    {
        Globals.VertexShape = VertexShape.Circle;
    }
    
    private void Menu_SquareSelect(object? sender, PointerPressedEventArgs e)
    {
        Globals.VertexShape = VertexShape.Square;
    }
    
    private void Menu_ZavrSelect(object? sender, PointerPressedEventArgs e)
    {
        Globals.Algorithm = Algorithms.Zavr;  
    }
    
    private void Menu_JarvisSelect(object? sender, PointerPressedEventArgs e)
    {
        Globals.Algorithm = Algorithms.Jarvis;
    }

private void Win_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        Console.WriteLine("CLICK"); 
        // отладочная печать – просто проверить, что метод вызвался при клике

        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        // нам нужен объект (переменная), ссылающийся на наш контрол.
        // для этого воспользуемся методом Find и найдем его по имени myCC

        if (!IsClickOnUI)       //Перехватываем UI-шные клики и вызываем методы клика только когда клик не по UI
        {
            if (e.GetCurrentPoint(null).Properties.IsLeftButtonPressed)
            {
                cc.LeftPointerPressed(e.GetPosition(cc).X, e.GetPosition(cc).Y);

                // при помощи GetPosition приводим координаты курсора 
                // в систему координат нашего контрола для рисования
                // передав его (cc) в качестве параметра

                // А затем просто вызываем созданный выше метод PointerPressed, 
                // относящийся уже не к окну, а к контролу – там мы сможем рисовать

            }

            if (e.GetCurrentPoint(null).Properties.IsRightButtonPressed)
            {
                cc.RigthPointerPressed(e.GetPosition(cc).X, e.GetPosition(cc).Y);
            }
        }
        else
        {
            IsClickOnUI = false;        //Если клик пришёл по UI, поняли, нифига не делаем, только следующий чур будет какой надо
        }
    }

    private void Win_PointerMoved(object? sender, PointerEventArgs e)       //Как и предыдущий, ловит событие (здесь - перемещение курсора) и прокидывает координаты в DrawingControl
    {
        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        cc.PointerMoved(e.GetPosition(cc).X, e.GetPosition(cc).Y);          //Здесь и в Released костыль с IsClickOnUI не засунут для экономии места в коде - если уже перехватили клики, эти 2 метода ничего не сделают
    }
    
    private void Win_PointerReleased(object? sender, PointerReleasedEventArgs e)    //Та же аналогия
    {
        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        cc.PointerReleased(e.GetPosition(cc).X, e.GetPosition(cc).Y);
    }
}