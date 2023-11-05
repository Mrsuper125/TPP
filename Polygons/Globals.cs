using Avalonia.Media;

namespace Polygons;
public enum VertexShape         //Enum с перечислением вершин.
{                               //Нужен для удобного переключение типов вершин в коде.
    Circle,                     //Это точно с приходом делегатов придётся переписать.
    Square,                     //А ещё это пока что ставит крест на плагинах
    Triangle                    //Но раз всё равно потом перепишу - пока так пойдёт.
}                               //Но вот этот костыль будет тяжелее выпиливать. А что ещё делать?

public static class Globals                 //Этот класс является временным костылём исключительно для облегчения разработки. Сюда я вынесу всякие настройки, по типу цвета, радиуса и т.д. Выпиливать его будет несложно, ведь можно просто пробежаться повсюду через Ctrl+F и позаменять. Выпиливаться он будет постепенно, по мере изучения делегатов и т.д.
{
    public static readonly int VertexRadius = 10;
    public static readonly IImmutableSolidColorBrush BrushColor = Brushes.Green;
    public static readonly Color FillColor = Colors.Black;
    public static readonly VertexShape VertexShape = VertexShape.Circle;

}