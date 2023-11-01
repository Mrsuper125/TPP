using System;

namespace Polygons;

class TriangleVertex: Shape
{
    public TriangleVertex(double x, double y) : base(x, y)
    {}

    public override bool IsInside(double x, double y)
    {
        double topVertexX = this.x;
        double topVertexY = this.y + VertexRadius;
        double leftVertexX = this.x - Math.Cos(0.523599d) * VertexRadius;  //30 градусов - половина угла треугольника, гипотенуза которого = радиус описанной
        double leftVertexY = this.y - Math.Sin(0.523599d) * VertexRadius;  //Радианы вместо градусов. Спрашивается, нафига? Это шарп, ничего не попишешь.
        double rightVertexX = this.x + Math.Cos(0.523599d) * VertexRadius;  //30 градусов - половина угла треугольника, гипотенуза которого = радиус описанной
        double rightVertexY = leftVertexY;

        if (!((y - leftVertexY)/(topVertexY - leftVertexY) < (x - leftVertexX)/(topVertexX - leftVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }
        
        if (!((y - rightVertexY)/(topVertexY - rightVertexY) < (x - rightVertexX)/(topVertexX - rightVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }

        if (y < rightVertexY)
        {
            return false;
        }

        return true;
    }
}