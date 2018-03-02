using System;
using CoreAnimation;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using ARKit;
using System.Threading.Tasks;
using System.Threading;

namespace iTracker
{
    public class GazeTrackingSession
    {
        private Random randomizer = new Random();
        private const double sessionDurationInSeconds = 14;

        private ARSession currentSession;
        private ARKitSessionDelegate currentSessionDelegate;

        public GazeTrackingSession()
        {

        }


        public async Task RunInView(UIView view)
        {
            PrepareSession();

            var grid = CreateGridFromView(view);
            var drawnPath = DrawStraightPathThroughGrid(grid);
            var animation = CreateTrackingAnimation(drawnPath.Path);

            dot = CreateTrackingDotAt(drawnPath.StartingPoint);

            view.Layer.AddSublayer(dot);

            RunSession();

            dot.AddAnimation(animation, "position");
            dot.Position = drawnPath.EndingPoint;

            await Task.Run(() => MonitorGaze(dot));

            dot.RemoveFromSuperLayer();

            EndSession();
        }



        private void PrepareSession()
        {
            currentSession = new ARSession();
            currentSessionDelegate = new ARKitSessionDelegate();
            currentSession.Delegate = currentSessionDelegate;
        }

        private void RunSession()
        {
            var faceTrackingConfig = new ARFaceTrackingConfiguration();

            currentSession.Run(faceTrackingConfig, ARSessionRunOptions.RemoveExistingAnchors);
        }

        private void EndSession()
        {
            if (currentSession != null)
            {
                currentSession.Pause();
                currentSession = null;
            }

            if (currentSessionDelegate != null)
            {
                currentSessionDelegate = null;
            }
        }

        #region Tracking
        private int count = 1;
        private int totalCount = 1;
        private object snapshotPadlock = new object();
        List<GazeSnapshot> snapshots = new List<GazeSnapshot>();
        string sessionKey = Guid.NewGuid().ToString();
        CAShapeLayer dot;

        public async void AnchorUpdated(ARFaceAnchor faceAnchor)
        {
            var gaze = new GazeSnapshot(faceAnchor,
                                        //must use the presentation layer to get position during an animation
                                        dot.PresentationLayer.Position);


            gaze.PartitionKey = sessionKey;
            gaze.RowKey = totalCount.ToString();

            snapshots.Add(gaze);
            System.Console.WriteLine($"Snapshot at {dot.PresentationLayer.Position} taken. {count} in batch, total: {totalCount}.");

            List<GazeSnapshot> snapshotBatch = null;

            lock (snapshotPadlock)
            {
                count++;
                totalCount++;

                if (count > 99)
                {
                    snapshotBatch = snapshots;
                    snapshots = new List<GazeSnapshot>();

                    count = 1;
                }
            }

            if (snapshotBatch != null)
            {
                try
                {
                    await Task.Run(() => RecordGazeSnapshots(snapshotBatch));
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        private async Task MonitorGaze(CAShapeLayer dot1)
        {
            //int count = 0;
            //int totalCount = 0;

            sessionKey = Guid.NewGuid().ToString();

            //var snapshotPadlock = new Object();
            //List<GazeSnapshot> snapshots = new List<GazeSnapshot>();

            //var timer = new Timer(5);

            if (currentSessionDelegate != null)
            {
                currentSessionDelegate.StartBroadcastingAnchor(AnchorUpdated);

                await Task.Delay(TimeSpan.FromSeconds(sessionDurationInSeconds - .6));

                currentSessionDelegate.StopBroadcastingAnchor();
            }

            //         await Task.Delay(TimeSpan.FromSeconds(sessionDurationInSeconds - .6));

            //void TakeGazeSnapshot(object sender, ElapsedEventArgs e)
            //{

            //    UIApplication.SharedApplication.BeginInvokeOnMainThread(() =>
            //    {

            //    });
            //    var gaze = new GazeSnapshot(currentSessionDelegate.FaceAnchor,
            //                                //must use the presentation layer to get position during an animation
            //                                dot.PresentationLayer.AnchorPoint);

            //    gaze.PartitionKey = sessionKey;
            //    gaze.RowKey = totalCount.ToString();

            //    snapshots.Add(gaze);

            //    lock (snapshotPadlock)
            //    {
            //        if (count >= 100)
            //        {
            //            List<GazeSnapshot> snapshotBatch;

            //            snapshotBatch = snapshots;
            //            snapshots = new List<GazeSnapshot>();

            //            //                        System.Console.WriteLine("Recording");
            //            Task.Run(() => RecordGazeSnapshots(snapshotBatch));

            //            count = 0;
            //        }

            //        count++;
            //        totalCount++;

            //        System.Console.WriteLine($"Snapping - {totalCount}");
            //    }
            //}
        }

        private async Task RecordGazeSnapshots(List<GazeSnapshot> batch)
        {
            try
            {
                System.Console.WriteLine("Recording");
                await AzureTableStorage.UploadSnapshots(batch);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        #endregion

        private CAShapeLayer CreateTrackingDotAt(CGPoint point)
        {
            var circleLayer = new CAShapeLayer();
            var circlePath = CGPath.EllipseFromRect(new CGRect(new CGPoint(0, 0), new CGSize(8, 8)));

            //circleLayer.Position = new CGPoint(50, 50);
            circleLayer.Path = circlePath;
            circleLayer.FillColor = UIColor.Red.CGColor;

            //circleLayer.AnchorPoint = point;

            return circleLayer;
        }

        #region Path Drawing
        private static int rows = 6;
        private static int columns = 4;

        private List<CGRect> CreateGridFromView(UIView view)
        {
            var gridGuide = new List<CGRect>();

            var gridCellHeight = view.Frame.Height / rows;
            var gridCellWidth = view.Frame.Width / columns;

            var gridCellSize = new CGSize(gridCellWidth, gridCellHeight);

            nfloat columnStartPosition = 0;
            nfloat rowStartPosition = 0;

            for (int column = 0; column < columns; column++)
            {
                //Uncomment the three comment sections if you want it to be more snake-like, as opposed to top bottom
                //var evenColumn = (column % 2 == 0);

                for (int row = 0; row < rows; row++)
                {
                    var rect = new CGRect(new CGPoint(columnStartPosition, rowStartPosition),
                                          gridCellSize);
                    gridGuide.Add(rect);

                    //if (!evenColumn)
                    //{
                    //    rowStartPosition -= gridCellHeight;
                    //}
                    //else
                    rowStartPosition += gridCellHeight;
                }

                //if (evenColumn)
                //{
                //    rowStartPosition = view.Frame.Height;
                //}
                //else
                rowStartPosition = 0;
                columnStartPosition += gridCellWidth;
            }

            //for (int column = 0; column < columns; column++)
            //{
            //    var evenColumn = (column % 2 == 0);

            //    for (int row = 1; row <= rows; row++)
            //    {
            //        var rect = new CGRect(new CGPoint(gridCellWidth * column, gridCellHeight * row), gridCellSize);

            //        gridGuide.Add(rect);
            //    }
            //}

            return gridGuide;
        }


        private (CGPoint StartingPoint, CGPath Path, CGPoint EndingPoint) DrawStraightPathThroughGrid(List<CGRect> grid)
        {
            var path = new CGPath();

            var startingPoint = RandomPointInRect(grid[0]);
            grid.RemoveAt(0);

            //start at the same starting cell
            path.MoveToPoint(startingPoint);

            for (int count = 0; count < grid.Count - 1; count++)
            {
                path.AddLineToPoint(NextPointFromGrid(count));
            }

            var endingPoint = NextPointFromGrid(0);
            path.AddLineToPoint(endingPoint);

            return (startingPoint, path, endingPoint);

            CGPoint NextPointFromGrid(int index)
            {
                var rect = grid[index];
                grid.Remove(rect);

                return RandomPointInRect(rect);
            }

            CGPoint RandomPointInRect(CGRect rect)
            {
                return new CGPoint(randomizer.Next((int)rect.GetMinX(), (int)rect.GetMaxX()),
                                   randomizer.Next((int)rect.GetMinY(), (int)rect.GetMaxY()));
            }
        }


        private (CGPoint StartingPoint, CGPath Path) DrawPathThroughGrid(List<CGRect> grid)
        {
            var path = new CGPath();

            var startingPoint = RandomPointInRect(grid[0]);
            grid.RemoveAt(0);

            //start at the same starting cell
            path.MoveToPoint(startingPoint);

            var previousPoint = startingPoint;
            bool first = true;

            while (grid.Count > 0)
            {
                var rect = grid[randomizer.Next(grid.Count)];
                var point = RandomPointInRect(rect);

                var midPoint = MidPointForPoints(startingPoint, point);

                if (first)
                {
                    path.AddLineToPoint(midPoint);
                    first = false;
                }
                else
                {
                    path.AddQuadCurveToPoint(previousPoint.X, previousPoint.Y, midPoint.X, midPoint.Y);
                }

                grid.Remove(rect);
                previousPoint = point;
            }

            return (startingPoint, path);

            CGPoint RandomPointInRect(CGRect rect)
            {
                return new CGPoint(randomizer.Next((int)rect.GetMinX(), (int)rect.GetMaxX()),
                                   randomizer.Next((int)rect.GetMinY(), (int)rect.GetMaxY()));
            }

            CGPoint MidPointForPoints(CGPoint one, CGPoint two)
            {
                return new CGPoint((one.X + two.X) / 2,
                                   (one.Y + two.Y) / 2);
            }
        }

        private CAKeyFrameAnimation CreateTrackingAnimation(CGPath path)
        {
            var animation = CAKeyFrameAnimation.FromKeyPath("position");
            animation.Path = path;
            animation.Duration = sessionDurationInSeconds;

            return animation;
        }

        #endregion

    }

}