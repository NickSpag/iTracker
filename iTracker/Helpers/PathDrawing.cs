using System;
using System.Collections.Generic;

using UIKit;
using CoreAnimation;
using CoreGraphics;

namespace iTracker
{
    public static partial class Help
    {
        public static class PathDrawing
        {
            private static Random randomizer = new Random();

            private static int rows = 5;
            private static int columns = 3;

            public static List<CGRect> CreateGridFromView(UIView view)
            {
                var gridGuide = new List<CGRect>();

                var gridCellHeight = view.Frame.Height / rows;
                var gridCellWidth = view.Frame.Width / columns;

                var gridCellSize = new CGSize(gridCellWidth, gridCellHeight);

                nfloat columnStartPosition = 0;
                nfloat rowStartPosition = 0;

                for (int column = 0; column < columns; column++)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        var rect = new CGRect(new CGPoint(columnStartPosition, rowStartPosition),
                                                      gridCellSize);

                        gridGuide.Add(rect);
                        rowStartPosition += gridCellHeight;
                    }

                    rowStartPosition = 0;
                    columnStartPosition += gridCellWidth;
                }

                return gridGuide;
            }

            public static (CGPoint StartingPoint, CGPath Path, CGPoint EndingPoint) DrawPathThroughGrid(List<CGRect> grid, UIView view)
            {
                var path = new CGPath();

                var startingPoint = RandomPointInRect(grid[0]);
                grid.RemoveAt(0);
                grid = ShuffleGrid(grid);

                path.MoveToPoint(startingPoint);

                CGPoint previousPoint = startingPoint;
                foreach (var rect in grid)
                {
                    var point = RandomPointInRect(rect);

                    var controlPoint = QuadControlPoint(previousPoint, point, view);

                    path.AddQuadCurveToPoint(controlPoint.X, controlPoint.Y, point.X, point.Y);
                    previousPoint = point;
                }

                return (startingPoint, path, previousPoint);

                List<CGRect> ShuffleGrid(List<CGRect> gridToBeShuffled)
                {
                    var shuffledGrid = new List<CGRect>();

                    while (gridToBeShuffled.Count > 0)
                    {
                        var rect = gridToBeShuffled[randomizer.Next(gridToBeShuffled.Count)];
                        shuffledGrid.Insert(randomizer.Next(shuffledGrid.Count), rect);

                        gridToBeShuffled.Remove(rect);
                    }

                    return shuffledGrid;
                }

                CGPoint RandomPointInRect(CGRect rect)
                {
                    return new CGPoint(randomizer.Next((int)rect.GetMinX(), (int)rect.GetMaxX()),
                                        randomizer.Next((int)rect.GetMinY(), (int)rect.GetMaxY()));
                }
            }

            private static CGPoint QuadControlPoint(CGPoint start, CGPoint end, UIView view)
            {
                var path = new CGPath();

                var midPoint = MidpointBetween(start, end);

                var xModifier = randomizer.Next(0, 100);
                xModifier = (randomizer.Next(0, 1) % 2 == 0) ? xModifier :
                                                               xModifier * -1;

                var yModifier = randomizer.Next(0, 100);
                yModifier = (randomizer.Next(0, 1) % 2 == 0) ? yModifier :
                                                               yModifier * -1;

                midPoint.X += xModifier;
                midPoint.Y += yModifier;

                midPoint.X = AdjustForBoundaries(midPoint.X, view.Bounds.Width);
                midPoint.Y = AdjustForBoundaries(midPoint.Y, view.Bounds.Height);

                return midPoint;

                nfloat AdjustForBoundaries(nfloat value, nfloat bounds)
                {
                    if (value > bounds) return bounds;
                    else if (value < 0) return 0;
                    else return value;
                }

                CGPoint MidpointBetween(CGPoint one, CGPoint two)
                {
                    return new CGPoint(((one.X + two.X) / 2) + ((one.X + two.X) % 2),
                                        ((one.Y + two.Y) / 2) + ((one.Y + two.Y) % 2));
                }
            }

        }
    }
}
