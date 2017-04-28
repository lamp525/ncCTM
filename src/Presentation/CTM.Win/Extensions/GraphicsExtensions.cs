using System.Drawing;
using System.Drawing.Drawing2D;

namespace CTM.Win.Extensions
{
    public static class GraphicsExtensions
    {
        public static void DrawCustomFlodLineWithArrow(this Graphics g, Pen pen, PointF targetPoint, float foldDX, float foldDY, float straightDX)
        {
            PointF foldPoint = new PointF(targetPoint.X + foldDX, targetPoint.Y + foldDY);
            pen.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(pen, foldPoint ,targetPoint );

            PointF endPoint = new PointF(foldPoint.X + straightDX, foldPoint.Y);
            pen.EndCap = LineCap.NoAnchor;
            g.DrawLine(pen, endPoint, foldPoint);
        }
    }
}