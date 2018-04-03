using System;
using CoreAnimation;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using ARKit;
using System.Threading.Tasks;
using CoreML;
using Foundation;
using CoreMotion;

namespace iTracker
{
    public class GazeTrackingSession
    {
        private Random randomizer = new Random();

        private double sessionDurationInSeconds = 20;
        private ARSession currentSession;
        private ARKitSessionDelegate currentSessionDelegate;
        private CMMotionManager motionManager = new CMMotionManager();

        private CAShapeLayer trackingDot;
        private CAShapeLayer testingDot;

        private MLModel yModel;
        private MLModel xModel;

        public GazeTrackingSession()
        {

        }

        public async Task RunTesting(UIView view)
        {
            if (testingDot == null)
            {
                testingDot = CreateTestingDot(new CGPoint(0, 0));
            }

            if (trackingDot == null)
            {
                trackingDot = CreateTrackingDotAt(new CGPoint(randomizer.Next(0, (int)view.Bounds.Width),
                                                              randomizer.Next(0, (int)view.Bounds.Height)));
            }

            view.Layer.AddSublayer(testingDot);
            view.Layer.AddSublayer(trackingDot);

            PrepareARSession();
            PrepareCoreMLModel();

            BeginMotionSession();
            RunSession();

            await Task.Run(() => MonitorGazeForTesting());

            testingDot.RemoveFromSuperLayer();
            trackingDot.RemoveFromSuperLayer();

            EndSession();
        }

        public async Task RunTraining(UIView view)
        {
            PrepareARSession();

            var grid = Help.PathDrawing.CreateGridFromView(view);
            var drawnPath = Help.PathDrawing.DrawPathThroughGrid(grid, view);
            var animation = CreateTrackingAnimation(drawnPath.Path);

            dot = CreateTrackingDotAt(drawnPath.StartingPoint);
            view.Layer.AddSublayer(dot);

            BeginMotionSession();

            RunSession();



            dot.AddAnimation(animation, "position");
            dot.Position = drawnPath.EndingPoint;

            await Task.Run(() => MonitorGazeForTraining());

            dot.RemoveFromSuperLayer();

            EndSession();
        }

        private void PrepareARSession()
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
        List<GazeTrainingSnapshot> snapshots = new List<GazeTrainingSnapshot>();
        string sessionKey = Guid.NewGuid().ToString();
        CAShapeLayer dot;

        public async void AnchorUpdated(ARFaceAnchor faceAnchor)
        {
            var gaze = new GazeTrainingSnapshot(faceAnchor,
                                        //must use the presentation layer to get position *during* an animation
                                        dot.PresentationLayer.Position,
                                        motionManager.DeviceMotion.Attitude);

            gaze.PartitionKey = sessionKey;
            gaze.RowKey = totalCount.ToString();

            snapshots.Add(gaze);
            System.Console.WriteLine($"Snapshot at {dot.PresentationLayer.Position} taken. {count} in batch, total: {totalCount}.");

            List<GazeTrainingSnapshot> snapshotBatch = null;

            lock (snapshotPadlock)
            {
                count++;
                totalCount++;

                if (count > 99)
                {
                    snapshotBatch = snapshots;
                    snapshots = new List<GazeTrainingSnapshot>();

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

        private void PrepareCoreMLModel()
        {
            var xModelPath = NSBundle.MainBundle.GetUrlForResource("xModel", "mlmodelc");
            var yModelPath = NSBundle.MainBundle.GetUrlForResource("yModel", "mlmodelc");

            xModel = MLModel.Create(xModelPath, out NSError xModelErrors);
            System.Console.WriteLine(xModel);

            yModel = MLModel.Create(yModelPath, out NSError yModelErrors);
            System.Console.WriteLine(yModel);
        }

        public void MakePrediction(ARFaceAnchor faceAnchor)
        {
            if (testingDot == null) return;

            var gaze = GazePredictionInput.FromAnchor(faceAnchor, motionManager.DeviceMotion.Attitude);

            try
            {
                var yPrediction = yModel.GetPrediction(gaze, out NSError yPredictionError);
                var yResult = yPrediction.GetFeatureValue("yPoint").DoubleValue;

                var xPrediction = xModel.GetPrediction(gaze, out NSError xPredictionError);
                var xResult = xPrediction.GetFeatureValue("xPoint").DoubleValue;

                System.Console.WriteLine($"Prediction is: ({xResult}, {yResult}");

                testingDot.Position = new CGPoint(xResult, yResult);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        private void AddTestDotToView(double x, double y)
        {
            CreateTestingDot(new CGPoint(x, y));
        }

        private async Task MonitorGazeForTesting()
        {
            if (currentSessionDelegate != null)
            {

                currentSessionDelegate.StartBroadcastingAnchor(MakePrediction);

                await Task.Delay(TimeSpan.FromSeconds(sessionDurationInSeconds - .6));

                currentSessionDelegate.StopBroadcastingAnchor();
            }
        }

        private void BeginMotionSession()
        {
            if (motionManager.GyroAvailable)
            {
                motionManager.StartDeviceMotionUpdates(CMAttitudeReferenceFrame.XMagneticNorthZVertical);
            }
            else
            {
                System.Console.WriteLine("Gyroscope not available");
            }
        }

        private async Task MonitorGazeForTraining()
        {
            sessionKey = Guid.NewGuid().ToString();

            if (currentSessionDelegate != null)
            {
                currentSessionDelegate.StartBroadcastingAnchor(AnchorUpdated);

                await Task.Delay(TimeSpan.FromSeconds(sessionDurationInSeconds - .6));

                currentSessionDelegate.StopBroadcastingAnchor();
            }
        }

        private async Task RecordGazeSnapshots(List<GazeTrainingSnapshot> batch)
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

        private CAShapeLayer CreateTestingDot(CGPoint point)
        {
            var circleLayer = new CAShapeLayer();
            var circlePath = CGPath.EllipseFromRect(new CGRect(new CGPoint(0, 0), new CGSize(8, 8)));

            circleLayer.AnchorPoint = point;
            circleLayer.Path = circlePath;
            circleLayer.FillColor = UIColor.Orange.CGColor;

            return circleLayer;
        }

        private CAShapeLayer CreateTrackingDotAt(CGPoint point)
        {
            var circleLayer = new CAShapeLayer();
            var circlePath = CGPath.EllipseFromRect(new CGRect(new CGPoint(0, 0), new CGSize(8, 8)));

            circleLayer.Position = point;
            circleLayer.Path = circlePath;
            circleLayer.FillColor = UIColor.Red.CGColor;

            return circleLayer;
        }

        private CAKeyFrameAnimation CreateTrackingAnimation(CGPath path)
        {
            var animation = CAKeyFrameAnimation.FromKeyPath("position");
            animation.Path = path;
            animation.Duration = sessionDurationInSeconds;

            return animation;
        }
    }
}