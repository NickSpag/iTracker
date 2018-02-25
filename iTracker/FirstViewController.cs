using System;

using UIKit;
using CoreGraphics;
using CoreAnimation;
using System.Threading.Tasks;

namespace iTracker
{
    public partial class FirstViewController : UIViewController
    {
        private Random randomizer = new Random();

        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            InstructionsLabel.Hidden = true;
            StartTrackingButton.Hidden = false;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async partial void StartTrackingClicked(Foundation.NSObject sender)
        {
            await StartTracking();
        }

        private async Task StartTracking()
        {
            InstructionsLabel.Hidden = false;
            StartTrackingButton.Hidden = true;

            var dot = CreateTrackingDot();
            this.View.Layer.AddSublayer(dot);

            var animateDot = CreateTrackingAnimation();

            dot.Position = getEndingPoint();
            dot.AddAnimation(animateDot, "position");


            //prepare to track
            //track


            //await UIView.AnimateKeyframesAsync(2, 0, UIViewKeyframeAnimationOptions.CalculationModeLinear, await HandleAction());

            //await animateDot.

            //animatedDot.RemoveFromSuperLayer();

            //process it
        }

        async Task HandleAction()
        {

        }

        private CGPoint getStartingPoint()
        {
            var pointToStart = TrackingGuideBox.Frame.Location;
            pointToStart.X += 20;
            pointToStart.Y += 20;

            return pointToStart;
        }

        private CGPoint getEndingPoint()
        {
            var endingPoint = getStartingPoint();
            endingPoint.Y += TrackingGuideBox.Frame.Height - 100;
            endingPoint.X += TrackingGuideBox.Frame.Width - 100;

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

        private void EndTracking(bool finished)
        {
            InstructionsLabel.Hidden = true;
            StartTrackingButton.Hidden = false;
        }

    }
}
