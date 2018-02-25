using System;
using ARKit;
using Foundation;

namespace iTracker
{
    public class SessionDelegate : ARSessionDelegate, IARSessionObserver
    {
        #region Constructors
        public SessionDelegate()
        {
        }
        #endregion

        #region Override Methods
        public override void CameraDidChangeTrackingState(ARSession session, ARCamera camera)
        {
            var state = "";
            var reason = "";

            switch (camera.TrackingState)
            {
                case ARTrackingState.NotAvailable:
                    state = "Tracking Not Available";
                    break;
                case ARTrackingState.Normal:
                    state = "Tracking Normal";
                    break;
                case ARTrackingState.Limited:
                    state = "Tracking Limited";
                    switch (camera.TrackingStateReason)
                    {
                        case ARTrackingStateReason.ExcessiveMotion:
                            reason = "because of excessive motion";
                            break;
                        case ARTrackingStateReason.Initializing:
                            reason = "because tracking is initializing";
                            break;
                        case ARTrackingStateReason.InsufficientFeatures:
                            reason = "because of insufficient features in the environment";
                            break;
                        case ARTrackingStateReason.None:
                            reason = "because of an unknown reason";
                            break;
                    }
                    break;
            }

            // Inform user
            Console.WriteLine("{0} {1}", state, reason);
        }

        #endregion

        //public static class ARSessionObserverExtensions
        //{
        //    #region ARSessionObserver methods
        //    [Export("session:cameraDidChangeTrackingState:")]
        //    public static void CameraDidChangeTrackingState(ARSession session, ARCamera camera)
        //    {
        //        System.Console.WriteLine("camera changed tracking status");
        //    }

        //    [Export("session:didOutputAudioSampleBuffer:")]
        //    public static void DidOutputAudioSampleBuffer(this IARSessionObserver This, ARSession session, CMSampleBuffer audioSampleBuffer)
        //    {

        //    }

        //    [Export("sessionInterruptionEnded:")]
        //    public static void InterruptionEnded(this IARSessionObserver This, ARSession session)
        //    {
        //        System.Console.WriteLine("interruption ended");
        //    }

        //    [Export("sessionWasInterrupted:")]
        //    public static void WasInterrupted(this IARSessionObserver This, ARSession session)
        //    {
        //        System.Console.WriteLine("interruption started");
        //    }
        //    #endregion
        //}
    }
}
