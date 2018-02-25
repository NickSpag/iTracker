// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using SceneKit;

namespace iTracker
{
    public partial class TrackingViewController : UIViewController
    {
        private Random randomizer = new Random();

        private UIView backingView => this.View;

        public TrackingViewController(IntPtr handle) : base(handle)
        {

        }

        private SCNNode faceNode;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            arView.Delegate = new SessionDelegate();

        }

        private CGPoint getStartingPoint()
        {
            var pointToStart = this.View.Frame.Location;
            pointToStart.X += 20;
            pointToStart.Y += 20;

            return pointToStart;
        }

        private CGPoint getEndingPoint()
        {
            var endingPoint = getStartingPoint();
            endingPoint.Y += this.View.Frame.Height - 100;
            endingPoint.X += this.View.Frame.Width - 100;

            return endingPoint;
        }



        private CAKeyFrameAnimation CreateTrackingAnimation()
        {
            var start = getStartingPoint();
            var endingPoint = getEndingPoint();

            var animation = CAKeyFrameAnimation.FromKeyPath("position");
            animation.Path = GenerateRandomPath(start);
            animation.Duration = 2;

            return animation;
        }

        private CAShapeLayer CreateTrackingDot()
        {
            var circleLayer = new CAShapeLayer();

            var circlePath = CGPath.EllipseFromRect(new CGRect(new CGPoint(0, 0), new CGSize(10, 10)));

            circleLayer.Position = new CGPoint(50, 50);
            circleLayer.Path = circlePath;
            circleLayer.FillColor = UIColor.Red.CGColor;

            return circleLayer;
        }

        private CGPath GenerateRandomPath(CGPoint start)
        {
            var endingPoint = getEndingPoint();

            var path = new CGPath();

            path.MoveToPoint(start);
            path.AddArcToPoint(start.X, start.Y, endingPoint.X, endingPoint.Y, 10);
            //path.AddLineToPoint(endingPoint.X, endingPoint.Y);

            return path;
        }
    }
}