using Avalonia.Controls;
using System;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Polygons;
using Polygons.Abstract_Algorithms;
using Polygons.Windows;

namespace Polygons;

public partial class MainWindow : Window
{
    public bool IsClickOnUI;    //Костыльная переменная, в которую пишется true, если клик пришёл по чему угодно, кроме рисовалки. TODO: спросить Завра про идеи получше
    private RadiusWindow radiusWindow;
    private bool radiusWindowAlive;
    private ColorSelectWindow colorSelectWindow;
    private bool colorSelectWindowAlive;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InitializeRadiusWindow()
    {
        radiusWindow = new RadiusWindow(Shape.VertexRadius);
        radiusWindow.RadiusChanged += OnRadiusChanged;
        radiusWindowAlive = true;
        radiusWindow.Closed += RadiusWindowClosed;
        radiusWindow.RegisterInitialization();
    }

    private void InitializeColorWindow()
    {
        colorSelectWindow = new ColorSelectWindow(Globals.BrushColor.Color);
        colorSelectWindow.colorChanged += OnColorChanged;
        colorSelectWindowAlive = true;
        colorSelectWindow.Closed += ColorWindowClosed;
        colorSelectWindow.RegisterInitialization();
    }

    private void RadiusWindowClosed(object? sender, EventArgs args)
    {
        radiusWindowAlive = false;
        radiusWindow.RegisterClose();
    }

    private void ColorWindowClosed(object? sender, EventArgs args)
    {
        colorSelectWindowAlive = false;
        colorSelectWindow.RegisterClose();
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
    
    public void OnRadiusChanged(object? sender, RadiusEventArgs e)
    {
        Shape.VertexRadius = e.R;
        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        cc.UpdateVisual();
    }

    public void OnColorChanged(object? sender, ColorSelectedEventArgs e)
    {
        Globals.BrushColor = new ImmutableSolidColorBrush(new Color(255, e.Red, e.Green, e.Blue));
        Console.WriteLine(e.Red);
        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        cc.UpdateVisual();
    }

    private void Menu_OnRadiusPressed(object? sender, PointerPressedEventArgs e)
    {
        IsClickOnUI = true;
        if (radiusWindowAlive)
        {
            radiusWindow.Activate();
        }
        else
        {
            InitializeRadiusWindow();
            radiusWindow.Show();
        }
    }

    private void Menu_OnColorPressed(object? sender, PointerPressedEventArgs e)
    {
        IsClickOnUI = true;
        if (colorSelectWindowAlive)
        {
            colorSelectWindow.Activate();
        }
        else
        {
            InitializeColorWindow();
            colorSelectWindow.Show();
        }
    }

    private void Menu_OnJarvisMeasurePressed(object? sender, PointerPressedEventArgs e)
    {
        IsClickOnUI = true;
        GraphingWindow graphingWindow = new GraphingWindow(AbstractAlgorithms.Jarvis, 1000, 10000, 100);
        graphingWindow.Show();
    }
    
    private void Menu_OnZavrMeasurePressed(object? sender, PointerPressedEventArgs e)
    {
        IsClickOnUI = true;
        GraphingWindow graphingWindow = new GraphingWindow(AbstractAlgorithms.Zavr, 100, 300, 10);
        graphingWindow.Show();
    }

    private void Menu_OnSavePressed(object? sender, PointerPressedEventArgs e)
    {
        IsClickOnUI = true;
        DrawingControl cc = this.Find<DrawingControl>("MyDrawingControl");
        cc.SaveState();
    }
}