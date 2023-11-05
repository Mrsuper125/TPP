using System;

namespace Polygons;

class TriangleVertex: Shape
{
    public readonly double TopVertexX;
    public readonly double TopVertexY;
    public readonly double LeftVertexX;
    public readonly double LeftVertexY;
    public readonly double RightVertexX;
    public readonly double RightVertexY;

    public TriangleVertex(double x, double y) : base(x, y)
    {
        TopVertexX = this.x;
        TopVertexY = this.y + VertexRadius;
        LeftVertexX = this.x - Math.Cos(0.523599d) * VertexRadius;
        LeftVertexY = this.y - Math.Sin(0.523599d) * VertexRadius;
        RightVertexX = this.x + Math.Cos(0.523599d) * VertexRadius;
        RightVertexY = LeftVertexY;
    }

    public override bool IsInside(double x, double y)
    {
        if (!((y - LeftVertexY)/(TopVertexY - LeftVertexY) < (x - LeftVertexX)/(TopVertexX - LeftVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }
        
        if (!((y - RightVertexY)/(TopVertexY - RightVertexY) < (x - RightVertexX)/(TopVertexX - RightVertexX)))  //длинное страншное неравенство, проверяющее нахождение точки под прямой. Инвертируется для того, чтобы отловить неподходящие точки
        {
            return false;
        }

        if (y < RightVertexY)
        {
            return false;
        }

        return true;
    }
}