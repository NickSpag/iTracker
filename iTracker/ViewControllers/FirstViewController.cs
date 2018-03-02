using System;

using UIKit;
using CoreGraphics;
using CoreAnimation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using ARKit;
using System.Linq;
using Foundation;

namespace iTracker
{
    public partial class FirstViewController : UIViewController, IARSessionDelegate
    {
        private Random randomizer = new Random();


        private ARFaceAnchor _currentFaceAnchor;
        private ARFaceAnchor currentFaceAnchor
        {
            get => _currentFaceAnchor;
            set
            {
                if (_currentFaceAnchor != null)
                {
                    _currentFaceAnchor.Dispose();
                }

                _currentFaceAnchor = value;
            }
        }

        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #region Overrides
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        #endregion

        async partial void StartTrackingClicked(Foundation.NSObject sender)
        {
            await StartTracking();
        }

        private async Task StartTracking()
        {
            var trackingSession = new GazeTrackingSession();

            StartTrackingButton.Hidden = true;

            await trackingSession.RunInView(TrackingGuideBox);

            StartTrackingButton.Hidden = false;

        }

    }
}
