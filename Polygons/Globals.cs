using Avalonia.Media;

namespace Polygons;

public static class Globals                 //Этот класс является временным костылём исключительно для облегчения разработки. Сюда я вынесу всякие настройки, по типу цвета, радиуса и т.д. Выпиливать его будет несложно, ведь можно просто пробежаться повсюду через Ctrl+F и позаменять. Выпиливаться он будет постепенно, по мере изучения делегатов и т.д.
{
    public static int VertexRadius = 5;
    public static IImmutableSolidColorBrush BrushColor = Brushes.Green;
    public static Color FillColor = Colors.Black;
}