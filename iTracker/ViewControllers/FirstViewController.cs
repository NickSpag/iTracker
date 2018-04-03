using System;
using System.Threading.Tasks;

using Foundation;
using UIKit;
using ARKit;

using Plugin.Permissions.Abstractions;

namespace iTracker
{
    public partial class FirstViewController : UIViewController, IARSessionDelegate
    {
        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private async Task StartTestingSession()
        {
            var trackingSession = new GazeTrackingSession();

            await trackingSession.RunTesting(TrackingGuideBox);
        }

        private async Task StartTrainingSession()
        {
            var trackingSession = new GazeTrackingSession();

            StartTrackingButton.Hidden = true;

            await trackingSession.RunTraining(TrackingGuideBox);

            StartTrackingButton.Hidden = false;
        }

        #region Permissions
        private async Task<bool> CheckPermissions()
        {
            bool hasCameraAccess;
            bool motionSensorAccess;

            if (await Help.Permissions.IsGranted(Permission.Camera) is false)
            {
                hasCameraAccess = await Help.Permissions.Request(Permission.Camera);
            }
            else hasCameraAccess = true;

            if (await Help.Permissions.IsGranted(Permission.Sensors) is false)
            {
                motionSensorAccess = await Help.Permissions.Request(Permission.Sensors);
            }
            else motionSensorAccess = true;

            return (hasCameraAccess && motionSensorAccess);
        }

        private async Task StartTrackingSession(GazeTrackingSessionType sessionType)
        {
            if (await CheckPermissions())
            {
                switch (sessionType)
                {
                    case GazeTrackingSessionType.Testing:
                        await StartTestingSession();
                        break;
                    case GazeTrackingSessionType.Training:
                        await StartTrainingSession();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Bound Actions
        async partial void TestingButtonClicked(NSObject sender)
        {
            await StartTrackingSession(GazeTrackingSessionType.Testing);
        }

        async partial void StartTrackingClicked(NSObject sender)
        {
            await StartTrackingSession(GazeTrackingSessionType.Training);
        }
        #endregion

    }
}
