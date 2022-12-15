
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Beacons;

public class Deadzone
{
    private readonly Point[] _polygon;

    public Deadzone(Coordinate centre, int size)
    {
        var top = new Point(centre.X, centre.Y - size);
        var bottom = new Point(centre.X, centre.Y + size);
        var left = new Point(centre.X - size, centre.Y);
        var right = new Point(centre.X + size, centre.Y);
        _polygon = new[] { top, right, bottom, left };
    }

    public bool Contains(Coordinate testCoordinate)
    {
        var testPoint = new Point(testCoordinate.X, testCoordinate.Y);
        return IsInPolygon(_polygon, testPoint);
    }
    
    // adapted from https://stackoverflow.com/a/7123291
    public static bool IsInPolygon(Point[] poly, Point p)
    {
        // this doesn't work for the top, right, bottom, right points themselves
        // so we check those explicitly first
        if (poly.Contains(p))
        {
            return true;
        }

        bool inside = false;

        if (poly.Length < 3)
        {
            return inside;
        }

        var oldPoint = new Point(poly[^1].X, poly[^1].Y);

        for (var i = 0; i < poly.Length; i++)
        {
            var newPoint = new Point(poly[i].X, poly[i].Y);

            Point p1;
            Point p2;
            if (newPoint.X > oldPoint.X)
            {
                p1 = oldPoint;
                p2 = newPoint;
            }
            else
            {
                p1 = newPoint;
                p2 = oldPoint;
            }

            if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                && (p.Y - (long) p1.Y)*(p2.X - p1.X)
                < (p2.Y - (long) p1.Y)*(p.X - p1.X))
            {
                inside = !inside;
            }

            oldPoint = newPoint;
        }

        return inside;
    }
}