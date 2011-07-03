using System;
using System.Windows;

namespace MultiTouch.Behaviors.WP7
{
    public static class Extensions
    {
        public static double DistanceTo(this Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }
    }
}