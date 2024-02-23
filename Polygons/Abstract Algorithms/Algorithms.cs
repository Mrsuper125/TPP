using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Media;

namespace Polygons.Abstract_Algorithms;

public enum AbstractAlgorithms
{
    Zavr, Jarvis        //TODO: optimized Zavr
}

public class Algorithms
{
    
    private static double VectorsCos(Shape first, Shape second, Shape third)
    {
        double firstVectorX = second.X - first.X;
        double firstVectorY = second.Y - first.Y;
        double secondVectorX = second.X - third.X;
        double secondVectorY = second.Y - third.Y;
        double firstVectorLength = Math.Sqrt(firstVectorX * firstVectorX + firstVectorY * firstVectorY);
        double secondVectorLength = Math.Sqrt(secondVectorX * secondVectorX + secondVectorY * secondVectorY);
        return (firstVectorX * secondVectorX + firstVectorY * secondVectorY) /
               (firstVectorLength * secondVectorLength);
    }

    private static double Distance(Shape first, Shape second)
    {
        double firstVectorX = first.X - second.X;
        double firstVectorY = first.Y - second.Y;
        return Math.Sqrt(firstVectorX * firstVectorX + firstVectorY * firstVectorY);
    }
    
    private static void AbstractJarvis(List<Shape> vertices)
    {
        if (vertices.Count >= 3)
        {
            Shape lowest = vertices[0];
            for (int i = 1; i < vertices.Count; i++)
            {
                if ((vertices[i].Y > lowest.Y))
                {
                    lowest = vertices[i];
                }
                else if ((vertices[i].Y == lowest.Y) && (vertices[i].X < lowest.X))
                {
                    lowest = vertices[i];
                }
            }
            
            Shape current = lowest;

            List<Shape> ShapeVertices = new List<Shape>(0);
            ShapeVertices.Add(current);
            
            Shape previous = new CircleVertex(lowest.X - 100, lowest.Y);
            Shape next;

            double currentDistance;
            
            do
            {
                currentDistance = Double.MaxValue;
                double MinCos = 1;
                next = null;
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (VectorsCos(previous, current, vertices[i]) < MinCos)
                    {
                        next = vertices[i];
                        MinCos = VectorsCos(previous, current, vertices[i]);
                        currentDistance = Distance(current, vertices[i]);
                    }
                    else
                    {
                        if (VectorsCos(previous, current, vertices[i]) == MinCos &&
                            Distance(current, vertices[i]) < currentDistance)
                        {
                            next = vertices[i];
                            currentDistance = Distance(current, vertices[i]);
                        }
                    }

                }

                previous = current;
                current = next;

                ShapeVertices.Add(current);

            } while (current != lowest);
        }
    }
    
    private static void AbstractZavr(List<Shape> vertices) 
        {
            if (vertices.Count >= 3)
            {
                for (int i = 0; i < vertices.Count - 1; i++)
                {
                    for (int j = i + 1; j < vertices.Count; j++)
                    {
                        Shape first = vertices[i];
                        Shape second = vertices[j];
                        double k = (second.Y - first.Y) / (second.X - first.X);
                        double b = first.Y - first.X * k;
                        int above = 0;
                        int below = 0;
                        for (int l = 0; l < vertices.Count; l++)
                        {
                            if ((l != i) && (l != j))
                            {
                                Shape checking = vertices[l];
                                double tempY = k * checking.X + b;
                                if (tempY > checking.Y)
                                {
                                    below++;
                                }

                                if (tempY < checking.Y)
                                {
                                    above++;
                                }
                            }
                        }

                        if (above == 0 || below == 0)
                        {
                            //Pen pen = new Pen(Globals.BrushColor, 1, lineCap: PenLineCap.Square);
                            //drawingContext.DrawLine(pen, new Point(first.X, first.Y), new Point(second.X, second.Y));
                            first.IsConnected = true;
                            second.IsConnected = true;
                        }
                    }
                }
            }
        }

    private static List<Shape> GeneratePoints(int amount)
    {
        List<Shape> res = new List<Shape>();
        Random rand = new Random();
        for (int i = 0; i < amount; i++)
        {
            res.Add(new CircleVertex(1500 * rand.NextDouble(), 1000 * rand.NextDouble()));
        }

        return res;
    }

    public static TimeSpan MeasureTime(AbstractAlgorithms alg, int amount)
    {
        List<Shape> points = GeneratePoints(amount);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        switch (alg)
        {
            case AbstractAlgorithms.Jarvis:
                AbstractJarvis(points);
                break;
            case AbstractAlgorithms.Zavr:
                AbstractZavr(points);
                break;
        }
        sw.Stop();
        return sw.Elapsed;
    }
}